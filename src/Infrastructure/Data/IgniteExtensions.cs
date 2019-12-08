using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Apache.Ignite.Core.Cache.Configuration;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public static class IgniteExtensions
    {
        public static IIgniteCacheAdapter<TK, TV> GetCache<TK, TV>(this IIgniteAdapter ignite) 
        {
            var cfg = new CacheConfiguration
            {
                Name = typeof(TV).FullName,
                QueryEntities = new[]
                {
                    new QueryEntity(typeof(TK), typeof(TV))
                    {
                        Fields = GetFields<TV>().ToArray()
                    }
                },
                SqlEscapeAll = true
            };
            
            return ignite.GetOrCreateCache<TK, TV>(cfg);
        }

        public static IAsyncRepository<TEntity> GetRepo<TEntity>(this IIgniteAdapter ignite) 
            where TEntity : BaseEntity, IAggregateRoot
        {
            return new IgniteRepository<TEntity>(ignite);
        }
        
        private static IEnumerable<QueryField> GetFields<T>()
        {
            return GetSelfAndBaseTypes(typeof(T))
                .SelectMany(t => t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                .Select(p => new QueryField(p.Name, p.PropertyType));
        }

        /// <summary>
        /// Returns full type hierarchy.
        /// </summary>
        private static IEnumerable<Type> GetSelfAndBaseTypes(Type type)
        {
            while (type != typeof(object) && type != null)
            {
                yield return type;

                type = type.BaseType;
            }
        }
    }
}