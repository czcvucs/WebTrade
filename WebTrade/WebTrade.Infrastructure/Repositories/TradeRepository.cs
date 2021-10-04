using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Interfaces;
using WebTrade.Domain.Models;

namespace WebTrade.Infrastructure.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly WebTradeDbContext _webTradeDbContext;

        public TradeRepository(WebTradeDbContext webTradeDbContext)
        {
            _webTradeDbContext = webTradeDbContext;
        }

        public async Task<IEnumerable<Trade>> GetTrades(CancellationToken cancellationToken)
        {
            return await _webTradeDbContext.Trades.Include(b=>b.Buyer).Include(m=>m.Market).ToListAsync(cancellationToken);
        }

        public async Task AddTrade(Trade newTrade, CancellationToken cancellationToken)
        {
            await _webTradeDbContext.Trades.AddAsync(newTrade);
            await _webTradeDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Trade> GetTradeById(Guid tradeId, CancellationToken cancellationToken)
        {
            return await _webTradeDbContext.Trades.Where(t => t.Id == tradeId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task DeleteTrade(Trade existingTrade, CancellationToken cancellationToken)
        {
            _webTradeDbContext.Trades.Remove(existingTrade);
            await _webTradeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
