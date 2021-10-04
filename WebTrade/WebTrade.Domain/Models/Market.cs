using System;
using System.Collections.Generic;
using System.Text;

namespace WebTrade.Domain.Models
{
    public class Market : EntityBase
    {
        public string SecurityCode { get; set; }
        public double MarketPrice { get; set; }
        public ICollection<Trade> Trades { get; set; }
    }
}
