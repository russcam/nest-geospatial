using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class MultiPolygonExtensions
    {
        public static IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> GetCoordinates(
            this IMultiPolygon multiPolygon)
        {
            return multiPolygon.Geometries
                .Cast<IPolygon>()
                .Select(p => p.GetCoordinates());
        }
    }
}