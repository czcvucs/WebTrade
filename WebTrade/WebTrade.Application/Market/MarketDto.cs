using System;

namespace WebTrade.Application.Market
{
    public class MarketDto
    {
        public Guid Id { get; set; }
        public string SecurityCode { get; set; }
        public double MarketPrice { get; set; }
    }
}
