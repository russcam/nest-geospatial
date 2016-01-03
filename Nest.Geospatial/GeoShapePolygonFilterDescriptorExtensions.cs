using System.Runtime.Remoting.Messaging;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapePolygonFilterDescriptorExtensions
    {
        public static GeoShapePolygonFilterDescriptor Coordinates(
            this GeoShapePolygonFilterDescriptor descriptor, 
            IPolygon polygon)
        {
            return descriptor.Coordinates(polygon.GetCoordinates());
        }
    }
}