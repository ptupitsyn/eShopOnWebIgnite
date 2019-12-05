using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.eShopWeb.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class GetById
    {
        private readonly IIgniteAdapter _catalogContext;
        private readonly OrderRepository _orderRepository;
        private OrderBuilder OrderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;
        public GetById(ITestOutputHelper output)
        {
            _output = output;
            _catalogContext = new IgniteAdapter(Ignition.Start()); // TODO: Test ignite config here
            _orderRepository = new OrderRepository(_catalogContext);
        }

        [Fact]
        public async Task GetsExistingOrder()
        {
            var existingOrder = OrderBuilder.WithDefaultValues();
            await _catalogContext.GetRepo<Order>().AddAsync(existingOrder);
            int orderId = existingOrder.Id;
            _output.WriteLine($"OrderId: {orderId}");

            var orderFromRepo = await _orderRepository.GetByIdAsync(orderId);
            Assert.Equal(OrderBuilder.TestBuyerId, orderFromRepo.BuyerId);

            // Note: Using InMemoryDatabase OrderItems is available. Will be null if using SQL DB.
            var firstItem = orderFromRepo.OrderItems.FirstOrDefault();
            Assert.Equal(OrderBuilder.TestUnits, firstItem.Units);
        }
    }
}
