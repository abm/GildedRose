using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
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
    }
}
