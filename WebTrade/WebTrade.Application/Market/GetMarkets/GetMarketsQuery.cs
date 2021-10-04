using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Interfaces;

namespace WebTrade.Application.Market.GetMarkets
{
    public class GetMarketsQuery : IRequest<IEnumerable<MarketDto>>
    {
    }

    public class GetMarketsQueryHandler : IRequestHandler<GetMarketsQuery, IEnumerable<MarketDto>>
    {
        private readonly IMarketRepository _marketRepository;

        public GetMarketsQueryHandler(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository;
        }

        public async Task<IEnumerable<MarketDto>> Handle(GetMarketsQuery request, CancellationToken cancellationToken)
        {
            var markets = await _marketRepository.GetMarkets(cancellationToken);
            return markets.Select(m => new MarketDto
            {
                Id = m.Id,
                SecurityCode = m.SecurityCode,
                MarketPrice = m.MarketPrice
            });
        }
    }
}
