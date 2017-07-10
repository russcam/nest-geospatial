using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for <see cref="GeoShapeCircleQueryDescriptor{T}"/>
	/// </summary>
	public static class GeoShapeCircleQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the point
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="point">the point</param>
		/// <returns>the <see cref="GeoShapeCircleQueryDescriptor{T}"/></returns>
		public static GeoShapeCircleQueryDescriptor<T> Coordinates<T>(
            this GeoShapeCircleQueryDescriptor<T> descriptor,
            IPoint point) where T : class => descriptor.Coordinates(point.GetCoordinates());
    }
}