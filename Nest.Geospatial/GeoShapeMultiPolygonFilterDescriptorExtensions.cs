using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeMultiPolygonFilterDescriptor
	/// </summary>
	public static class GeoShapeMultiPolygonFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the MultiPolygon
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="multiPolygon">the MultiPolygon</param>
		/// <returns>the <see cref="GeoShapeMultiPolygonFilterDescriptor"/></returns>
		public static GeoShapeMultiPolygonFilterDescriptor Coordinates(
            this GeoShapeMultiPolygonFilterDescriptor descriptor,
            IMultiPolygon multiPolygon)
        {
            return descriptor.Coordinates(multiPolygon.GetCoordinates());
        }
    }
}