using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class GeoShapeEnvelopeQueryDescriptorExtensions
    {
        public static GeoShapeEnvelopeQueryDescriptor<T> Coordinates<T>(
            this GeoShapeEnvelopeQueryDescriptor<T> descriptor, 
            Envelope envelope) where T : class
        {
            return descriptor.Coordinates(envelope.NorthWestAndSouthEast());
        }

        public static GeoShapeEnvelopeQueryDescriptor<T> Boost<T>(
            this GeoShapeEnvelopeQueryDescriptor<T> descriptor, 
            double? boost) where T : class
        {
            return boost.HasValue
                ? descriptor.Boost(boost.Value)
                : descriptor;
        }
    }
}