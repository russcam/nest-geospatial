using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeMultiPointFilterDescriptor
	/// </summary>
	public static class GeoShapeMultiPointFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the MultiPoint
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="multiPoint">the MultiPoint</param>
		/// <returns>the <see cref="GeoShapeMultiPointFilterDescriptor"/></returns>
		public static GeoShapeMultiPointFilterDescriptor Coordinates(
            this GeoShapeMultiPointFilterDescriptor descriptor,
            IMultiPoint multiPoint) => descriptor.Coordinates(multiPoint.GetCoordinates());
    }
}