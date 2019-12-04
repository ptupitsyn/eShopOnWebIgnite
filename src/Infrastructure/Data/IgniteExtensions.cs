using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public static class IgniteExtensions
    {
        public static ICache<int, TEntity> GetCache<TEntity>(this IIgnite ignite) 
            where TEntity : BaseEntity, IAggregateRoot
        {
            // TODO: Sql config
            var cfg = new CacheConfiguration
            {
                Name = typeof(TEntity).FullName
            };
            
            return ignite.GetOrCreateCache<int, TEntity>(cfg);
        }

        public static IAsyncRepository<TEntity> GetRepo<TEntity>(this IIgnite ignite) 
            where TEntity : BaseEntity, IAggregateRoot
        {
            return new IgniteRepository<TEntity>(ignite);
        }
    }
}