using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class MultiPointExtensions
    {
        public static IEnumerable<IEnumerable<double>> GetCoordinates(this IMultiPoint multiPoint)
        {
            return multiPoint.Geometries
                .Cast<IPoint>()
                .Select(p => p.GetCoordinates());
        } 
    }
}