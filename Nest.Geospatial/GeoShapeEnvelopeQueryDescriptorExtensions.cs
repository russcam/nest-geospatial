using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for GeoShapeEnvelopeQueryDescriptor&lt;T&gt;
	/// </summary>
	public static class GeoShapeEnvelopeQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets the coordinates using the Envelope
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="envelope">the Envelope</param>
		/// <returns>the <see cref="GeoShapeEnvelopeQueryDescriptor{T}"/></returns>
        public static GeoShapeEnvelopeQueryDescriptor<T> Coordinates<T>(
            this GeoShapeEnvelopeQueryDescriptor<T> descriptor, 
            Envelope envelope) where T : class
        {
            return descriptor.Coordinates(envelope.NorthWestAndSouthEast());
        }

		/// <summary>
		/// Sets the boost
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="boost">the boost</param>
		/// <returns>the <see cref="GeoShapeEnvelopeQueryDescriptor{T}"/></returns>
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