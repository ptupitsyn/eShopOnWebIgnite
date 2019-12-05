using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;
using Microsoft.eShopWeb.UnitTests.Builders;
using Xunit;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.BasketRepositoryTests
{
    public class SetQuantities : IDisposable
    {
        private readonly IIgniteAdapter _ignite;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly BasketBuilder _basketBuilder = new BasketBuilder();

        public SetQuantities()
        {
            _ignite = TestUtils.GetIgnite();
            _basketRepository = _ignite.GetRepo<Basket>();
        }

        [Fact]
        public async Task RemoveEmptyQuantities()
        {
            var basket = _basketBuilder.WithOneBasketItem();
            var basketService = new BasketService(_basketRepository, null);
            await _basketRepository.AddAsync(basket);

            await basketService.SetQuantities(_basketBuilder.BasketId, new Dictionary<string, int>() { { _basketBuilder.BasketId.ToString(), 0 } });

            Assert.Equal(0, basket.Items.Count);
        }

        public void Dispose()
        {
            _ignite.Dispose();
        }
    }
}
