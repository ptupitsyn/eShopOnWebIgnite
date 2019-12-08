using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests
{
    public class DeleteBasket
    {
        private Mock<IAsyncRepository<Basket>> _mockBasketRepo;

        public DeleteBasket()
        {
            _mockBasketRepo = new Mock<IAsyncRepository<Basket>>();
        }

        [Fact]
        public async Task Should_InvokeBasketRepositoryDeleteAsync_Once()
        {
            var basket = new Basket();
            basket.AddItem(Guid.NewGuid(), It.IsAny<decimal>(), It.IsAny<int>());
            basket.AddItem(Guid.NewGuid(), It.IsAny<decimal>(), It.IsAny<int>());
            _mockBasketRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(basket);
            var basketService = new BasketService(_mockBasketRepo.Object, null);

            await basketService.DeleteBasketAsync(It.IsAny<Guid>());

            _mockBasketRepo.Verify(x => x.DeleteAsync(It.IsAny<Basket>()), Times.Once);
        }
    }
}
