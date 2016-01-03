using System.Collections.Generic;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class PointExtensions
    {
        public static IEnumerable<double> GetCoordinates(this IPoint point)
        {
            return point.Coordinate.GetCoordinates();
        }
    }
}