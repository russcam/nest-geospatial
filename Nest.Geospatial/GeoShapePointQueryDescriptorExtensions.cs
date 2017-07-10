using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapePointQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapePointQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the Point
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="point">the Point</param>
		/// <returns>the <see cref="GeoShapePointQueryDescriptor{T}"/></returns>
		public static GeoShapePointQueryDescriptor<T> Coordinates<T>(
            this GeoShapePointQueryDescriptor<T> descriptor, 
            IPoint point) where T : class => descriptor.Coordinates(point.GetCoordinates());
    }
}