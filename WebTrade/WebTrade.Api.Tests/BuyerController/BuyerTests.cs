using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTrade.Application.Buyer;
using WebTrade.Domain.Models;

namespace WebTrade.Api.Tests.BuyerController
{
    [TestClass]
    public class BuyerTests : TestBase
    {
        [TestMethod]
        public async Task ShouldReturnEmptyList_WhenDontHaveBuyers()
        {
            var response = await ApiHttpClient.GetAsync($"api/buyer/GetBuyers");
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<BuyerDto>>(responseContent);

            result.Count().Should().Be(0);
        }

        [TestMethod]
        public async Task ShouldReturnListOfBuyers_WhenBuyersExists()
        {
            var buyer1 = new Buyer { Id = Guid.NewGuid(), Name = "John" };
            var buyer2 = new Buyer { Id = Guid.NewGuid(), Name = "Alice" };
            await AddAsync(buyer1);
            await AddAsync(buyer2);

            var response = await ApiHttpClient.GetAsync($"api/buyer/GetBuyers");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<BuyerDto>>(responseContent);

            result.Count().Should().Be(2);
            result.FirstOrDefault(b => b.Name == buyer1.Name).Should().NotBeNull();
            result.FirstOrDefault(b => b.Name == buyer2.Name).Should().NotBeNull();
        }
    }
}
