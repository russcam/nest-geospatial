using System.Collections.Generic;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    public static class EnvelopeExtensions
    {
        public static IEnumerable<double> NorthWest(this Envelope envelope)
        {
            return new[] { envelope.MinX, envelope.MaxY };
        }

        public static IEnumerable<double> SouthEast(this Envelope envelope)
        {
            return new[] { envelope.MaxX, envelope.MinY };
        }

        public static IEnumerable<IEnumerable<double>> NorthWestAndSouthEast(this Envelope envelope)
        {
            return new[] { envelope.NorthWest(), envelope.SouthEast() };
        } 
    }
}