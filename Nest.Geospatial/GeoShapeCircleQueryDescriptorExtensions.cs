using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeCircleQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapeCircleQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the point
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="point">the Point</param>
		/// <returns>the <see cref="GeoShapeCircleQueryDescriptor{T}"/></returns>
		public static GeoShapeCircleQueryDescriptor<T> Coordinates<T>(
            this GeoShapeCircleQueryDescriptor<T> descriptor,
            IPoint point) where T : class
        {
            return descriptor.Coordinates(point.GetCoordinates());
        }

		/// <summary>
		/// Sets the boost
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="boost">the boost</param>
		/// <returns>the <see cref="GeoShapeCircleQueryDescriptor{T}"/></returns>
        public static GeoShapeCircleQueryDescriptor<T> Boost<T>(
            this GeoShapeCircleQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        } 
    }
}