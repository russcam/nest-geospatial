using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeMultiLineStringFilterDescriptor
	/// </summary>
	public static class GeoShapeMultiLineStringFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the MultiLineString
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="multiLineString">the MultiLineString</param>
		/// <returns>the <see cref="GeoShapeMultiLineStringFilterDescriptor"/></returns>
		public static GeoShapeMultiLineStringFilterDescriptor Coordinates(
            this GeoShapeMultiLineStringFilterDescriptor descriptor, 
            IMultiLineString multiLineString) => descriptor.Coordinates(multiLineString.GetCoordinates());
    }
}