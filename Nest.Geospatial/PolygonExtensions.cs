using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Algorithm;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for Polygons
	/// </summary>
	public static class PolygonExtensions
	{
		/// <summary>
		/// Gets the coordinates for an <see cref="IPolygon"/>
		/// </summary>
		/// <param name="polygon">the Polygon</param>
		/// <returns>A collection of collections of coordinates</returns>
		public static IEnumerable<IEnumerable<IEnumerable<double>>> GetCoordinates(this IPolygon polygon)
		{
			if (polygon == null)
			{
				return Enumerable.Empty<IEnumerable<IEnumerable<double>>>();
			}

			var polygonCoordinates = new List<IEnumerable<IEnumerable<double>>>();
			var exteriorRingCoordinates = polygon.Shell.Coordinates;

			// outer ring should be counter clock-wise
			polygonCoordinates.Add(exteriorRingCoordinates.GetCoordinates());

			if (polygon.NumInteriorRings > 0)
			{
			    // inner rings should be clockwise
			    for (var index = 0; index < polygon.Holes.Length; index++)
			    {
			        var interiorRing = polygon.Holes[index];
			        var coordinates = interiorRing.Coordinates;
			        polygonCoordinates.Add(coordinates.GetCoordinates());
			    }
			}

			return polygonCoordinates;
		}
	}
}