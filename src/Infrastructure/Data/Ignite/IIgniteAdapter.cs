using Apache.Ignite.Core.Cache.Configuration;

namespace Microsoft.eShopWeb.Infrastructure.Data.Ignite
{
    public interface IIgniteAdapter
    {
        IIgniteCacheAdapter<TK, TV> GetOrCreateCache<TK, TV>(CacheConfiguration cfg);
    }
}