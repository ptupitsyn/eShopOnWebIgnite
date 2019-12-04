using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Apache.Ignite.Core;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class OrderRepository : IgniteRepository<Order>, IOrderRepository
    {
        public OrderRepository(IIgnite ignite) : base(ignite)
        {
        }

        public Task<Order> GetByIdWithItemsAsync(int id)
        {
            // TODO: Load OrderItems and ItemOrdered from passed Ignite instance
            var order = GetByIdAsync(id);
            return order;

//            return _dbContext.Orders
//                .Include(o => o.OrderItems)
//                .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
//                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
