using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Linq;

namespace Microsoft.eShopWeb.Infrastructure.Data.Ignite
{
    public class IgniteCacheAdapter<TK, TV> : IIgniteCacheAdapter<TK, TV>
    {
        private readonly ICache<TK, TV> _cache;

        public IgniteCacheAdapter(ICache<TK, TV> cache)
        {
            _cache = cache;
        }

        public Task<TV> GetAsync(TK id)
        {
            return _cache.GetAsync(id);
        }

        public Task<IReadOnlyList<TV>> ListAllAsync()
        {
            return Task.FromResult((IReadOnlyList<TV>) _cache.Select(e => e.Value).ToList());
        }

        public Task PutAsync(TK key, TV val)
        {
            return _cache.PutAsync(key, val);
        }

        public Task<bool> RemoveAsync(TK key)
        {
            return _cache.RemoveAsync(key);
        }

        public IQueryable<TV> AsQueryable()
        {
            return _cache.AsCacheQueryable().Select(e => e.Value);
        }
    }
}