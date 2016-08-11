using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for Coordinates
	/// </summary>
    public static class CoordinateExtensions
    {
		/// <summary>
		/// Gets the coordinates for a collection of <see cref="Coordinate"/>
		/// </summary>
		/// <param name="coordinates">The coordinates</param>
		/// <returns>A collection of coordinates</returns>
        public static IEnumerable<IEnumerable<double>> GetCoordinates(this IEnumerable<Coordinate> coordinates)
		{
			return coordinates?.Select(GetCoordinates) ?? Enumerable.Empty<IEnumerable<double>>();
		}

		/// <summary>
		/// Gets the coordinates for a <see cref="Coordinate"/>
		/// </summary>
		/// <param name="coordinate">The coordinate</param>
		/// <returns>The coordinates</returns>
		public static IEnumerable<double> GetCoordinates(this Coordinate coordinate)
		{
			return coordinate == null 
				? Enumerable.Empty<double>() 
				: new[] { coordinate.X, coordinate.Y };
		}
    }
}