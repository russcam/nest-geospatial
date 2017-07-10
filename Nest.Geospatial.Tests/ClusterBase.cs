using System;
using Nest.Geospatial.Tests.Process;

namespace Nest.Geospatial.Tests
{
	public abstract class ClusterBase : IDisposable
	{
		private const string ElasticsearchVersion = "1.7.5";

		protected ClusterBase()
		{
			var name = this.GetType().Name.Replace("Cluster", "");
			this.Node = new ElasticsearchNode(ElasticsearchVersion, true, false, name, false);
			this.Node.BootstrapWork.Subscribe(handle =>
			{
				this.Boostrap();
				handle.Set();
			});
			this.ConsoleOut = this.Node.Start(name, this.ServerSettings);
		}

		public ElasticsearchNode Node { get; }

		protected virtual string[] ServerSettings { get; } = { };

		protected IObservable<ElasticsearchMessage> ConsoleOut { get; set; }

		public virtual void Boostrap()
		{
		}

		public void Dispose() => this.Node?.Dispose();

		public virtual IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> settings = null) =>
			this.Node.GetClient(settings);
	}
}