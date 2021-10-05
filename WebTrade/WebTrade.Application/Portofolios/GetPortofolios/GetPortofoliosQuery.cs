using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Domain.Interfaces;

namespace WebTrade.Application.Portofolios.GetPortofolios
{
    public class GetPortofoliosQuery : IRequest<IEnumerable<PortofolioDto>>
    {
    }

    public class GetPortofoliosQueryHandler : IRequestHandler<GetPortofoliosQuery, IEnumerable<PortofolioDto>>
    {
        private readonly ITradeRepository _tradeRepository;

        public GetPortofoliosQueryHandler(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<IEnumerable<PortofolioDto>> Handle(GetPortofoliosQuery request, CancellationToken cancellationToken)
        {
            var trades = await _tradeRepository.GetTrades(cancellationToken);
            var buyerTrades = trades.GroupBy(t => t.BuyerId);
            var portofolios = buyerTrades.Select(bt => new PortofolioDto
            {
                HolderName = bt.First().Buyer.Name,
                PurchaseValue = bt.Sum(t => t.TradePrice * t.TradeQuantity),
                MarketValue = bt.Sum(t => t.Market.MarketPrice * t.TradeQuantity)
            });
            return portofolios;
        }
    }
}
