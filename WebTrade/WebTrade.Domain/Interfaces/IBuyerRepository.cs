using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Models;

namespace WebTrade.Domain.Interfaces
{
    public interface IBuyerRepository
    {
        Task<IEnumerable<Buyer>> GetBuyers(CancellationToken cancellationToken);
        Task<Buyer> GetBuyerById(Guid buyerId, CancellationToken cancellationToken);
    }
}
