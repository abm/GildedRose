using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace GildedRose.Web.Tests
{
    public class OrdersIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> api;

        public OrdersIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            api = factory;
        }

        [Fact]
        public async Task AuthNeeded()
        {
            var client = api.CreateClient();
            var order = new Order(
                new Customer(Guid.NewGuid(), "Example Customer"),
                new[] { new OrderItem(Guid.NewGuid(), 1) }
            );

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/orders", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GoodCustomer()
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
            var order = new Order(
                new Customer(Guid.NewGuid(), "Good Customer"),
                new[] { new OrderItem(item.Id, 1) }
            );

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "E5E2C487F77BA");
            var response = await client.PostAsync("/api/orders", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task NewCustomer()
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
            var order = new Order(
                new Customer(Guid.NewGuid(), "New Customer"),
                new[] { new OrderItem(item.Id, 1) }
            );

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "11B89A1A3A826");
            var response = await client.PostAsync("/api/orders", content);
            var result = JsonConvert.DeserializeObject<CanceledOrder>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("No payment method set", result.Reason);
        }

        [Fact]
        public async Task BankruptCustomer()
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
            var order = new Order(
                new Customer(Guid.NewGuid(), "Bankrupt Customer"),
                new[] { new OrderItem(item.Id, 1) }
            );

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "9A8BA13D14248");
            var response = await client.PostAsync("/api/orders", content);
            var result = JsonConvert.DeserializeObject<CanceledOrder>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Insufficient funds", result.Reason);
        }
    }
}