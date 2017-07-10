using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for <see cref="Envelope"/>
	/// </summary>
    public static class EnvelopeExtensions
    {
		/// <summary>
		/// Gets the North West coordinates of an <see cref="Envelope"/>
		/// </summary>
		/// <param name="envelope">the Envelope</param>
		/// <returns>The coordinates</returns>
        public static IEnumerable<double> NorthWest(this Envelope envelope)
        {
			return envelope == null
				? Enumerable.Empty<double>()
				: new[] { envelope.MinX, envelope.MaxY };
        }

		/// <summary>
		/// Gets the South East coordinates of an <see cref="Envelope"/>
		/// </summary>
		/// <param name="envelope">the Envelope</param>
		/// <returns>The coordinates</returns>
        public static IEnumerable<double> SouthEast(this Envelope envelope)
        {
	        return envelope == null 
				? Enumerable.Empty<double>() 
				: new[] { envelope.MaxX, envelope.MinY };
        }

	    /// <summary>
		/// Gets the North West and South East coordinates of an <see cref="Envelope"/>
		/// </summary>
		/// <param name="envelope">the Envelope</param>
		/// <returns>A collection of coordinates</returns>
        public static IEnumerable<IEnumerable<double>> NorthWestAndSouthEast(this Envelope envelope)
		{
			return envelope == null 
				? Enumerable.Empty<IEnumerable<double>>() 
				: new[] { envelope.NorthWest(), envelope.SouthEast() };
		}
    }
}