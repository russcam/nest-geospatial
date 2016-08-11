using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeMultiPolygonQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapeMultiPolygonQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the MultiPolygon
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="multiPolygon">the MultiPolygon</param>
		/// <returns>the <see cref="GeoShapeMultiPolygonQueryDescriptor{T}"/></returns>
		public static GeoShapeMultiPolygonQueryDescriptor<T> Coordinates<T>(
            this GeoShapeMultiPolygonQueryDescriptor<T> descriptor,
            IMultiPolygon multiPolygon) where T : class
        {
            return descriptor.Coordinates(multiPolygon.GetCoordinates());
        }

		/// <summary>
		/// Sets the boost
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="boost">the boost</param>
		/// <returns>the <see cref="GeoShapeMultiPolygonQueryDescriptor{T}"/></returns>
		public static GeoShapeMultiPolygonQueryDescriptor<T> Boost<T>(
            this GeoShapeMultiPolygonQueryDescriptor<T> descriptor,
            double? boost = null) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}