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
            IMultiLineString multiLineString) where T : class
        {
            return descriptor.Coordinates(multiLineString.GetCoordinates());
        }

		/// <summary>
		/// Sets the boost
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="boost">the boost</param>
		/// <returns>the <see cref="GeoShapeMultiLineStringQueryDescriptor{T}"/></returns>
		public static GeoShapeMultiLineStringQueryDescriptor<T> Boost<T>(
            this GeoShapeMultiLineStringQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}