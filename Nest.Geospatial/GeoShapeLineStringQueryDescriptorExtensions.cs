using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeLineStringQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapeLineStringQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the LineString
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="descriptor"></param>
		/// <param name="lineString"></param>
		/// <returns></returns>
        public static GeoShapeLineStringQueryDescriptor<T> Coordinates<T>(
            this GeoShapeLineStringQueryDescriptor<T> descriptor, 
            ILineString lineString) where T : class => descriptor.Coordinates(lineString.GetCoordinates());
    }
}