using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for MultiPoints
	/// </summary>
    public static class MultiPointExtensions
    {
		/// <summary>
		/// Gets the coordinates for a <see cref="IMultiPoint"/>
		/// </summary>
		/// <param name="multiPoint">the MultiPoint</param>
		/// <returns>A collection of coordinates</returns>
        public static IEnumerable<IEnumerable<double>> GetCoordinates(this IMultiPoint multiPoint)
		{
		    return multiPoint == null
		        ? Enumerable.Empty<IEnumerable<double>>()
		        : multiPoint.Geometries
		            .Cast<IPoint>()
		            .Select(p => p.GetCoordinates());
		} 
    }
}