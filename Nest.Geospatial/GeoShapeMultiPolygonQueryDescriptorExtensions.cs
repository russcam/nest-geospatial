using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeMultiPolygonQueryDescriptorExtensions
    {
        public static GeoShapeMultiPolygonQueryDescriptor<T> Coordinates<T>(
            this GeoShapeMultiPolygonQueryDescriptor<T> descriptor,
            IMultiPolygon multiPolygon) where T : class
        {
            return descriptor.Coordinates(multiPolygon.GetCoordinates());
        }

        public static GeoShapeMultiPolygonQueryDescriptor<T> Boost<T>(
            this GeoShapeMultiPolygonQueryDescriptor<T> descriptor,
            double? boost = null) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}