using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTrade.Application.Portofolios;
using WebTrade.Domain.Models;

namespace WebTrade.Api.Tests.PortofolioController
{
    [TestClass]
    public class PortofolioTests : TestBase
    {
        [TestMethod]
        public async Task ShouldReturnEmptyList_WhenDontHaveBuyers()
        {
            var response = await ApiHttpClient.GetAsync($"api/portofolios/GetPortofolios");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<PortofolioDto>>(responseContent);

            result.Count().Should().Be(0);
        }

        [TestMethod]
        public async Task ShouldReturnPortofolios_WhenBuyersExistsAndHaveTrades()
        {
            var buyer = new Buyer { Id = Guid.NewGuid(), Name = "John" };
            var market = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice = 10 };
            var trade = new Trade
            {
                Id = Guid.NewGuid(),
                BuyerId = buyer.Id,
                MarketId = market.Id,
                TradeDate = DateTime.Now,
                TradePrice = market.MarketPrice,
                TradeQuantity = 10
            };
            await AddAsync(market);
            await AddAsync(buyer);
            await AddAsync(trade);

            var response = await ApiHttpClient.GetAsync($"api/portofolios/GetPortofolios");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<PortofolioDto>>(responseContent);

            result.Count().Should().Be(1);
            result.FirstOrDefault(p => p.HolderName == buyer.Name).Should().NotBeNull();
            result.FirstOrDefault(p => p.HolderName == buyer.Name).PurchaseValue.Should().Be(market.MarketPrice * trade.TradeQuantity);
            result.FirstOrDefault(p => p.HolderName == buyer.Name).MarketValue.Should().Be(market.MarketPrice * trade.TradeQuantity);
            result.FirstOrDefault(p => p.HolderName == buyer.Name).ProfitLoss.Should().Be(0);
        }
    }
}
