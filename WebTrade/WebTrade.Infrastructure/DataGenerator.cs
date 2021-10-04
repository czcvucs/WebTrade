using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTrade.Domain.Models;

namespace WebTrade.Infrastructure
{
	public class DataGenerator
	{
		public static void SeedDatabase(WebTradeDbContext context)
		{
			if (!context.Buyers.Any())
			{
				var testBuyer1 = new Buyer
				{
					Id = Guid.NewGuid(),
					Name = "John",
				};

				var testMarket1 = new Market
				{
					Id = Guid.NewGuid(),
					SecurityCode = "MSFT",
					MarketPrice = 10
				};

				var testMarket2 = new Market
				{
					Id = Guid.NewGuid(),
					SecurityCode = "AAPL",
					MarketPrice = 12
				};

				var testTrade1 = new Trade
				{
					Id = Guid.NewGuid(),
					MarketId = testMarket1.Id,
					TradePrice = testMarket1.MarketPrice,
					TradeQuantity = 2,
					TradeDate = DateTime.Now.AddDays(-50),
					BuyerId = testBuyer1.Id
				};

				var testTrade2 = new Trade
				{
					Id = Guid.NewGuid(),
					MarketId = testMarket2.Id,
					TradePrice = testMarket2.MarketPrice,
					TradeQuantity = 3,
					TradeDate = DateTime.Now.AddDays(-50),
					BuyerId = testBuyer1.Id
				};

				context.Buyers.AddRange(testBuyer1);
				context.Markets.AddRange(testMarket1, testMarket2);
				context.Trades.AddRange(testTrade1, testTrade2);
				context.SaveChanges();
			}
		}
	}
}
