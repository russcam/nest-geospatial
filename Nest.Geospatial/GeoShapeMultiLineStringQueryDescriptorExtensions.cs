using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeMultiLineStringQueryDescriptorExtensions
    {
        public static GeoShapeMultiLineStringQueryDescriptor<T> Coordinates<T>(
            this GeoShapeMultiLineStringQueryDescriptor<T> descriptor, 
            IMultiLineString multiLineString) where T : class
        {
            return descriptor.Coordinates(multiLineString.GetCoordinates());
        }

        public static GeoShapeMultiLineStringQueryDescriptor<T> Boost<T>(
            this GeoShapeMultiLineStringQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}