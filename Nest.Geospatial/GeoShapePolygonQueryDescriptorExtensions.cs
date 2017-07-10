using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapePolygonQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapePolygonQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the Polygon
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="polygon">the Polygon</param>
		/// <returns>the <see cref="GeoShapePolygonQueryDescriptor{T}"/></returns>
        public static GeoShapePolygonQueryDescriptor<T> Coordinates<T>(
            this GeoShapePolygonQueryDescriptor<T> descriptor, 
            IPolygon polygon) where T : class => descriptor.Coordinates(polygon.GetCoordinates());
    }
}