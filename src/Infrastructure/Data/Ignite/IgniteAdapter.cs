using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache.Configuration;

namespace Microsoft.eShopWeb.Infrastructure.Data.Ignite
{
    public class IgniteAdapter : IIgniteAdapter
    {
        private readonly IIgnite _ignite;

        public IgniteAdapter(IIgnite ignite)
        {
            _ignite = ignite;
        }

        public IIgniteCacheAdapter<TK, TV> GetOrCreateCache<TK, TV>(CacheConfiguration cfg)
        {
            var cache = _ignite.GetOrCreateCache<TK, TV>(cfg);
            return new IgniteCacheAdapter<TK, TV>(cache);
        }

        public void Dispose()
        {
            _ignite?.Dispose();
        }
    }
}