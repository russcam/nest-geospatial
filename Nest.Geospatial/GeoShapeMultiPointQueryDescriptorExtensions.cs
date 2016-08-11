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
            IMultiPoint multiPoint) where T : class
        {
            return descriptor.Coordinates(multiPoint.GetCoordinates());
        }

		/// <summary>
		/// Sets the boost
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="boost">the boost</param>
		/// <returns>the <see cref="GeoShapeMultiPointQueryDescriptor{T}"/></returns>
		public static GeoShapeMultiPointQueryDescriptor<T> Boost<T>(
            this GeoShapeMultiPointQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}