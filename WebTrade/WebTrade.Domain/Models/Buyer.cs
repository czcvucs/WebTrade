using System;
using System.Collections.Generic;
using System.Text;

namespace WebTrade.Domain.Models
{
    public class Buyer : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Trade> Trades { get; set; }
    }
}
