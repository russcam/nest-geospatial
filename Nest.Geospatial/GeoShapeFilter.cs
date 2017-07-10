using System;
using System.Linq.Expressions;
using GeoAPI.Geometries;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;

namespace Nest.Geospatial
{
    /// <summary>
    /// A geo_shape query filter
    /// </summary>
    public interface IGeoShapeFilter : IGeoShapeBaseFilter
    {
        /// <summary>
        /// The shape coordinates to use for filtering
        /// </summary>
        [JsonProperty("shape")]
        [JsonConverter(typeof(GeometryConverter))]
        IGeometry Shape { get; set; }
    }

    /// <summary>
    /// A geo_shape query filter
    /// </summary>
    public class GeoShapeFilter : PlainFilter, IGeoShapeFilter
    {
        internal static bool IsConditionless(IGeoShapeFilter filter)
            => filter.Field == null || filter.Shape == null;

        bool IFilter.IsConditionless => IsConditionless(this);

        /// <summary>
        /// The field to filter on
        /// </summary>
        public PropertyPathMarker Field { get; set; }

        /// <summary>
        /// The shape coordinates to use for filtering
        /// </summary>
        public IGeometry Shape { get; set; }

        /// <summary>
        /// The relation for the filter
        /// </summary>
        public GeoShapeRelation? Relation { get; set; }

        /// <summary>
        /// Wraps the filter in a <see cref="IFilterContainer"/>
        /// </summary>
        /// <param name="container"></param>
        protected override void WrapInContainer(IFilterContainer container) => 
            container.GeoShape = this;
    }

    /// <summary>
    /// A descriptor for a geo_shape query filter
    /// </summary>
    public class GeoShapeFilterDescriptor<T> : FilterBase, IGeoShapeFilter where T : class
    {
        private IGeoShapeFilter Self => this;

        bool IFilter.IsConditionless => GeoShapeFilter.IsConditionless(this);

        PropertyPathMarker IFieldNameFilter.Field { get; set; }

        GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }

        IGeometry IGeoShapeFilter.Shape { get; set; }

        /// <summary>
        /// The field to filter on
        /// </summary>
        /// <param name="field">The field</param>
        /// <returns>the <see cref="GeoShapeFilterDescriptor{T}"/> instance</returns>
        public GeoShapeFilterDescriptor<T> OnField(string field)
        {
            this.Self.Field = field;
            return this;
        }

        /// <summary>
        /// The field to filter on
        /// </summary>
        /// <param name="objectPath">The expression to the field</param>
        /// <returns>the <see cref="GeoShapeFilterDescriptor{T}"/> instance</returns>
        public GeoShapeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
        {
            this.Self.Field = objectPath;
            return this;
        }

        /// <summary>
        /// The coordinates to use within the filter, using the given <see cref="IGeometry"/>
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public GeoShapeFilterDescriptor<T> Coordinates(IGeometry geometry)
        {
            this.Self.Shape = geometry;
            return this;
        }

        /// <summary>
        /// The relation for the filter
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public GeoShapeFilterDescriptor<T> Relation(GeoShapeRelation relation)
        {
            this.Self.Relation = relation;
            return this;
        }
    }
}