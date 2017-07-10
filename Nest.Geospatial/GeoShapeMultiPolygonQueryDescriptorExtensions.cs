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
            IMultiPolygon multiPolygon) where T : class => descriptor.Coordinates(multiPolygon.GetCoordinates());
    }
}