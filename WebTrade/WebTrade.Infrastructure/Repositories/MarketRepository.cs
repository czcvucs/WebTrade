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
    public class MarketRepository : IMarketRepository
    {
        private readonly WebTradeDbContext _webTradeDbContext;

        public MarketRepository(WebTradeDbContext webTradeDbContext)
        {
            _webTradeDbContext = webTradeDbContext;
        }

        public async Task<IEnumerable<Market>> GetMarkets(CancellationToken cancellationToken)
        {
            return await _webTradeDbContext.Markets.ToListAsync(cancellationToken);
        }

        public async Task<Market> GetMarketById(Guid marketId, CancellationToken cancellationToken)
        {
            return await _webTradeDbContext.Markets.Where(m => m.Id == marketId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
