using System;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests
{
    public class BasketWithItems
    {
        private readonly Guid _testBasketId = Guid.NewGuid();

        [Fact]
        public void MatchesBasketWithGivenId()
        {
            var spec = new BasketWithItemsSpecification(_testBasketId);

            var result = GetTestBasketCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(_testBasketId, result.Id);

        }

        [Fact]
        public void MatchesNoBasketsIfIdNotPresent()
        {
            var badId = Guid.Empty;
            var spec = new BasketWithItemsSpecification(badId);

            Assert.False(GetTestBasketCollection()
                .AsQueryable()
                .Any(spec.Criteria));
        }

        public List<Basket> GetTestBasketCollection()
        {
            return new List<Basket>
            {
                new Basket(),
                new Basket(),
                new Basket { Id = _testBasketId }
            };
        }
    }
}
