using System;
using System.Collections.Generic;
using System.Text;

namespace WebTrade.Domain.Models
{
    public class Trade : EntityBase
    {
        public double TradePrice { get; set; }
        public int TradeQuantity { get; set; }
        public DateTime TradeDate { get; set; }
        public Market Market { get; set; }
        public Guid MarketId { get; set; }
        public Buyer Buyer { get; set; }
        public Guid BuyerId { get; set; }
    }
}
