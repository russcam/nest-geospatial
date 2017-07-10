using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    /// <summary>
    /// Extension methods for <see cref="GeoShapeEnvelopeFilterDescriptor"/>
    /// </summary>
    public static class GeoShapeEnvelopeFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the Envelope
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="envelope">the envelope</param>
		/// <returns>the <see cref="GeoShapeEnvelopeFilterDescriptor"/></returns>
		public static GeoShapeEnvelopeFilterDescriptor Coordinates(
            this GeoShapeEnvelopeFilterDescriptor descriptor, 
            Envelope envelope) => descriptor.Coordinates(envelope.NorthWestAndSouthEast());
    }
}