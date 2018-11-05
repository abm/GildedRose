using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GildedRose.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddAuthentication("TokenAuthentication")
                .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>("TokenAuthentication", null);

            services.AddScoped<ITokenService>(service => new TokenService(new Dictionary<string, Customer>()
            {
                {"9A8BA13D14248", new Customer(Guid.NewGuid(), "Bankrupt Customer")},
                {"11B89A1A3A826", new Customer(Guid.NewGuid(), "New Customer")},
                {"E5E2C487F77BA", new Customer(Guid.NewGuid(), "Good Customer")}
            }));
            services.AddSingleton<IInventory>(
                Inventory.Build(new[] {
                    new InventoriedItem(
                        Guid.NewGuid(),
                        new Item(
                            "Armenian Enamelled & Filigree Silver Holy Altar Box",
                            "Armenia/Ottoman Turkey 18th-19th century; length: 7cm, width: 5.3cm, height: 2.6cm, weight: 61.45g",
                            10000
                        ),
                        1
                    )
                })
            );
            services.AddScoped<IPaymentProcessor, PaymentProcessor>();
            services.AddScoped<IOrderProcessor, OrderProcessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
