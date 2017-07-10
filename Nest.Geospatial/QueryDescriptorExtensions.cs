using System;
using System.Linq.Expressions;
using GeoAPI.Geometries;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers.Converters;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;

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
            Action<GeoShapeQueryDescriptor<T>> selector) where T : class
        {
            var query = new GeoShapeQueryDescriptor<T>();
            selector(query);

            if (queryDescriptor.IsConditionless && !queryDescriptor.IsVerbatim)
                return CreateConditionlessQueryDescriptor(queryDescriptor, query);

            return SetGeoShapeQuery(queryDescriptor, query);
        }

        private static QueryContainer SetGeoShapeQuery<T>(QueryDescriptor<T> queryDescriptor,
            IGeoShapeQuery query) where T : class
        {
            var descriptor = new QueryDescriptor<T>();
            ((IQueryContainer)descriptor).IsStrict = queryDescriptor.IsStrict;
            ((IQueryContainer)descriptor).IsVerbatim = queryDescriptor.IsVerbatim;
            ((IQueryContainer)descriptor).GeoShape = query;

            return descriptor;
        }

        private static QueryDescriptor<T> CreateConditionlessQueryDescriptor<T>(QueryDescriptor<T> queryDescriptor, IQuery query) where T : class
        {
            if (queryDescriptor.IsStrict && !queryDescriptor.IsVerbatim)
                throw new DslException("Query resulted in a conditionless " +
                                       $"{query.GetType().Name.Replace("Descriptor", "").Replace("`1", "")} " +
                                       "query (json by approx):\n" +
                                       $"{JsonConvert.SerializeObject(queryDescriptor, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })}");

            var q = new QueryDescriptor<T>();
            ((IQueryContainer)q).IsConditionless = true;
            return q;
        }
    }
}