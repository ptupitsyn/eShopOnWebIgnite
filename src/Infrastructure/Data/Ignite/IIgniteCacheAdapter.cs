using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data.Ignite
{
    public interface IIgniteCacheAdapter<TK, TV>
    {
        Task<TV> GetAsync(TK id);

        Task<IReadOnlyList<TV>> ListAllAsync();
        
        Task PutAsync(TK key, TV val);
        
        Task<bool> RemoveAsync(TK key);
        
        IQueryable<TV> AsQueryable();
    }
}