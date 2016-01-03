using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class MultiLineStringExtensions
    {
        public static IEnumerable<IEnumerable<IEnumerable<double>>> GetCoordinates(this IMultiLineString multiLineString)
        {
            return multiLineString.Geometries
                .Cast<ILineString>()
                .Select(l => l.GetCoordinates());
        }
    }
}