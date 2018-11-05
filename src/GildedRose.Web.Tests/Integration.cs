using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace GildedRose.Web.Tests
{
    public class Integration : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> api;

        public Integration(WebApplicationFactory<Startup> factory)
        {
            api = factory;
        }

        [Fact]
        public async Task Trivial()
        {
            var client = api.CreateClient();

            var response = await client.GetAsync("/api/items");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSimpleInventory()
        {
            var item = new InventoriedItem(
                Guid.NewGuid(),
                new Item(
                    "Armenian Enamelled & Filigree Silver Holy Altar Box",
                    "Armenia/Ottoman Turkey 18th-19th century; length: 7cm, width: 5.3cm, height: 2.6cm, weight: 61.45g",
                    10000
                ),
                1
            );
            var client = api.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                    services.AddSingleton<IInventory>(
                        Inventory.Build(new[] { item })
                    )
                )
            ).CreateClient();

            var response = await client.GetAsync("/api/items");

            response.EnsureSuccessStatusCode();
            var receivedItem = JsonConvert.DeserializeObject<IEnumerable<InventoriedItem>>(
                await response.Content.ReadAsStringAsync()
            );
            Assert.Equal(item, receivedItem.FirstOrDefault());
        }
    }
}
