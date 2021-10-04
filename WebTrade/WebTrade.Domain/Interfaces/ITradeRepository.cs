using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Models;

namespace WebTrade.Domain.Interfaces
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetTrades(CancellationToken cancellationToken);
        Task AddTrade(Trade newTrade, CancellationToken cancellationToken);
        Task<Trade> GetTradeById(Guid tradeId, CancellationToken cancellationToken);
        Task DeleteTrade(Trade existingTrade, CancellationToken cancellationToken);
    }
}
