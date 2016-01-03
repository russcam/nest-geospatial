using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeCircleFilterDescriptorExtensions
    {
        public static GeoShapeCircleFilterDescriptor Coordinates(
            this GeoShapeCircleFilterDescriptor descriptor,
            IPoint point)
        {
            return descriptor.Coordinates(point.GetCoordinates());
        }
    }
}