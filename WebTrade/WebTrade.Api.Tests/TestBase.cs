using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTrade.Infrastructure;

namespace WebTrade.Api.Tests
{
    public class TestBase
    {
        protected TestBase()
        {

        }

        internal static HttpClient ApiHttpClient { get; set; }

        internal static WebTradeDbContext WebTradeDbContext { private get; set; }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            WebTradeDbContext.Add(entity);
            await WebTradeDbContext.SaveChangesAsync();
        }

        [TestInitialize]
        public void TestSetUp()
        {
            WebTradeDbContext.Trades.RemoveRange(WebTradeDbContext.Trades.ToList());
            WebTradeDbContext.Buyers.RemoveRange(WebTradeDbContext.Buyers.ToList());
            WebTradeDbContext.Markets.RemoveRange(WebTradeDbContext.Markets.ToList());
            WebTradeDbContext.SaveChanges();
        }
    }
}
