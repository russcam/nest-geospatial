using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Algorithm;

namespace Nest.Geospatial
{
    public static class PolygonExtensions
    {
        public static IEnumerable<IEnumerable<IEnumerable<double>>> GetCoordinates(this IPolygon polygon)
        {
            var polygonCoordinates = new List<IEnumerable<IEnumerable<double>>>();

            var exteriorRingCoordinates = polygon.ExteriorRing.Coordinates;

            // outer ring should be counter clock-wise
            polygonCoordinates.Add(CGAlgorithms.IsCCW(exteriorRingCoordinates)
                ? exteriorRingCoordinates.GetCoordinates()
                : exteriorRingCoordinates.Reverse().GetCoordinates());

            if (polygon.NumInteriorRings > 0)
            {
                // inner rings should be clockwise
                foreach (var interiorRing in polygon.InteriorRings)
                {
                    var coordinates = interiorRing.Coordinates;

                    polygonCoordinates.Add(CGAlgorithms.IsCCW(interiorRing.Coordinates)
                        ? coordinates.Reverse().GetCoordinates()
                        : coordinates.GetCoordinates());
                }
            }

            return polygonCoordinates;
        }
    }
}