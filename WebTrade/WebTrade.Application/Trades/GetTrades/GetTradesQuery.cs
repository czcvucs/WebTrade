using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Interfaces;

namespace WebTrade.Application.Trades.GetTrades
{
    public class GetTradesQuery : IRequest<IEnumerable<TradeDto>>
    {
    }

    public class GetTradesQueryHandler : IRequestHandler<GetTradesQuery, IEnumerable<TradeDto>>
    {
        private readonly ITradeRepository _tradeRepository;

        public GetTradesQueryHandler(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<IEnumerable<TradeDto>> Handle(GetTradesQuery request, CancellationToken cancellationToken)
        {
            var trades = await _tradeRepository.GetTrades(cancellationToken);
            return trades.Select(t => new TradeDto
            {
                Id = t.Id,
                MarketName = t.Market.SecurityCode,
                TradePrice = t.TradePrice,
                TradeDate = t.TradeDate,
                TradeQuantity = t.TradeQuantity,
                BuyerName = t.Buyer.Name
            });
        }
    }
}
