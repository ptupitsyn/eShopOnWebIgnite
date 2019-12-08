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

        public async Task<TV> GetAsync(TK id)
        {
            var res = await _cache.TryGetAsync(id);
            return res.Value;
        }

        public Task<IReadOnlyList<TV>> ListAllAsync()
        {
            return Task.FromResult((IReadOnlyList<TV>) _cache.Select(e => e.Value).ToList());
        }

        public Task PutAsync(TK key, TV val)
        {
            return _cache.PutAsync(key, val);
        }

        public Task PutAllAsync(IEnumerable<KeyValuePair<TK, TV>> pairs)
        {
            return _cache.PutAllAsync(pairs);
        }

        public Task<bool> RemoveAsync(TK key)
        {
            return _cache.RemoveAsync(key);
        }

        public IQueryable<TV> AsQueryable()
        {
            return _cache.AsCacheQueryable().Select(e => e.Value);
        }

        public long GetSize()
        {
            return _cache.GetSize();
        }
    }
}