using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Application.Constants;
using WebTrade.Domain.Interfaces;
using WebTrade.Domain.Models;

namespace WebTrade.Application.Trades.AddTrade
{
    public class AddTradeCommand : IRequest<Unit>
    {
        public Guid MarketId { get; set; }
        public int TradeQuantity { get; set; }
        public Guid BuyerId { get; set; }
    }

    public class AddTradeCommandHandler : IRequestHandler<AddTradeCommand, Unit>
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IBuyerRepository _buyerRepository;

        public AddTradeCommandHandler(
            ITradeRepository tradeRepository,
            IMarketRepository marketRepository,
            IBuyerRepository buyerRepository)
        {
            _tradeRepository = tradeRepository;
            _marketRepository = marketRepository;
            _buyerRepository = buyerRepository;
        }

        public async Task<Unit> Handle(AddTradeCommand request, CancellationToken cancellationToken)
        {
            var market = await _marketRepository.GetMarketById(request.MarketId, cancellationToken);
            if (market == null)
            {
                throw new Exception(ExceptionMessages.MarketNotFound);
            }

            var buyer = await _buyerRepository.GetBuyerById(request.BuyerId, cancellationToken);
            if (buyer == null)
            {
                throw new Exception(ExceptionMessages.BuyerNotFound);
            }

            var newTrade = new Trade
            {
                TradeDate = DateTime.Now,
                TradeQuantity = request.TradeQuantity,
                MarketId = market.Id,
                TradePrice = market.MarketPrice,
                BuyerId = buyer.Id,
            };

            await _tradeRepository.AddTrade(newTrade, cancellationToken);

            return Unit.Value;
        }
    }
}
