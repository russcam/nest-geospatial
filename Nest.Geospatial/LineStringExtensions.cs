using System.Collections.Generic;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class LineStringExtensions
    {
        public static IEnumerable<IEnumerable<double>> GetCoordinates(this ILineString lineString)
        {
            return lineString.Coordinates.GetCoordinates();
        }
    }
}