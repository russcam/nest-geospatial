using System.Collections;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapePointFilterDescriptorExtensions
    {
        public static GeoShapePointFilterDescriptor Coordinates(this GeoShapePointFilterDescriptor descriptor, IPoint point)
        {
            return descriptor.Coordinates(point.GetCoordinates());
        }
    }
}