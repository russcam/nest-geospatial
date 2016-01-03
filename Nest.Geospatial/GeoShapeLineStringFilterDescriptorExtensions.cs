using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeLineStringFilterDescriptorExtensions
    {
        public static GeoShapeLineStringFilterDescriptor Coordinates(
            this GeoShapeLineStringFilterDescriptor descriptor, 
            ILineString lineString)
        {
            return descriptor.Coordinates(lineString.GetCoordinates());
        }
    }
}