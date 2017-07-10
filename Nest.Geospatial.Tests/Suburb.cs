using GeoAPI.Geometries;

namespace Nest.Geospatial.Tests
{
	public class Suburb
	{
		public IGeometry Geometry { get; set; }

		public int Id { get; set; }

		public string Name { get; set; }

		public State State { get; set; }
	}
}