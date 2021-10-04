using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Models;

namespace WebTrade.Domain.Interfaces
{
    public interface IMarketRepository
    {
        Task<IEnumerable<Market>> GetMarkets(CancellationToken cancellationToken);
        Task<Market> GetMarketById(Guid marketId, CancellationToken cancellationToken);
    }
}
