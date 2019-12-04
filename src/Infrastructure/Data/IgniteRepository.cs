using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Apache.Ignite.Linq;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IgniteRepository<T> : IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly ICache<int, T> _cache;

        public IgniteRepository(IIgnite ignite)
        {
            // TODO: Sql config
            var cfg = new CacheConfiguration
            {
                Name = typeof(T).FullName
            };
            _cache = ignite.GetOrCreateCache<int, T>(cfg);
        }
        
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _cache.GetAsync(id);
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            return Task.FromResult((IReadOnlyList<T>) _cache.ToList());
        }

        public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return Task.FromResult((IReadOnlyList<T>) ApplySpecification(spec).ToList());
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            return Task.FromResult(ApplySpecification(spec).Count());
        }

        public async Task<T> AddAsync(T entity)
        {
            await _cache.PutAsync(entity.Id, entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            await _cache.PutAsync(entity.Id, entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _cache.RemoveAsync(entity.Id);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_cache.AsCacheQueryable().Select(e => e.Value), spec);
        }
    }
}