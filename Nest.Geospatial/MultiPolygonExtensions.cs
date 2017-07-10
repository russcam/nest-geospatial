using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for MultiPolygons
	/// </summary>
    public static class MultiPolygonExtensions
    {
		/// <summary>
		/// Gets the coordinates for an <see cref="IMultiPolygon"/>
		/// </summary>
		/// <param name="multiPolygon">the MultiPolygon</param>
		/// <returns>A collection of collections of collections of coordinates</returns>
        public static IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> GetCoordinates(
            this IMultiPolygon multiPolygon)
		{
		    return multiPolygon == null
		        ? Enumerable.Empty<IEnumerable<IEnumerable<IEnumerable<double>>>>()
		        : multiPolygon.Geometries
		            .Cast<IPolygon>()
		            .Select(p => p.GetCoordinates());
		}
    }
}