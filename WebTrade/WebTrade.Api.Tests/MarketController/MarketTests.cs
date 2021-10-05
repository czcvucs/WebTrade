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
using WebTrade.Application.Market;
using WebTrade.Application.Market.UpdateMarket;
using WebTrade.Domain.Models;

namespace WebTrade.Api.Tests.MarketController
{
    [TestClass]
    public class MarketTests : TestBase
    {
        [TestMethod]
        public async Task ShouldReturnEmptyList_WhenDontHaveMarkets()
        {
            var response = await ApiHttpClient.GetAsync($"api/market/GetMarkets");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<MarketDto>>(responseContent);

            result.Count().Should().Be(0);
        }

        [TestMethod]
        public async Task ShouldReturnListOfBuyers_WhenBuyersExists()
        {
            var market1 = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice=10};
            var market2 = new Market { Id = Guid.NewGuid(), SecurityCode = "AAPL", MarketPrice = 15};
            await AddAsync(market1);
            await AddAsync(market2);

            var response = await ApiHttpClient.GetAsync($"api/market/GetMarkets");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<MarketDto>>(responseContent);

            result.Count().Should().Be(2);
            result.FirstOrDefault(b => b.SecurityCode == market1.SecurityCode).Should().NotBeNull();
            result.FirstOrDefault(b => b.SecurityCode == market1.SecurityCode).MarketPrice.Should().Be(10);
            result.FirstOrDefault(b => b.SecurityCode == market2.SecurityCode).Should().NotBeNull();
            result.FirstOrDefault(b => b.SecurityCode == market2.SecurityCode).MarketPrice.Should().Be(15);
        }

        [TestMethod]
        public async Task ShouldReturnException_WhenTryToUpdateWithWrongValue()
        {
            var market1 = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice = 10 };
            await AddAsync(market1);

            var newMarket = new UpdateMarketCommand { MarketId = market1.Id, NewMarketPrice = 0 };
            var serializedContent = JsonConvert.SerializeObject(newMarket);
            using var httpContent = new StringContent(serializedContent);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await ApiHttpClient.PutAsync($"api/market/UpdateMarket", httpContent);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Contain("'New Market Price' must not be empty");
        }

        [TestMethod]
        public async Task ShouldUpdateMarket()
        {
            var market1 = new Market { Id = Guid.NewGuid(), SecurityCode = "MSFT", MarketPrice = 10 };
            await AddAsync(market1);

            var newMarket = new UpdateMarketCommand { MarketId = market1.Id, NewMarketPrice = 20 };
            var serializedContent = JsonConvert.SerializeObject(newMarket);
            using var httpContent = new StringContent(serializedContent);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await ApiHttpClient.PutAsync($"api/market/UpdateMarket", httpContent);

            var response = await ApiHttpClient.GetAsync($"api/market/GetMarkets");
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<MarketDto>>(responseContent);
            result.FirstOrDefault(b => b.SecurityCode == market1.SecurityCode).MarketPrice.Should().Be(20);
        }
    }
}
