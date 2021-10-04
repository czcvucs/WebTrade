using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Interfaces;

namespace WebTrade.Application.Buyer.GetBuyers
{
    public class GetBuyersQuery : IRequest<IEnumerable<BuyerDto>>
    {
    }

    public class GetBuyersQueryHandler : IRequestHandler<GetBuyersQuery, IEnumerable<BuyerDto>>
    {
        private readonly IBuyerRepository _buyerRepository;

        public GetBuyersQueryHandler(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public async Task<IEnumerable<BuyerDto>> Handle(GetBuyersQuery request, CancellationToken cancellationToken)
        {
            var buyers = await _buyerRepository.GetBuyers(cancellationToken);
            return buyers.Select(b => new BuyerDto
            {
               Id = b.Id,
               Name = b.Name
            });
        }
    }
}
