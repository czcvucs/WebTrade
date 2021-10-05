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
    public class BuyerRepository : IBuyerRepository
    {
        private readonly WebTradeDbContext _webTradeDbContext;

        public BuyerRepository(WebTradeDbContext webTradeDbContext)
        {
            _webTradeDbContext = webTradeDbContext;
        }

        public async Task<IEnumerable<Buyer>> GetBuyers(CancellationToken cancellationToken)
        {
            return await _webTradeDbContext.Buyers.ToListAsync(cancellationToken);
        }
        
        public async Task<Buyer> GetBuyerById(Guid buyerId, CancellationToken cancellationToken)
        {
            return await _webTradeDbContext.Buyers.Where(b => b.Id == buyerId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
