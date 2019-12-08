using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        private static readonly List<CatalogBrand> CatalogBrands = new List<CatalogBrand>
        {
            new CatalogBrand { Brand = "Azure"},
            new CatalogBrand { Brand = ".NET" },
            new CatalogBrand { Brand = "Visual Studio" },
            new CatalogBrand { Brand = "SQL Server" }, 
            new CatalogBrand { Brand = "Other" }
        };

        private static readonly List<CatalogType> CatalogTypes = new List<CatalogType>
        {
            new CatalogType { Type = "Mug"},
            new CatalogType { Type = "T-Shirt" },
            new CatalogType { Type = "Sheet" },
            new CatalogType { Type = "USB Memory Stick" }
        };

        public static async Task SeedAsync(IIgniteAdapter catalogContext)
        {
            await SeedAsync(catalogContext, CatalogBrands);
            await SeedAsync(catalogContext, CatalogTypes);
            await SeedAsync(catalogContext, GetPreconfiguredItems());
        }

        private static async Task SeedAsync<T>(IIgniteAdapter catalogContext, IEnumerable<T> data)
            where T : BaseEntity
        {
            var cache = catalogContext.GetCache<Guid, T>();
            if (cache.GetSize() == 0)
            {
                await cache.PutAllAsync(data.Select(b => new KeyValuePair<Guid, T>(b.Id, b)));
            }
        }

        private static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>
            {
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[1].Id, CatalogBrandId = CatalogBrands[1].Id, Description = ".NET Bot Black Sweatshirt",
                    Name = ".NET Bot Black Sweatshirt", Price = 19.5M,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[0].Id, CatalogBrandId = CatalogBrands[1].Id, Description = ".NET Black & White Mug",
                    Name = ".NET Black & White Mug", Price = 8.50M,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/2.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[1].Id, CatalogBrandId = CatalogBrands[4].Id, Description = "Prism White T-Shirt",
                    Name = "Prism White T-Shirt", Price = 12,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/3.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[1].Id, CatalogBrandId = CatalogBrands[1].Id, Description = ".NET Foundation Sweatshirt",
                    Name = ".NET Foundation Sweatshirt", Price = 12,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/4.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[2].Id, CatalogBrandId = CatalogBrands[4].Id, Description = "Roslyn Red Sheet", Name = "Roslyn Red Sheet",
                    Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/5.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[1].Id, CatalogBrandId = CatalogBrands[1].Id, Description = ".NET Blue Sweatshirt",
                    Name = ".NET Blue Sweatshirt", Price = 12,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/6.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[1].Id, CatalogBrandId = CatalogBrands[4].Id, Description = "Roslyn Red T-Shirt",
                    Name = "Roslyn Red T-Shirt", Price = 12,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/7.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[1].Id, CatalogBrandId = CatalogBrands[4].Id, Description = "Kudu Purple Sweatshirt",
                    Name = "Kudu Purple Sweatshirt", Price = 8.5M,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/8.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[0].Id, CatalogBrandId = CatalogBrands[4].Id, Description = "Cup<T> White Mug", Name = "Cup<T> White Mug",
                    Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/9.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[2].Id, CatalogBrandId = CatalogBrands[1].Id, Description = ".NET Foundation Sheet",
                    Name = ".NET Foundation Sheet", Price = 12,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/10.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[2].Id, CatalogBrandId = CatalogBrands[1].Id, Description = "Cup<T> Sheet", Name = "Cup<T> Sheet",
                    Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/11.png"
                },
                new CatalogItem
                {
                    CatalogTypeId = CatalogTypes[1].Id, CatalogBrandId = CatalogBrands[4].Id, Description = "Prism White TShirt",
                    Name = "Prism White TShirt", Price = 12,
                    PictureUri = "http://catalogbaseurltobereplaced/images/products/12.png"
                }
            };
        }
    }
}
