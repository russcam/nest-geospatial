namespace Nest.Geospatial.Tests
{
	public abstract class GeoTests
	{
		private readonly ClusterBase _cluster;

		protected GeoTests(ClusterBase cluster)
		{
			_cluster = cluster;
			Client = _cluster.GetClient();
		}

		protected IElasticClient Client { get;  }
	}
}