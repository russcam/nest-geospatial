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
        /// <summary>
        /// The shape coordinates to use for querying
        /// </summary>
        [JsonProperty("shape")]
        [JsonConverter(typeof(GeometryConverter))]
        IGeometry Shape { get; set; }

        /// <summary>
        /// The boost to apply to the query
        /// </summary>
        [JsonProperty(PropertyName = "boost")]
        double? Boost { get; set; }
    }

    /// <summary>
    /// A geo_shape query
    /// </summary>
    public class GeoShapeQuery : PlainQuery, IGeoShapeQuery
    {
        internal static bool IsConditionless(IGeoShapeQuery query) => 
            query.Field == null || 
            string.IsNullOrEmpty(query.Name) && query.Field.Type == null ||
            query.Shape == null;

        PropertyPathMarker IFieldNameQuery.GetFieldName() => this.Field;

        void IFieldNameQuery.SetFieldName(string fieldName) => this.Field = fieldName;

        bool IQuery.IsConditionless => IsConditionless(this);

        /// <summary>
        /// The name for the query
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The field to query on
        /// </summary>
        public PropertyPathMarker Field { get; set; }

        /// <summary>
        /// The boost to apply to the query
        /// </summary>
        public double? Boost { get; set; }

        /// <summary>
        /// The shape coordinates to use for querying
        /// </summary>
        public IGeometry Shape { get; set; }

        /// <summary>
        /// Wraps the query in a <see cref="IQueryContainer"/>
        /// </summary>
        /// <param name="container"></param>
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

        bool IQuery.IsConditionless => GeoShapeQuery.IsConditionless(this);

        string IQuery.Name { get; set; }

        double? IGeoShapeQuery.Boost { get; set; }

        void IFieldNameQuery.SetFieldName(string fieldName) => Self.Field = fieldName;

        PropertyPathMarker IFieldNameQuery.GetFieldName() => Self.Field;

        /// <summary>
        /// The name for the query
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>the <see cref="GeoShapeQueryDescriptor{T}"/> instance</returns>
        public GeoShapeQueryDescriptor<T> Name(string name)
        {
            Self.Name = name;
            return this;
        }

        /// <summary>
        /// The field to query on
        /// </summary>
        /// <param name="field">The field</param>
        /// <returns>the <see cref="GeoShapeQueryDescriptor{T}"/> instance</returns>
        public GeoShapeQueryDescriptor<T> OnField(string field)
        {
            Self.Field = field;
            return this;
        }

        /// <summary>
        /// The field to query on
        /// </summary>
        /// <param name="objectPath">The expression to the field</param>
        /// <returns>the <see cref="GeoShapeQueryDescriptor{T}"/> instance</returns>
        public GeoShapeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
        {
            Self.Field = objectPath;
            return this;
        }

        /// <summary>
        /// The coordinates to use within the query, using the given <see cref="IGeometry"/>
        /// </summary>
        /// <param name="geometry">the geometry from which to define the coordinates</param>
        /// <returns>the <see cref="GeoShapeQueryDescriptor{T}"/> instance</returns>
        public GeoShapeQueryDescriptor<T> Coordinates(IGeometry geometry)
        {
            Self.Shape = geometry;
            return this;
        }

        /// <summary>
        /// Apply a boost to the query
        /// </summary>
        /// <param name="boost">The boost</param>
        /// <returns>the <see cref="GeoShapeQueryDescriptor{T}"/> instance</returns>
        public GeoShapeQueryDescriptor<T> Boost(double boost)
        {
            Self.Boost = boost;
            return this;
        }
    }
}