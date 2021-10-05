using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebTrade.Application.Trades;
using WebTrade.Application.Trades.AddTrade;
using WebTrade.Domain.Models;

namespace WebTrade.Api.Tests.TradeController
{
    [TestClass]
    public class TradeTests : TestBase
    {
        [TestMethod]
        public async Task ShouldReturnEmptyList_WhenDontHaveTrades()
        {
            var response = await ApiHttpClient.GetAsync($"api/trade/GetTrades");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TradeDto>>(responseContent);

            result.Count().Should().Be(0);
        }

        [TestMethod]
        public async Task ShouldReturnTrades_WhenTradesExists()
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
            
            var response = await ApiHttpClient.GetAsync($"api/trade/GetTrades");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TradeDto>>(responseContent);

            result.Count().Should().Be(1);
        }


        [TestMethod]
        public async Task ShouldReturnException_WhenTryToAddTradeWithUnexistingMarket()
        {
            var buyer = new Buyer { Id = Guid.NewGuid(), Name = "John" };
            var market = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice = 10 };
            await AddAsync(buyer);
            await AddAsync(market);

            var newTrade = new AddTradeCommand { MarketId = Guid.NewGuid(), BuyerId = buyer.Id, TradeQuantity = 10 };
            var serializedContent = JsonConvert.SerializeObject(newTrade);
            using var httpContent = new StringContent(serializedContent);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await ApiHttpClient.PostAsync($"api/trade/AddTrade", httpContent);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Contain("The market does not exist.");
        }

        [TestMethod]
        public async Task ShouldReturnException_WhenTryToAddTradeWithUnexistingBuyer()
        {
            var buyer = new Buyer { Id = Guid.NewGuid(), Name = "John" };
            var market = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice = 10 };
            await AddAsync(buyer);
            await AddAsync(market);

            var newTrade = new AddTradeCommand { MarketId = market.Id, BuyerId = Guid.NewGuid(), TradeQuantity = 10 };
            var serializedContent = JsonConvert.SerializeObject(newTrade);
            using var httpContent = new StringContent(serializedContent);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await ApiHttpClient.PostAsync($"api/trade/AddTrade", httpContent);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Contain("The buyer does not exist.");
        }

        [TestMethod]
        public async Task ShouldReturnException_WhenTryToAddTradeWithWrongQuantity()
        {
            var buyer = new Buyer { Id = Guid.NewGuid(), Name = "John" };
            var market = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice = 10 };
            await AddAsync(buyer);
            await AddAsync(market);

            var newTrade = new AddTradeCommand { MarketId = market.Id, BuyerId = buyer.Id, TradeQuantity = 0 };
            var serializedContent = JsonConvert.SerializeObject(newTrade);
            using var httpContent = new StringContent(serializedContent);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await ApiHttpClient.PostAsync($"api/trade/AddTrade", httpContent);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Contain("'Trade Quantity' must not be empty.");
        }

        [TestMethod]
        public async Task ShouldDeleteTrade_WhenTradeExists()
        {
            var buyer = new Buyer { Id = Guid.NewGuid(), Name = "John" };
            var market = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice = 10 };
            var trade1 = new Trade
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
            await AddAsync(trade1);

            await ApiHttpClient.DeleteAsync($"api/trade/DeleteTrade/{trade1.Id}");
            var response = await ApiHttpClient.GetAsync($"api/trade/GetTrades");
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TradeDto>>(responseContent);

            result.Count().Should().Be(0);
            await AddAsync(trade1);
        }

        [TestMethod]
        public async Task ShouldReturnException_WhenTradeDoesNotExist()
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

            var response = await ApiHttpClient.DeleteAsync($"api/trade/DeleteTrade/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Contain("The trade does not exist.");
        }
    }
}
