using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IgniteRepository<T> : IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        // ReSharper disable once StaticMemberInGenericType (intended)
        private static int _id;
        
        protected readonly IIgniteCacheAdapter<int, T> _cache;

        public IgniteRepository(IIgniteAdapter ignite)
        {
            _cache = ignite.GetCache<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _cache.GetAsync(id);
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            return _cache.ListAllAsync();
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
            // Generate ID
            // TODO: Use Ignite sequence
            entity.Id = Interlocked.Increment(ref _id);
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
            return SpecificationEvaluator<T>.GetQuery(_cache.AsQueryable(), spec);
        }
    }
}