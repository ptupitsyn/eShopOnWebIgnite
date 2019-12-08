﻿using Microsoft.eShopWeb.ApplicationCore.Entities;
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
        public static async Task SeedAsync(IIgniteAdapter catalogContext)
        {
            await SeedAsync(catalogContext, GetPreconfiguredCatalogBrands());
            await SeedAsync(catalogContext, GetPreconfiguredCatalogTypes());
            await SeedAsync(catalogContext, GetPreconfiguredItems());
        }

        private static async Task SeedAsync<T>(IIgniteAdapter catalogContext, IEnumerable<T> data)
            where T : BaseEntity
        {
            var brands = catalogContext.GetCache<int, T>();
            if (brands.GetSize() == 0)
            {
                await brands.PutAllAsync(data.Select(b => new KeyValuePair<int, T>(b.Id, b)));
            }
        }

        private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>
            {
                new CatalogBrand { Brand = "Azure"},
                new CatalogBrand { Brand = ".NET" },
                new CatalogBrand { Brand = "Visual Studio" },
                new CatalogBrand { Brand = "SQL Server" }, 
                new CatalogBrand { Brand = "Other" }
            };
        }

        private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                new CatalogType { Type = "Mug"},
                new CatalogType { Type = "T-Shirt" },
                new CatalogType { Type = "Sheet" },
                new CatalogType { Type = "USB Memory Stick" }
            };
        }

        private static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>
            {
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=2, Description = ".NET Bot Black Sweatshirt", Name = ".NET Bot Black Sweatshirt", Price = 19.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png" },
                new CatalogItem { CatalogTypeId=1,CatalogBrandId=2, Description = ".NET Black & White Mug", Name = ".NET Black & White Mug", Price= 8.50M, PictureUri = "http://catalogbaseurltobereplaced/images/products/2.png" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=5, Description = "Prism White T-Shirt", Name = "Prism White T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/3.png" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=2, Description = ".NET Foundation Sweatshirt", Name = ".NET Foundation Sweatshirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/4.png" },
                new CatalogItem { CatalogTypeId=3,CatalogBrandId=5, Description = "Roslyn Red Sheet", Name = "Roslyn Red Sheet", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/5.png" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=2, Description = ".NET Blue Sweatshirt", Name = ".NET Blue Sweatshirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/6.png" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=5, Description = "Roslyn Red T-Shirt", Name = "Roslyn Red T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/7.png"  },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=5, Description = "Kudu Purple Sweatshirt", Name = "Kudu Purple Sweatshirt", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/8.png" },
                new CatalogItem { CatalogTypeId=1,CatalogBrandId=5, Description = "Cup<T> White Mug", Name = "Cup<T> White Mug", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/9.png" },
                new CatalogItem { CatalogTypeId=3,CatalogBrandId=2, Description = ".NET Foundation Sheet", Name = ".NET Foundation Sheet", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/10.png" },
                new CatalogItem { CatalogTypeId=3,CatalogBrandId=2, Description = "Cup<T> Sheet", Name = "Cup<T> Sheet", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/11.png" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=5, Description = "Prism White TShirt", Name = "Prism White TShirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/12.png" }
            };
        }
    }
}
