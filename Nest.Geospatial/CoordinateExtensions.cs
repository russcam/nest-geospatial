using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class CoordinateExtensions
    {
        public static IEnumerable<IEnumerable<double>> GetCoordinates(this IEnumerable<Coordinate> coordinates)
        {
            return coordinates.Select(GetCoordinates);
        }

        public static IEnumerable<double> GetCoordinates(this Coordinate coordinate)
        {
            return new[] { coordinate.X, coordinate.Y };
        } 
    }
}