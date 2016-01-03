using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeCircleQueryDescriptorExtensions
    {
        public static GeoShapeCircleQueryDescriptor<T> Coordinates<T>(
            this GeoShapeCircleQueryDescriptor<T> descriptor,
            IPoint point) where T : class
        {
            return descriptor.Coordinates(point.GetCoordinates());
        }

        public static GeoShapeCircleQueryDescriptor<T> Boost<T>(
            this GeoShapeCircleQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        } 
    }
}