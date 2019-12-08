using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class OrderRepository : IgniteRepository<Order>, IOrderRepository
    {
        public OrderRepository(IIgniteAdapter ignite) : base(ignite)
        {
        }

        public Task<Order> GetByIdWithItemsAsync(Guid id)
        {
            // TODO: Load OrderItems and ItemOrdered from passed Ignite instance
            // TODO: Do we even need this? We can store Orders as full objects in a single table
            // See how usages work. Tests are already green :)
            var order = GetByIdAsync(id);
            return order;

//            return _dbContext.Orders
//                .Include(o => o.OrderItems)
//                .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
//                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
