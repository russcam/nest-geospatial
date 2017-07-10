using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Elasticsearch.Net;
using Elasticsearch.Net.ConnectionPool;

namespace Nest.Geospatial.Tests.Process
{
	public class ElasticsearchNode : IDisposable
	{
		private static readonly object Lock = new object();
		private string[] DefaultNodeSettings { get; }

		private readonly bool _doNotSpawnIfAlreadyRunning;
		private readonly bool _shieldEnabled;
		private ObservableProcess _process;
		private IDisposable _processListener;

		public string Version { get; set; }
		public ElasticsearchVersionInfo VersionInfo { get; }
		public string Binary { get; }

		private string RoamingFolder { get; }
		private string RoamingClusterFolder { get; }

		public bool Started { get; private set; }
		public bool RunningIntegrations { get; }
		public string ClusterName { get; }
		public string NodeName { get; }
		public string RepositoryPath { get; }
		public ElasticsearchNodeInfo Info { get; private set; }

		public int Port { get; private set; } = 9200;

		private TimeSpan HandleTimeout { get; } = TimeSpan.FromMinutes(10);

		private readonly Subject<ManualResetEvent> _blockingSubject = new Subject<ManualResetEvent>();
		public IObservable<ManualResetEvent> BootstrapWork { get; }

		public ElasticsearchNode(
			string elasticsearchVersion,
			bool runningIntegrations,
			bool doNotSpawnIfAlreadyRunning,
			string name,
			bool shieldEnabled
			)
		{
			this._doNotSpawnIfAlreadyRunning = doNotSpawnIfAlreadyRunning;
			this._shieldEnabled = shieldEnabled;

			var prefix = name.ToLowerInvariant();
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			this.ClusterName = $"{prefix}-cluster-{suffix}";
			this.NodeName = $"{prefix}-node-{suffix}";

			this.VersionInfo = new ElasticsearchVersionInfo(runningIntegrations ? elasticsearchVersion : "0.0.0-unittest");
			this.Version = this.VersionInfo.Version + (this.VersionInfo.IsSnapshot ? $"-{VersionInfo.SnapshotIdentifier}" : string.Empty);
			this.RunningIntegrations = runningIntegrations;

			this.BootstrapWork = _blockingSubject;

			var appData = GetApplicationDataDirectory();
			this.RoamingFolder = Path.Combine(appData, "NEST", this.Version);
			this.RoamingClusterFolder = Path.Combine(this.RoamingFolder, "elasticsearch-" + this.VersionInfo.Version);
			this.RepositoryPath = Path.Combine(RoamingFolder, "repositories");
			this.Binary = Path.Combine(this.RoamingClusterFolder, "bin", "elasticsearch") + ".bat";

			var attr = this.VersionInfo.ParsedVersion.Major >= 5 ? "attr." : "";
			this.DefaultNodeSettings = new[]
			{
				$"es.cluster.name={this.ClusterName}",
				$"es.node.name={this.NodeName}",
				$"es.path.repo=\"{this.RepositoryPath}\"",
				$"es.script.inline=on",
				$"es.script.indexed=on",
				$"es.http.compression=true",
				$"es.node.{attr}testingcluster=true",
				$"es.shield.enabled=" + (shieldEnabled ? "true" : "false")
			};

			if (!runningIntegrations)
			{
				return;
			}

			Console.WriteLine("========> {0}", this.RoamingFolder);
			this.DownloadAndExtractElasticsearch();
		}

		public IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> settings = null)
		{
			if (!this.Started)
				throw new Exception("can not request a client from an ElasticsearchNode if that node hasn't started yet");

			return GetPrivateClient(settings);
		}

		private IElasticClient GetPrivateClient(Func<ConnectionSettings, ConnectionSettings> settings = null)
		{
			var uri = new Uri($"http://{Host}:{Port}");
			var pool = new SingleNodeConnectionPool(uri);
			var s = new ConnectionSettings(pool);

			if (settings != null)
				s = settings(s);

			return new ElasticClient(s);
		}

		public IObservable<ElasticsearchMessage> Start(string typeOfCluster, string[] additionalSettings = null)
		{
			if (!this.RunningIntegrations) return Observable.Empty<ElasticsearchMessage>();

			this.Stop();

			var settingMarker = this.VersionInfo.ParsedVersion.Major >= 5 ? "-E " : "-D";
			var settings = DefaultNodeSettings
				.Concat(additionalSettings ?? Enumerable.Empty<string>())
				.Select(s => $"{settingMarker}{s}")
				.ToList();

			var easyRunBat = Path.Combine(this.RoamingFolder, $"run-{typeOfCluster.ToLowerInvariant()}.bat");
			if (!File.Exists(easyRunBat))
			{
				var badSettings = new[] { "node.name", "cluster.name" };
				var batSettings = string.Join(" ", settings.Where(s => !badSettings.Any(s.Contains)));
				File.WriteAllText(easyRunBat, $@"elasticsearch-{this.Version}\bin\elasticsearch.bat {batSettings}");
			}

			var handle = new ManualResetEvent(false);
			var alreadyRunning = UseAlreadyRunningInstance(handle);
			if (alreadyRunning != null) return alreadyRunning;

			this._process = new ObservableProcess(this.Binary, settings.ToArray());

			var observable = Observable.Using(() => this._process, process => process.Start())
				.Select(consoleLine => new ElasticsearchMessage(consoleLine));
			this._processListener = observable.Subscribe(onNext: s => HandleConsoleMessage(s, handle));

			if (handle.WaitOne(this.HandleTimeout, true)) return observable;

			this.Stop();
			throw new Exception($"Could not start elasticsearch within {this.HandleTimeout}");
		}

		public static bool RunningFiddler = System.Diagnostics.Process.GetProcessesByName("fiddler").Any();

		public static string Host => RunningFiddler ? "ipv4.fiddler" : "localhost";

		private IObservable<ElasticsearchMessage> UseAlreadyRunningInstance(ManualResetEvent handle)
		{
			if (!_doNotSpawnIfAlreadyRunning) return null;

			var client = GetPrivateClient();
			var rootNodeInfo = client.RootNodeInfo();

			if (!rootNodeInfo.IsValid) return null;

			this.Started = true;
			this.Info = new ElasticsearchNodeInfo(rootNodeInfo.Version.Number, null, rootNodeInfo.Version.LuceneVersion);
			this._blockingSubject.OnNext(handle);

			if (!handle.WaitOne(this.HandleTimeout, true))
				throw new Exception($"Could not launch tests on already running elasticsearch within {this.HandleTimeout}");

			return Observable.Empty<ElasticsearchMessage>();
		}

		private void HandleConsoleMessage(ElasticsearchMessage message, ManualResetEvent handle)
		{
			if (!this.RunningIntegrations || this.Started) return;

			ElasticsearchNodeInfo info;
			int port;

			if (message.TryParseNodeInfo(out info))
			{
				this.Info = info;
			}
			else if (message.TryGetStartedConfirmation())
			{
				var healthyCluster = this.GetPrivateClient().ClusterHealth(g => g
					.WaitForStatus(WaitForStatus.Yellow)
					.Timeout("30s")
				);

				if (healthyCluster.IsValid)
				{
					this.Started = true;
					this._blockingSubject.OnNext(handle);
				}
				else
				{
					this._blockingSubject.OnError(new Exception("Did not see a healthy cluster after the node started for 30 seconds"));
					handle.Set();
					this.Stop();
				}
			}
			else if (message.TryGetPortNumber(out port))
			{
				this.Port = port;
			}
		}

		private void DownloadAndExtractElasticsearch()
		{
			lock (Lock)
			{
				var localZip = Path.Combine(this.RoamingFolder, this.VersionInfo.Zip);

				Directory.CreateDirectory(this.RoamingFolder);
				if (!File.Exists(localZip))
				{
					Console.WriteLine($"Download elasticsearch: {this.VersionInfo.Version} from {this.VersionInfo.DownloadUrl}");
					new WebClient().DownloadFile(this.VersionInfo.DownloadUrl, localZip);
					Console.WriteLine($"Downloaded elasticsearch: {this.VersionInfo.Version}");
				}

				if (!Directory.Exists(this.RoamingClusterFolder))
				{
					Console.WriteLine($"Unzipping elasticsearch: {this.VersionInfo.Version} ...");
					ZipFile.ExtractToDirectory(localZip, this.RoamingFolder);
				}

				var easyRunBat = Path.Combine(this.RoamingClusterFolder, "run.bat");
				if (!File.Exists(easyRunBat))
				{
					File.WriteAllText(easyRunBat, @"bin\elasticsearch.bat ");
				}
			}
		}

		private string GetApplicationDataDirectory() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		public void Stop()
		{
			var hasStarted = this.Started;
			this.Started = false;

			Console.WriteLine($"Stopping... node has started {hasStarted} ran integrations: {this.RunningIntegrations}");

			this._process?.Dispose();
			this._processListener?.Dispose();

			if (this.Info?.Pid != null)
			{
				var esProcess = System.Diagnostics.Process.GetProcessById(this.Info.Pid.Value);
				Console.WriteLine($"Killing elasticsearch PID {this.Info.Pid}");
				esProcess.Kill();
				esProcess.WaitForExit(5000);
				esProcess.Close();
			}

			if (!this.RunningIntegrations || !hasStarted) return;
			Console.WriteLine($"Node started on port: {this.Port} using PID: {this.Info?.Pid}");

			if (this._doNotSpawnIfAlreadyRunning) return;
			var dataFolder = Path.Combine(this.RoamingClusterFolder, "data", this.ClusterName);
			if (Directory.Exists(dataFolder))
			{
				Console.WriteLine($"attempting to delete cluster data: {dataFolder}");
				Directory.Delete(dataFolder, true);
			}

			var logPath = Path.Combine(this.RoamingClusterFolder, "logs");
			var files = Directory.GetFiles(logPath, this.ClusterName + "*.log");
			foreach (var f in files)
			{
				Console.WriteLine($"attempting to delete log file: {f}");
				File.Delete(f);
			}

			if (Directory.Exists(this.RepositoryPath))
			{
				Console.WriteLine("attempting to delete repositories");
				Directory.Delete(this.RepositoryPath, true);
			}
		}

		public void Dispose()
		{
			this.Stop();
		}
	}
}
