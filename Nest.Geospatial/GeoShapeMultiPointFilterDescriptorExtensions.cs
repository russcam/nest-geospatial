using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeMultiPointFilterDescriptorExtensions
    {
        public static GeoShapeMultiPointFilterDescriptor Coordinates(
            this GeoShapeMultiPointFilterDescriptor descriptor,
            IMultiPoint multiPoint)
        {
            return descriptor.Coordinates(multiPoint.GetCoordinates());
        }
    }
}