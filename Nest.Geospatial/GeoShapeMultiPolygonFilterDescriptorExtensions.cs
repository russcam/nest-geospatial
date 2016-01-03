using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeMultiPolygonFilterDescriptorExtensions
    {
        public static GeoShapeMultiPolygonFilterDescriptor Coordinates(
            this GeoShapeMultiPolygonFilterDescriptor descriptor,
            IMultiPolygon multiPolygon)
        {
            return descriptor.Coordinates(multiPolygon.GetCoordinates());
        }
    }
}