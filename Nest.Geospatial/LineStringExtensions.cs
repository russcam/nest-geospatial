using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for LineStrings
	/// </summary>
    public static class LineStringExtensions
    {
		/// <summary>
		/// Gets the coordinates for a <see cref="ILineString"/>
		/// </summary>
		/// <param name="lineString">the LineString</param>
		/// <returns>A collection of coordinates</returns>
        public static IEnumerable<IEnumerable<double>> GetCoordinates(this ILineString lineString)
		{
			return lineString == null 
				? Enumerable.Empty<IEnumerable<double>>() 
				: lineString.Coordinates.GetCoordinates();
		}
    }
}