using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<int> GetBasketItemCountAsync(string userName);
        Task TransferBasketAsync(string anonymousId, string userName);
        Task AddItemToBasket(Guid basketId, Guid catalogItemId, decimal price, int quantity = 1);
        Task SetQuantities(Guid basketId, Dictionary<string, int> quantities);
        Task DeleteBasketAsync(Guid basketId);
    }
}
