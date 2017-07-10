using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for MultiLineStrings
	/// </summary>
    public static class MultiLineStringExtensions
    {
		/// <summary>
		/// Gets the coordinates for an <see cref="IMultiLineString"/>
		/// </summary>
		/// <param name="multiLineString">the MultiLineString</param>
		/// <returns>A collection of collections of coordinates</returns>
        public static IEnumerable<IEnumerable<IEnumerable<double>>> GetCoordinates(this IMultiLineString multiLineString)
		{
		    return multiLineString == null
		        ? Enumerable.Empty<IEnumerable<IEnumerable<double>>>()
		        : multiLineString.Geometries
		            .Cast<ILineString>()
		            .Select(l => l.GetCoordinates());
		}
    }
}