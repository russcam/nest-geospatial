using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Nest.Geospatial.Tests.Process
{
	public class ObservableProcess : IDisposable
	{
		public ObservableProcess(string bin, params string[] args)
		{
			Binary = bin;
			Arguments = string.Join(" ", args);
			Process = new System.Diagnostics.Process
			{
				EnableRaisingEvents = true,
				StartInfo =
				{
					FileName = this.Binary,
					Arguments = this.Arguments,
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					RedirectStandardInput = false
				}
			};
		}

		public string Arguments { get; }

		public string Binary { get; }

		public int? ExitCode { get; set; }

		public System.Diagnostics.Process Process { get; }

		private bool Started { get; set; }

		public void Dispose() => this.Stop();

		public IObservable<string> Start()
		{
			return Observable.Create<string>(observer =>
			{
				var stdOut = this.Process.CreateStandardOutputObservable();
				var stdErr = this.Process.CreateStandardErrorObservable();

				var stdOutSubscription = stdOut.Subscribe(observer);
				var stdErrSubscription = stdErr.Subscribe(observer);

				var processExited = Observable.FromEventPattern(h => this.Process.Exited += h, h => this.Process.Exited -= h);
				var processError = CreateProcessExitSubscription(this.Process, processExited, observer);

				if (!this.Process.Start())
					throw new Exception($"Failed to start observable process: {this.Binary}");

				this.Process.BeginOutputReadLine();
				this.Process.BeginErrorReadLine();
				this.Started = true;

				return new CompositeDisposable(stdOutSubscription, stdErrSubscription, processError);
			});
		}

		public void Stop()
		{
			if (this.Started)
			{
				try
				{
					this.Process?.Kill();
					this.Process?.WaitForExit(2000);
					this.Process?.Close();
				}
				catch (Exception)
				{
				}
			}
			this.Started = false;
		}

		private IDisposable CreateProcessExitSubscription(System.Diagnostics.Process process, IObservable<EventPattern<object>> processExited,
			IObserver<string> observer)
		{
			return processExited.Subscribe(args =>
			{
				try
				{
					this.ExitCode = process?.ExitCode;
					if (process?.ExitCode > 0)
					{
						observer.OnError(new Exception(
							$"Process '{process.StartInfo.FileName}' terminated with error code {process.ExitCode}"));
					}
					else observer.OnCompleted();
				}
				finally
				{
					this.Started = false;
					process?.Close();
				}
			});
		}
	}

	public static class RxProcessUtilities
	{
		public static IObservable<string> CreateStandardErrorObservable(this System.Diagnostics.Process process)
		{
			var receivedStdErr =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.ErrorDataReceived += h, h => process.ErrorDataReceived -= h)
					.Select(e => e.EventArgs.Data);

			return Observable.Create<string>(observer =>
			{
				var cancel = Disposable.Create(process.CancelErrorRead);
				return new CompositeDisposable(cancel, receivedStdErr.Subscribe(observer));
			});
		}

		public static IObservable<string> CreateStandardOutputObservable(this System.Diagnostics.Process process)
		{
			var receivedStdOut =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.OutputDataReceived += h, h => process.OutputDataReceived -= h)
					.Select(e => e.EventArgs.Data);

			return Observable.Create<string>(observer =>
			{
				var cancel = Disposable.Create(process.CancelOutputRead);
				return new CompositeDisposable(cancel, receivedStdOut.Subscribe(observer));
			});
		}
	}
}