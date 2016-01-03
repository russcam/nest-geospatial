using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeEnvelopeFilterDescriptorExtensions
    {
        public static GeoShapeEnvelopeFilterDescriptor Coordinates(
            this GeoShapeEnvelopeFilterDescriptor descriptor, 
            Envelope envelope)
        {
            return descriptor.Coordinates(envelope.NorthWestAndSouthEast());
        }
    }
}