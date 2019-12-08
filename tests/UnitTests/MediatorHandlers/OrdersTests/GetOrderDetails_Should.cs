﻿using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests
{
    public class GetOrderDetails_Should
    {
        private readonly Mock<IAsyncRepository<Order>> _mockOrderRepository;
        private readonly Guid _orderId = Guid.NewGuid();

        public GetOrderDetails_Should()
        {
            var item = new OrderItem(new CatalogItemOrdered(Guid.NewGuid(), "ProductName", "URI"), 10.00m, 10);
            var address = new Address(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Order order = new Order("buyerId", address, new List<OrderItem> {item}) {Id = _orderId};

            _mockOrderRepository = new Mock<IAsyncRepository<Order>>();
            _mockOrderRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Order>>())).ReturnsAsync(new List<Order> { order });
        }

        [Fact]
        public async Task NotBeNull_If_Order_Exists()
        {
            var request = new GetOrderDetails("SomeUserName", _orderId);

            var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task BeNull_If_Order_Not_found()
        {
            var request = new GetOrderDetails("SomeUserName", Guid.NewGuid());

            var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
