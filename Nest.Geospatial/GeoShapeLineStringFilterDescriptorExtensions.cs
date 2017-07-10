using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeLineStringFilterDescriptor
	/// </summary>
	public static class GeoShapeLineStringFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the LineString
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="lineString">the LineString</param>
		/// <returns>the <see cref="GeoShapeLineStringFilterDescriptor"/></returns>
		public static GeoShapeLineStringFilterDescriptor Coordinates(
            this GeoShapeLineStringFilterDescriptor descriptor, 
            ILineString lineString) => descriptor.Coordinates(lineString.GetCoordinates());
    }
}