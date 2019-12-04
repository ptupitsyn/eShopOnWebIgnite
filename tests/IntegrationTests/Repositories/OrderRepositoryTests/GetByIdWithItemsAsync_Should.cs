﻿using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.UnitTests.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class GetByIdWithItemsAsync_Should
    {
        private readonly IIgnite _catalogContext;
        private readonly OrderRepository _orderRepository;
        private OrderBuilder OrderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;
        public GetByIdWithItemsAsync_Should(ITestOutputHelper output)
        {
            _output = output;
            _orderRepository = new OrderRepository(_catalogContext);
            _catalogContext =  Ignition.Start(); // TODO: Test ignite config here
        }

        [Fact]
        public async Task GetOrderAndItemsByOrderId_When_MultipleOrdersPresent()
        {
            //Arrange
            var itemOneUnitPrice = 5.50m;
            var itemOneUnits = 2;
            var itemTwoUnitPrice = 7.50m;
            var itemTwoUnits = 5;

            var firstOrder = OrderBuilder.WithDefaultValues();
            await _catalogContext.GetRepo<Order>().AddAsync(firstOrder);
            int firstOrderId = firstOrder.Id;

            var secondOrderItems = new List<OrderItem>();
            secondOrderItems.Add(new OrderItem(OrderBuilder.TestCatalogItemOrdered, itemOneUnitPrice, itemOneUnits));
            secondOrderItems.Add(new OrderItem(OrderBuilder.TestCatalogItemOrdered, itemTwoUnitPrice, itemTwoUnits));
            var secondOrder = OrderBuilder.WithItems(secondOrderItems);
            await _catalogContext.GetRepo<Order>().AddAsync(secondOrder);
            int secondOrderId = secondOrder.Id;

            //Act
            var orderFromRepo = await _orderRepository.GetByIdWithItemsAsync(secondOrderId);

            //Assert
            Assert.Equal(secondOrderId, orderFromRepo.Id);
            Assert.Equal(secondOrder.OrderItems.Count, orderFromRepo.OrderItems.Count);
            Assert.Equal(1, orderFromRepo.OrderItems.Count(x => x.UnitPrice == itemOneUnitPrice));
            Assert.Equal(1, orderFromRepo.OrderItems.Count(x => x.UnitPrice == itemTwoUnitPrice));
            Assert.Equal(itemOneUnits, orderFromRepo.OrderItems.SingleOrDefault(x => x.UnitPrice == itemOneUnitPrice).Units);
            Assert.Equal(itemTwoUnits, orderFromRepo.OrderItems.SingleOrDefault(x => x.UnitPrice == itemTwoUnitPrice).Units);
        }
    }
}
