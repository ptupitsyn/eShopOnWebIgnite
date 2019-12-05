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
            // TODO: Sql config
            var cfg = new CacheConfiguration
            {
                Name = typeof(TV).FullName
            };
            
            return ignite.GetOrCreateCache<TK, TV>(cfg);
        }

        public static IAsyncRepository<TEntity> GetRepo<TEntity>(this IIgniteAdapter ignite) 
            where TEntity : BaseEntity, IAggregateRoot
        {
            return new IgniteRepository<TEntity>(ignite);
        }
    }
}