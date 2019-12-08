using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CustomerOrdersWithItemsSpecification : BaseSpecification<Order>
    {
        public CustomerOrdersWithItemsSpecification(string buyerId)
            : base(o => o.BuyerId == buyerId)
        {
            // No-op.
        }
    }
}
