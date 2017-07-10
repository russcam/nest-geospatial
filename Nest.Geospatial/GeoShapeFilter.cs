using System;
using System.Linq.Expressions;
using GeoAPI.Geometries;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;

namespace Nest.Geospatial
{
    public interface IGeoShapeFilter : IGeoShapeBaseFilter
    {
        [JsonProperty("shape")]
        [JsonConverter(typeof(GeometryConverter))]
        IGeometry Shape { get; set; }
    }

    public class GeoShapeFilter : PlainFilter, IGeoShapeFilter
    {
        bool IFilter.IsConditionless => this.Field == null || this.Shape == null;

        public PropertyPathMarker Field { get; set; }

        public IGeometry Shape { get; set; }

        public GeoShapeRelation? Relation { get; set; }

        protected override void WrapInContainer(IFilterContainer container) => 
            container.GeoShape = this;
    }

    public class GeoShapeFilterDescriptor<T> : FilterBase, IGeoShapeFilter where T : class
    {
        private IGeoShapeFilter Self => this;

        bool IFilter.IsConditionless => this.Self.Field == null || this.Self.Shape == null;

        PropertyPathMarker IFieldNameFilter.Field { get; set; }
        GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
        IGeometry IGeoShapeFilter.Shape { get; set; }

        public GeoShapeFilterDescriptor<T> OnField(string field)
        {
            this.Self.Field = field;
            return this;
        }

        public GeoShapeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
        {
            this.Self.Field = objectPath;
            return this;
        }

        public GeoShapeFilterDescriptor<T> Coordinates(IGeometry geometry)
        {
            this.Self.Shape = geometry;
            return this;
        }

        public GeoShapeFilterDescriptor<T> Relation(GeoShapeRelation relation)
        {
            this.Self.Relation = relation;
            return this;
        }
    }
}