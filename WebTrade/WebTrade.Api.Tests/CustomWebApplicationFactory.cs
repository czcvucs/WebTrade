using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WebTrade.Infrastructure;

namespace WebTrade.Api.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _ = builder.ConfigureServices(services =>
            {
                RegisterInMemoryDatabase(services);

                // Build the service provider
                var sp = services.BuildServiceProvider();

                TestAssemblyInitialize.ServiceProvider = sp;

                // Create a scope to obtain a reference to the database
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<WebTradeDbContext>();

                // Ensure the database is created.
                dbContext.Database.EnsureCreated();
            })
            .UseEnvironment("IntegrationTest");
        }

        private static void RegisterInMemoryDatabase(IServiceCollection services)
        {
            // Remove the app's WebTradeContext registration.
            var dbDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<WebTradeDbContext>));
            if (dbDescriptor != null)
            {
                services.Remove(dbDescriptor);
            }

            // Add a database context using an in-memory 
            // database for testing.
            services.AddDbContext<WebTradeDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForApiTests");
            });
        }
    }
}
