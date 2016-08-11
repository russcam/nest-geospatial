using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapePolygonFilterDescriptor
	/// </summary>
	public static class GeoShapePolygonFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the Polygon
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="polygon">the polygon</param>
		/// <returns>the <see cref="GeoShapePolygonFilterDescriptor"/></returns>
		public static GeoShapePolygonFilterDescriptor Coordinates(
            this GeoShapePolygonFilterDescriptor descriptor, 
            IPolygon polygon)
        {
            return descriptor.Coordinates(polygon.GetCoordinates());
        }
    }
}