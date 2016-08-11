using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeEnvelopeFilterDescriptor
	/// </summary>
	public static class GeoShapeEnvelopeFilterDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the Envelope
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="envelope">the Envelope</param>
		/// <returns>the <see cref="GeoShapeEnvelopeFilterDescriptor"/></returns>
		public static GeoShapeEnvelopeFilterDescriptor Coordinates(
            this GeoShapeEnvelopeFilterDescriptor descriptor, 
            Envelope envelope)
        {
            return descriptor.Coordinates(envelope.NorthWestAndSouthEast());
        }
    }
}