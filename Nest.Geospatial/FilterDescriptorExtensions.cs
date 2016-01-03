using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
    /// <summary>
    /// Extension methods for <see cref="FilterDescriptor{T}"/>
    /// </summary>
    public static class FilterDescriptorExtensions
    {
        /// <summary>
        ///     A filter allowing to filter hits based on a point location using a bounding box
        /// </summary>
        public static FilterContainer GeoBoundingBox<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            Envelope envelope,
            GeoExecution? geoExecution = null) where T : class
        {
            return filterDescriptor.GeoBoundingBox(
                expression,
                envelope.MinX,
                envelope.MaxY,
                envelope.MaxX,
                envelope.MinY,
                geoExecution);
        }

        /// <summary>
        ///     A filter allowing to filter hits based on a point location using a bounding box
        /// </summary>
        public static FilterContainer GeoBoundingBox<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            Envelope envelope,
            GeoExecution? geoExecution = null) where T : class
        {
            return filterDescriptor.GeoBoundingBox(
                field,
                envelope.MinX,
                envelope.MaxY,
                envelope.MaxX,
                envelope.MinY,
                geoExecution);
        }

        /// <summary>
        ///     A filter allowing to include hits that only fall within a polygon of points.
        /// </summary>
        /// <remarks>
        ///     Only the exterior ring of the polygon is used
        /// </remarks>
        public static FilterContainer GeoPolygon<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            IPolygon polygon) where T : class
        {
            return filterDescriptor.GeoPolygon(expression, GetCoordinates(polygon.ExteriorRing));
        }

        /// <summary>
        ///     A filter allowing to include hits that only fall within a polygon of points.
        /// </summary>
        /// <remarks>
        ///     Only the exterior ring of the polygon is used
        /// </remarks>
        public static FilterContainer GeoPolygon<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            IPolygon polygon) where T : class
        {
            return filterDescriptor.GeoPolygon(field, GetCoordinates(polygon.ExteriorRing));
        }

        /// <summary>
        ///     Filter documents indexed using a geo_shape type.
        /// </summary>
        public static FilterContainer GeoShape<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            IGeometry geometry,
            GeoShapeRelation relation) where T : class
        {
            switch (geometry.OgcGeometryType)
            {
                case OgcGeometryType.Point:
                    var point = (IPoint)geometry;
                    return filterDescriptor.GeoShapePoint(
                        expression,
                        geo => geo.Coordinates(point).Relation(relation));

                case OgcGeometryType.LineString:
                    var lineString = (ILineString)geometry;
                    return filterDescriptor.GeoShapeLineString(
                        expression,
                        geo => geo.Coordinates(lineString).Relation(relation));

                case OgcGeometryType.Polygon:
                    var polygon = (IPolygon)geometry;
                    return filterDescriptor.GeoShapePolygon(
                        expression,
                        geo => geo.Coordinates(polygon).Relation(relation));

                case OgcGeometryType.MultiPoint:
                    var multiPoint = (IMultiPoint)geometry;
                    return filterDescriptor.GeoShapeMultiPoint(
                        expression,
                        geo => geo.Coordinates(multiPoint).Relation(relation));

                case OgcGeometryType.MultiLineString:
                    var multiLineString = (IMultiLineString)geometry;
                    return filterDescriptor.GeoShapeMultiLineString(
                        expression,
                        geo => geo.Coordinates(multiLineString).Relation(relation));

                case OgcGeometryType.MultiPolygon:
                    var multiPolygon = (IMultiPolygon)geometry;
                    return filterDescriptor.GeoShapeMultiPolygon(
                        expression,
                        geo => geo.Coordinates(multiPolygon).Relation(relation));

                default:
                    throw new NotSupportedException($"geometry '{geometry.GeometryType}' not supported");
            }
        }

        /// <summary>
        ///     Filter documents indexed using a geo_shape type.
        /// </summary>
        public static FilterContainer GeoShape<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            IGeometry geometry,
            GeoShapeRelation relation) where T : class
        {
            switch (geometry.OgcGeometryType)
            {
                case OgcGeometryType.Point:
                    var point = (IPoint)geometry;
                    return filterDescriptor.GeoShapePoint(
                        field,
                        geo => geo.Coordinates(point).Relation(relation));

                case OgcGeometryType.LineString:
                    var lineString = (ILineString)geometry;
                    return filterDescriptor.GeoShapeLineString(
                        field,
                        geo => geo.Coordinates(lineString).Relation(relation));

                case OgcGeometryType.Polygon:
                    var polygon = (IPolygon)geometry;
                    return filterDescriptor.GeoShapePolygon(
                        field,
                        geo => geo.Coordinates(polygon).Relation(relation));

                case OgcGeometryType.MultiPoint:
                    var multiPoint = (IMultiPoint)geometry;
                    return filterDescriptor.GeoShapeMultiPoint(
                        field,
                        geo => geo.Coordinates(multiPoint).Relation(relation));

                case OgcGeometryType.MultiLineString:
                    var multiLineString = (IMultiLineString)geometry;
                    return filterDescriptor.GeoShapeMultiLineString(
                        field,
                        geo => geo.Coordinates(multiLineString).Relation(relation));

                case OgcGeometryType.MultiPolygon:
                    var multiPolygon = (IMultiPolygon)geometry;
                    return filterDescriptor.GeoShapeMultiPolygon(
                        field,
                        geo => geo.Coordinates(multiPolygon).Relation(relation));

                default:
                    throw new NotSupportedException($"geometry '{geometry.GeometryType}' not supported");
            }
        }

        /// <summary>
        ///     Filter documents indexed using the circle geo_shape type.
        /// </summary>
        public static FilterContainer GeoShapeCircle<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            IPoint point,
            double distance,
            GeoPrecisionUnit unit,
            GeoShapeRelation relation) where T : class
        {
            return filterDescriptor.GeoShapeCircle(
                expression,
                geo => geo
                    .Coordinates(point)
                    .Radius($"{distance}{unit.GetStringValue()}")
                    .Relation(relation));
        }

        /// <summary>
        ///     Filter documents indexed using the circle geo_shape type.
        /// </summary>
        public static FilterContainer GeoShapeCircle<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            IPoint point,
            string radius,
            GeoShapeRelation relation) where T : class
        {
            return filterDescriptor.GeoShapeCircle(
                expression,
                geo => geo
                    .Coordinates(point)
                    .Radius(radius)
                    .Relation(relation));
        }

        /// <summary>
        ///     Filter documents indexed using the circle geo_shape type.
        /// </summary>
        public static FilterContainer GeoShapeCircle<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            IPoint point,
            double distance,
            GeoPrecisionUnit unit,
            GeoShapeRelation relation) where T : class
        {
            return filterDescriptor.GeoShapeCircle(
                field,
                geo => geo
                    .Coordinates(point)
                    .Radius($"{distance}{unit.GetStringValue()}")
                    .Relation(relation));
        }

        /// <summary>
        ///     Filter documents indexed using the circle geo_shape type.
        /// </summary>
        public static FilterContainer GeoShapeCircle<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            IPoint point,
            string radius,
            GeoShapeRelation relation) where T : class
        {
            return filterDescriptor.GeoShapeCircle(
                field,
                geo => geo
                    .Coordinates(point)
                    .Radius(radius)
                    .Relation(relation));
        }

        /// <summary>
        ///     Filter documents indexed using the envelope geo_shape type.
        /// </summary>
        public static FilterContainer GeoShapeEnvelope<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            Envelope envelope,
            GeoShapeRelation relation) where T : class
        {
            return filterDescriptor.GeoShapeEnvelope(
                expression,
                geo => geo
                    .Coordinates(envelope)
                    .Relation(relation));
        }

        /// <summary>
        ///     Filter documents indexed using the envelope geo_shape type.
        /// </summary>
        public static FilterContainer GeoShapeEnvelope<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            Envelope envelope,
            GeoShapeRelation relation) where T : class
        {
            return filterDescriptor.GeoShapeEnvelope(
                field,
                geo => geo
                    .Coordinates(envelope)
                    .Relation(relation));
        }

        private static IEnumerable<Tuple<double, double>> GetCoordinates(ILineString lineString)
        {
            return lineString.Coordinates.Select(c => Tuple.Create(c.X, c.Y));
        }
    }
}