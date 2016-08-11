using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for Points
	/// </summary>
    public static class PointExtensions
    {
		/// <summary>
		/// Gets the coordinates for an <see cref="IPoint"/>
		/// </summary>
		/// <param name="point">the Point</param>
		/// <returns>A coordinate</returns>
        public static IEnumerable<double> GetCoordinates(this IPoint point)
		{
			return point == null 
				? Enumerable.Empty<double>() 
				: point.Coordinate.GetCoordinates();
		}
    }
}