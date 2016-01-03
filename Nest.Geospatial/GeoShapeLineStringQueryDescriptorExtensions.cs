using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeLineStringQueryDescriptorExtensions
    {
        public static GeoShapeLineStringQueryDescriptor<T> Coordinates<T>(
            this GeoShapeLineStringQueryDescriptor<T> descriptor, 
            ILineString lineString) where T : class
        {
            return descriptor.Coordinates(lineString.GetCoordinates());
        }

        public static GeoShapeLineStringQueryDescriptor<T> Boost<T>(
            this GeoShapeLineStringQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}