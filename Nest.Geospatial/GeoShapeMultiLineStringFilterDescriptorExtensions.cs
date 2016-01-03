using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeMultiLineStringFilterDescriptorExtensions
    {
        public static GeoShapeMultiLineStringFilterDescriptor Coordinates(
            this GeoShapeMultiLineStringFilterDescriptor descriptor, 
            IMultiLineString multiLineString)
        {
            return descriptor.Coordinates(multiLineString.GetCoordinates());
        }
    }
}