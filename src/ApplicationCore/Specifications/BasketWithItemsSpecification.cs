using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public sealed class BasketWithItemsSpecification : BaseSpecification<Basket>
    {
        public BasketWithItemsSpecification(Guid basketId)
            :base(b => b.Id == basketId)
        {
            // No-op.
        }
        public BasketWithItemsSpecification(string buyerId)
            :base(b => b.BuyerId == buyerId)
        {
            // No-op.
        }
    }
}
