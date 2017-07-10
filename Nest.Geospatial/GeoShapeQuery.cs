using System;
using System.Linq.Expressions;
using GeoAPI.Geometries;
using Nest.DSL.Query.Behaviour;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;

namespace Nest.Geospatial
{
    /// <summary>
    /// A geo_shape query
    /// </summary>
    public interface IGeoShapeQuery : Nest.IGeoShapeQuery
    {
        [JsonProperty("shape")]
        [JsonConverter(typeof(GeometryConverter))]
        IGeometry Shape { get; set; }

        [JsonProperty(PropertyName = "boost")]
        double? Boost { get; set; }
    }

    /// <summary>
    /// A geo_shape query
    /// </summary>
    public class GeoShapeQuery : PlainQuery, IGeoShapeQuery
    {
        public string Name { get; set; }

        public PropertyPathMarker Field { get; set; }

        public double? Boost { get; set; }

        public IGeometry Shape { get; set; }

        bool IQuery.IsConditionless => this.Field == null || 
                                       (string.IsNullOrEmpty(this.Name) && this.Field.Type == null) || 
                                       this.Shape == null;

        PropertyPathMarker IFieldNameQuery.GetFieldName() => this.Field;

        void IFieldNameQuery.SetFieldName(string fieldName) => this.Field = fieldName;

        protected override void WrapInContainer(IQueryContainer container) =>
            container.GeoShape = this;
    }

    /// <summary>
    /// A descriptor for a geo_shape query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GeoShapeQueryDescriptor<T> : IGeoShapeQuery where T : class
    {
        private IGeoShapeQuery Self => this;

        PropertyPathMarker Nest.IGeoShapeQuery.Field { get; set; }
        IGeometry IGeoShapeQuery.Shape { get; set; }
        bool IQuery.IsConditionless => Self.Field == null || (string.IsNullOrEmpty(Self.Name) && Self.Field.Type == null) || Self.Shape == null;
        string IQuery.Name { get; set; }
        double? IGeoShapeQuery.Boost { get; set; }

        void IFieldNameQuery.SetFieldName(string fieldName)
        {
            Self.Field = fieldName;
        }

        PropertyPathMarker IFieldNameQuery.GetFieldName()
        {
            return Self.Field;
        }

        public GeoShapeQueryDescriptor<T> Name(string name)
        {
            Self.Name = name;
            return this;
        }

        public GeoShapeQueryDescriptor<T> OnField(string field)
        {
            Self.Field = field;
            return this;
        }

        public GeoShapeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
        {
            Self.Field = objectPath;
            return this;
        }

        /// <summary>
        /// Sets the coordinates using the given <see cref="IGeometry"/>
        /// </summary>
        /// <param name="geometry">the geometry from which to define the coordinates</param>
        /// <returns>the <see cref="GeoShapeQueryDescriptor{T}"/></returns>
        public GeoShapeQueryDescriptor<T> Coordinates(IGeometry geometry)
        {
            Self.Shape = geometry;
            return this;
        }

        public GeoShapeQueryDescriptor<T> Boost(double boost)
        {
            Self.Boost = boost;
            return this;
        }
    }
}