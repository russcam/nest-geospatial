using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeMultiPointQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapeMultiPointQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the MultiPoint
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="multiPoint">the MultiPoint</param>
		/// <returns>the <see cref="GeoShapeMultiPointQueryDescriptor{T}"/></returns>
		public static GeoShapeMultiPointQueryDescriptor<T> Coordinates<T>(
            this GeoShapeMultiPointQueryDescriptor<T> descriptor, 
            IMultiPoint multiPoint) where T : class => descriptor.Coordinates(multiPoint.GetCoordinates());
    }
}