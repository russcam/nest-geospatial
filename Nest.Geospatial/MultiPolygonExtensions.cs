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
			if (multiPolygon == null)
			{
				return Enumerable.Empty<IEnumerable<IEnumerable<IEnumerable<double>>>>();
			}

            return multiPolygon.Geometries
                .Cast<IPolygon>()
                .Select(p => p.GetCoordinates());
        }
    }
}