using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.eShopWeb.FunctionalTests.Web.Controllers;
using Microsoft.eShopWeb.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;
using Microsoft.eShopWeb.IntegrationTests;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.WebRazorPages
{
    [Collection("Sequential")]
    public class BasketPageCheckout : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public BasketPageCheckout(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        public HttpClient Client { get; }

        private string GetRequestVerificationToken(string input)
        {
            string regexpression = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
            var regex = new Regex(regexpression);
            var match = regex.Match(input);
            return match.Groups.Values.LastOrDefault().Value;
        }

        [Fact]
        public async Task RedirectsToLoginIfNotAuthenticated()
        {
            // Arrange & Act

            // Load Home Page
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse1 = await response.Content.ReadAsStringAsync();

            string token = GetRequestVerificationToken(stringResponse1);

            // Add Item to Cart
            var itemName = ".NET Black & White Mug";
            var catalogItems = await new IgniteAdapter(Ignition.GetIgnite()).GetRepo<CatalogItem>().ListAllAsync();
            var catalogItemId = catalogItems.Single(x => x.Name == itemName).Id;
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", catalogItemId.ToString()),
                new KeyValuePair<string, string>("name", "shirt"),
                new KeyValuePair<string, string>("price", "19.49"),
                new KeyValuePair<string, string>("__RequestVerificationToken", token)
            };

            var formContent = new FormUrlEncodedContent(keyValues);

            var postResponse = await Client.PostAsync("/basket/index", formContent);
            postResponse.EnsureSuccessStatusCode();
            var stringResponse = await postResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains(HtmlEncoder.Default.Encode(itemName), stringResponse);

            keyValues.Clear();
            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));

            formContent = new FormUrlEncodedContent(keyValues);
            var postResponse2 = await Client.PostAsync("/Basket/Checkout", formContent);
            Assert.Contains("/Identity/Account/Login", postResponse2.RequestMessage.RequestUri.ToString());
        }
    }
}
