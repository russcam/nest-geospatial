using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapePointQueryDescriptorExtensions
    {
        public static GeoShapePointQueryDescriptor<T> Coordinates<T>(
            this GeoShapePointQueryDescriptor<T> descriptor, 
            IPoint point) where T : class
        {
            return descriptor.Coordinates(point.GetCoordinates());
        }

        public static GeoShapePointQueryDescriptor<T> Boost<T>(
            this GeoShapePointQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}