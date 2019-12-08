using System;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests
{
    public class CatalogFilterSpecificationFilter
    {
        private static readonly Guid[] Ids = Enumerable.Range(1, 10).Select(_ => Guid.NewGuid()).ToArray();
        
        [Theory]
        [InlineData(null, null, 5)]
        [InlineData(1, null, 3)]
        [InlineData(2, null, 2)]
        [InlineData(null, 1, 2)]
        [InlineData(null, 3, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(2, 3, 0)]
        public void MatchesExpectedNumberOfItems(int? brandId, int? typeId, int expectedCount)
        {
            var spec = new CatalogFilterSpecification(
                brandId == null ? (Guid?) null : Ids[brandId.Value],
                typeId == null ? (Guid?) null : Ids[typeId.Value]);

            var result = GetTestItemCollection()
                .AsQueryable()
                .Where(spec.Criteria);

            Assert.Equal(expectedCount, result.Count());
        }

        public List<CatalogItem> GetTestItemCollection()
        {
            return new List<CatalogItem>
            {
                new CatalogItem {CatalogBrandId = Ids[1], CatalogTypeId = Ids[1]},
                new CatalogItem {CatalogBrandId = Ids[1], CatalogTypeId = Ids[2]},
                new CatalogItem {CatalogBrandId = Ids[1], CatalogTypeId = Ids[3]},
                new CatalogItem {CatalogBrandId = Ids[2], CatalogTypeId = Ids[1]},
                new CatalogItem {CatalogBrandId = Ids[2], CatalogTypeId = Ids[2]},
            };
        }
    }
}
