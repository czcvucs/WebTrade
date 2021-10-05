using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTrade.Infrastructure;

namespace WebTrade.Api.Tests
{
    [TestClass]
    public static class TestAssemblyInitialize
    {
        public static ServiceProvider ServiceProvider { get; set; }
        private static IServiceScope Scope { get; set; }
        private static CustomWebApplicationFactory<Startup> _webApplicationFactory;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            _webApplicationFactory = new CustomWebApplicationFactory<Startup>();
            TestBase.ApiHttpClient = _webApplicationFactory.CreateClient();

            Scope = ServiceProvider.CreateScope();

            var scopedServices = Scope.ServiceProvider;
            TestBase.WebTradeDbContext = scopedServices.GetRequiredService<WebTradeDbContext>();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            TestBase.ApiHttpClient.Dispose();
            _webApplicationFactory.Dispose();
            Scope.Dispose();
        }
    }
}
