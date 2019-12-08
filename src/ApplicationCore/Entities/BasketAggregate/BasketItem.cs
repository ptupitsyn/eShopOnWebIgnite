using System;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Guid CatalogItemId { get; set; }
        public Guid BasketId { get; set; }
    }
}
