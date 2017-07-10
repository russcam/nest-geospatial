using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    /// <summary>
    /// Extension methods for <see cref="GeoShapeEnvelopeQueryDescriptor{T}"/>
    /// </summary>
    public static class GeoShapeEnvelopeQueryDescriptorExtensions
    {
		/// <summary>
		/// Sets coordinates using an <see cref="Envelope"/>
		/// </summary>
		/// <typeparam name="T">the document type</typeparam>
		/// <param name="descriptor">the descriptor</param>
		/// <param name="envelope">the envelope</param>
		/// <returns>the <see cref="GeoShapeEnvelopeQueryDescriptor{T}"/></returns>
        public static GeoShapeEnvelopeQueryDescriptor<T> Coordinates<T>(
            this GeoShapeEnvelopeQueryDescriptor<T> descriptor, 
            Envelope envelope) where T : class => descriptor.Coordinates(envelope.NorthWestAndSouthEast());
    }
}