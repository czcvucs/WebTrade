using System;

namespace WebTrade.Application.Trades
{
    public class TradeDto
    {
        public Guid Id { get; set; }
        public double TradePrice { get; set; }
        public int TradeQuantity { get; set; }
        public DateTime TradeDate { get; set; }
        public string MarketName { get; set; }
        public string BuyerName { get; set; }
    }
}
