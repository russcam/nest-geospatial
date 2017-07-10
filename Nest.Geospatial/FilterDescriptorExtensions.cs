using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GeoAPI.Geometries;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;

namespace Nest.Geospatial
{
    /// <summary>
    /// Extension methods for <see cref="FilterDescriptor{T}"/>
    /// </summary>
    public static class FilterDescriptorExtensions
    {
        /// <summary>
        ///     A filter allowing to filter hits based on a point location, using a bounding box
        /// </summary>
        public static FilterContainer GeoBoundingBox<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            Envelope envelope,
            GeoExecution? geoExecution = null) where T : class => 
            filterDescriptor.GeoBoundingBox(
                expression,
                envelope.MinX,
                envelope.MaxY,
                envelope.MaxX,
                envelope.MinY,
                geoExecution);

        /// <summary>
        ///     A filter allowing to filter hits based on a point location using a bounding box
        /// </summary>
        public static FilterContainer GeoBoundingBox<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            Envelope envelope,
            GeoExecution? geoExecution = null) where T : class => 
            filterDescriptor.GeoBoundingBox(
                field,
                envelope.MinX,
                envelope.MaxY,
                envelope.MaxX,
                envelope.MinY,
                geoExecution);

        /// <summary>
        ///     A filter allowing to include hits that only fall within a polygon of points.
        /// </summary>
        /// <remarks>
        ///     Only the exterior ring of the polygon is used
        /// </remarks>
        public static FilterContainer GeoPolygon<T>(
            this FilterDescriptor<T> filterDescriptor,
            Expression<Func<T, object>> expression,
            IPolygon polygon) where T : class => 
            filterDescriptor.GeoPolygon(expression, GetCoordinates(polygon.ExteriorRing));

        /// <summary>
        ///     A filter allowing to include hits that only fall within a polygon of points.
        /// </summary>
        /// <remarks>
        ///     Only the exterior ring of the polygon is used
        /// </remarks>
        public static FilterContainer GeoPolygon<T>(
            this FilterDescriptor<T> filterDescriptor,
            string field,
            IPolygon polygon) where T : class => 
            filterDescriptor.GeoPolygon(field, GetCoordinates(polygon.ExteriorRing));

        /// <summary>
        ///     Filter documents indexed using a geo_shape type.
        /// </summary>
        public static FilterContainer GeoShape<T>(
            this FilterDescriptor<T> filterDescriptor,
            Action<GeoShapeFilterDescriptor<T>> selector) where T : class
        {
            var descriptor = new GeoShapeFilterDescriptor<T>();
            selector?.Invoke(descriptor);
            IGeoShapeFilter filter = descriptor;
            SetCacheAndName(filterDescriptor, filter);

            return New(filterDescriptor, filter, f => f.GeoShape = filter);
        }

        private static FilterDescriptor<T> New<T>(FilterDescriptor<T> filterDescriptor, IFilter filter, Action<IFilterContainer> fillProperty)
            where T : class
        {
            IFilterContainer self = filterDescriptor;
            if (filter.IsConditionless && !self.IsVerbatim)
            {
                ResetCache(filterDescriptor);
                return CreateConditionlessFilterDescriptor(filterDescriptor, filter);
            }

            SetCacheAndName(filterDescriptor, filter);
            var f = new FilterDescriptor<T>();
            ((IFilterContainer)f).IsStrict = self.IsStrict;
            ((IFilterContainer)f).IsVerbatim = self.IsVerbatim;
            ((IFilterContainer)f).FilterName = self.FilterName;

            fillProperty?.Invoke(f);

            ResetCache(filterDescriptor);
            return f;
        }

        private static FilterDescriptor<T> CreateConditionlessFilterDescriptor<T>(FilterDescriptor<T> filterDescriptor, IFilter filter, string type = null) where T : class
        {
            IFilterContainer self = filterDescriptor;
            if (self.IsStrict && !self.IsVerbatim)
                throw new DslException(
                    "Filter resulted in a conditionless " +
                    $"'{type ?? filter.GetType().Name.Replace("Descriptor", "").Replace("`1", "")}' " +
                    "filter (json by approx):\n" +
                    $"{JsonConvert.SerializeObject(filterDescriptor, Formatting.Indented, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})}"
                );
            var f = new FilterDescriptor<T>();
            ((IFilterContainer)f).IsStrict = self.IsStrict;
            ((IFilterContainer)f).IsVerbatim = self.IsVerbatim;
            ((IFilterContainer)f).IsConditionless = true;
            return f;
        }

        private static void ResetCache(IFilterContainer filterContainer)
        {
            filterContainer.Cache = null;
            filterContainer.CacheKey = null;
            filterContainer.FilterName = null;
        }

        private static void SetCacheAndName(IFilterContainer filterDescriptor, IFilter filter)
        {
            filter.IsStrict = filterDescriptor.IsStrict;
            filter.IsVerbatim = filterDescriptor.IsVerbatim;

            if (filterDescriptor.Cache.HasValue)
                filter.Cache = filterDescriptor.Cache;
            if (!string.IsNullOrWhiteSpace(filterDescriptor.FilterName))
                filter.FilterName = filterDescriptor.FilterName;
            if (!string.IsNullOrWhiteSpace(filterDescriptor.CacheKey))
                filter.CacheKey = filterDescriptor.CacheKey;
        }

        private static IEnumerable<Tuple<double, double>> GetCoordinates(ILineString lineString) => 
            lineString.Coordinates.Select(c => Tuple.Create(c.X, c.Y));
    }
}