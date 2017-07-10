using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeMultiLineStringQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapeMultiLineStringQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the MultiLineString
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="multiLineString">the MultiLineString</param>
		/// <returns>the <see cref="GeoShapeMultiLineStringQueryDescriptor{T}"/></returns>
		public static GeoShapeMultiLineStringQueryDescriptor<T> Coordinates<T>(
            this GeoShapeMultiLineStringQueryDescriptor<T> descriptor, 
            IMultiLineString multiLineString) where T : class => descriptor.Coordinates(multiLineString.GetCoordinates());
    }
}