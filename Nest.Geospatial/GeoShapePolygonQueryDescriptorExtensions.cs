using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapePolygonQueryDescriptorExtensions
    {
        public static GeoShapePolygonQueryDescriptor<T> Coordinates<T>(
            this GeoShapePolygonQueryDescriptor<T> descriptor, 
            IPolygon polygon) where T : class
        {
            return descriptor.Coordinates(polygon.GetCoordinates());
        }

        public static GeoShapePolygonQueryDescriptor<T> Boost<T>(
            this GeoShapePolygonQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}