using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeCircleFilterDescriptor
	/// </summary>
	public static class GeoShapeCircleFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the coordinates of the Point
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="point">the Point</param>
		/// <returns>the <see cref="GeoShapeCircleFilterDescriptor"/></returns>
		public static GeoShapeCircleFilterDescriptor Coordinates(
            this GeoShapeCircleFilterDescriptor descriptor,
            IPoint point) => descriptor.Coordinates(point.GetCoordinates());
    }
}