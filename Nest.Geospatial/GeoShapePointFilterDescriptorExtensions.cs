using System.Collections;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapePointFilterDescriptor
	/// </summary>
	public static class GeoShapePointFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the Point
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="point">the Point</param>
		/// <returns>the <see cref="GeoShapePointFilterDescriptor"/></returns>
		public static GeoShapePointFilterDescriptor Coordinates(this GeoShapePointFilterDescriptor descriptor, IPoint point)
        {
            return descriptor.Coordinates(point.GetCoordinates());
        }
    }
}