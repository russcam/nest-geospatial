using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeMultiPointQueryDescriptorExtensions
    {
        public static GeoShapeMultiPointQueryDescriptor<T> Coordinates<T>(
            this GeoShapeMultiPointQueryDescriptor<T> descriptor, 
            IMultiPoint multiPoint) where T : class
        {
            return descriptor.Coordinates(multiPoint.GetCoordinates());
        }

        public static GeoShapeMultiPointQueryDescriptor<T> Boost<T>(
            this GeoShapeMultiPointQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}