using System;
using System.Linq.Expressions;
using GeoAPI.Geometries;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for QueryDescriptor&lt;T&gt;
	/// </summary>
	public static class QueryDescriptorExtensions
    {
        /// <summary>
        /// Query documents indexed using a geo_shape type.
        /// </summary>
        public static QueryContainer GeoShape<T>(
            this QueryDescriptor<T> queryDescriptor,
            Expression<Func<T, object>> expression,
            IGeometry geometry,
            string name = null,
            double? boost = null) where T : class
        {
            switch (geometry.OgcGeometryType)
            {
                case OgcGeometryType.Point:
                    var point = (IPoint)geometry;
                    return queryDescriptor.GeoShapePoint(
                        geo => geo
                            .Name(name)
                            .Boost(boost)
                            .OnField(expression)
                            .Coordinates(point));

                case OgcGeometryType.LineString:
                    var lineString = (ILineString)geometry;
                    return queryDescriptor.GeoShapeLineString(
                        geo => geo
                            .Name(name)
                            .Boost(boost)
                            .OnField(expression)
                            .Coordinates(lineString));

                case OgcGeometryType.Polygon:
                    var polygon = (IPolygon)geometry;
                    return queryDescriptor.GeoShapePolygon(
                        geo => geo
                            .Name(name)
                            .Boost(boost)
                            .OnField(expression)
                            .Coordinates(polygon));

                case OgcGeometryType.MultiPoint:
                    var multiPoint = (IMultiPoint)geometry;
                    return queryDescriptor.GeoShapeMultiPoint(
                        geo => geo
                            .Name(name)
                            .Boost(boost)
                            .OnField(expression)
                            .Coordinates(multiPoint));

                case OgcGeometryType.MultiLineString:
                    var multiLineString = (IMultiLineString)geometry;
                    return queryDescriptor.GeoShapeMultiLineString(
                        geo => geo
                            .Name(name)
                            .Boost(boost)
                            .OnField(expression)
                            .Coordinates(multiLineString));

                case OgcGeometryType.MultiPolygon:
                    var multiPolygon = (IMultiPolygon)geometry;
                    return queryDescriptor.GeoShapeMultiPolygon(
                        geo => geo
                            .Name(name)
                            .Boost(boost)
                            .OnField(expression)
                            .Coordinates(multiPolygon));

                default:
                    throw new NotSupportedException($"geometry '{geometry.GeometryType}' not supported");
            }
        }

        /// <summary>
        /// Query documents indexed using a geo_shape type.
        /// </summary>
        public static QueryContainer GeoShape<T>(
            this QueryDescriptor<T> queryDescriptor,
            string field,
            IGeometry geometry,
            string name = null,
            double? boost = null) where T : class
        {
            switch (geometry.OgcGeometryType)
            {
                case OgcGeometryType.Point:
                    var point = (IPoint)geometry;
                    return queryDescriptor.GeoShapePoint(geo => geo
                        .Name(name)
                        .Boost(boost)
                        .OnField(field)
                        .Coordinates(point)
					);

                case OgcGeometryType.LineString:
                    var lineString = (ILineString)geometry;
                    return queryDescriptor.GeoShapeLineString(geo => geo
                        .Name(name)
                        .Boost(boost)
                        .OnField(field)
                        .Coordinates(lineString)
					);

                case OgcGeometryType.Polygon:
                    var polygon = (IPolygon)geometry;
                    return queryDescriptor.GeoShapePolygon(geo => geo
                        .Name(name)
                        .Boost(boost)
                        .OnField(field)
                        .Coordinates(polygon)
					);

                case OgcGeometryType.MultiPoint:
                    var multiPoint = (IMultiPoint)geometry;
                    return queryDescriptor.GeoShapeMultiPoint(geo => geo
                        .Name(name)
                        .Boost(boost)
                        .OnField(field)
                        .Coordinates(multiPoint)
					);

                case OgcGeometryType.MultiLineString:
                    var multiLineString = (IMultiLineString)geometry;
                    return queryDescriptor.GeoShapeMultiLineString(geo => geo
                        .Name(name)
                        .Boost(boost)
                        .OnField(field)
                        .Coordinates(multiLineString)
					);

                case OgcGeometryType.MultiPolygon:
                    var multiPolygon = (IMultiPolygon)geometry;
                    return queryDescriptor.GeoShapeMultiPolygon(geo => geo
						.Name(name)
						.Boost(boost)
						.OnField(field)
						.Coordinates(multiPolygon)
					);

                default:
                    throw new NotSupportedException($"geometry '{geometry.GeometryType}' not supported");
            }
        }

        /// <summary>
        /// Query documents indexed using the circle geo_shape type.
        /// </summary>
        public static QueryContainer GeoShapeCircle<T>(
            this QueryDescriptor<T> queryDescriptor,
            Expression<Func<T, object>> expression,
            IPoint point,
            double distance,
            GeoPrecisionUnit unit,
            string name = null,
            double? boost = null) where T : class
        {
            return queryDescriptor.GeoShapeCircle(geo => geo
                .OnField(expression)
                .Name(name)
                .Boost(boost)
                .Coordinates(point.GetCoordinates())
                .Radius($"{distance}{unit.GetStringValue()}")
			);
        }

        /// <summary>
        /// Query documents indexed using the circle geo_shape type.
        /// </summary>
        public static QueryContainer GeoShapeCircle<T>(
            this QueryDescriptor<T> queryDescriptor,
            Expression<Func<T, object>> expression,
            IPoint point,
            string radius,
            string name = null,
            double? boost = null) where T : class
        {
            return queryDescriptor.GeoShapeCircle(geo => geo
                .OnField(expression)
                .Name(name)
                .Boost(boost)
                .Coordinates(point)
                .Radius(radius)
			);
        }

        /// <summary>
        /// Query documents indexed using the circle geo_shape type.
        /// </summary>
        public static QueryContainer GeoShapeCircle<T>(
            this QueryDescriptor<T> queryDescriptor,
            string field,
            IPoint point,
            double distance,
            GeoPrecisionUnit unit,
            string name = null,
            double? boost = null) where T : class
        {
            return queryDescriptor.GeoShapeCircle(geo => geo
                .OnField(field)
                .Name(name)
                .Boost(boost)
                .Coordinates(point)
                .Radius($"{distance}{unit.GetStringValue()}")
			);
        }

        /// <summary>
        /// Query documents indexed using the circle geo_shape type.
        /// </summary>
        public static QueryContainer GeoShapeCircle<T>(
            this QueryDescriptor<T> queryDescriptor,
            string field,
            IPoint point,
            string radius,
            string name = null,
            double? boost = null) where T : class
        {
            return queryDescriptor.GeoShapeCircle(geo => geo
                .OnField(field)
                .Name(name)
                .Boost(boost)
                .Coordinates(point)
                .Radius(radius)
			);
        }

        /// <summary>
        /// Query documents indexed using the envelope geo_shape type.
        /// </summary>
        public static QueryContainer GeoShapeEnvelope<T>(
            this QueryDescriptor<T> queryDescriptor,
            Expression<Func<T, object>> expression,
            Envelope envelope,
            string name = null,
            double? boost = null) where T : class
        {
            return queryDescriptor.GeoShapeEnvelope(geo => geo
                .OnField(expression)
                .Name(name)
                .Boost(boost)
                .Coordinates(envelope)
			);
        }

        /// <summary>
        /// Query documents indexed using the envelope geo_shape type.
        /// </summary>
        public static QueryContainer GeoShapeEnvelope<T>(
            this QueryDescriptor<T> queryDescriptor,
            string field,
            Envelope envelope,
            string name = null,
            double? boost = null) where T : class
        {
            return queryDescriptor.GeoShapeEnvelope(geo => geo
                .OnField(field)
                .Name(name)
                .Boost(boost)
                .Coordinates(envelope)
			);
        }
    }
}