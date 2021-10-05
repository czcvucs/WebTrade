using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Application.Constants;
using WebTrade.Domain.Interfaces;

namespace WebTrade.Application.Market.UpdateMarket
{
    public class UpdateMarketCommand : IRequest<Unit>
    {
        public Guid MarketId { get; set; }
        public double NewMarketPrice { get; set; }
    }

    public class UpdateMarketCommandHandler : IRequestHandler<UpdateMarketCommand, Unit>
    {
        private readonly IMarketRepository _marketRepository;

        public UpdateMarketCommandHandler(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository;
        }

        public async Task<Unit> Handle(UpdateMarketCommand request, CancellationToken cancellationToken)
        {
            var market = await _marketRepository.GetMarketById(request.MarketId, cancellationToken);
            if (market == null)
            {
                throw new Exception(ExceptionMessages.MarketNotFound);
            }

            await _marketRepository.UpdateMarketPrice(market, request.NewMarketPrice, cancellationToken);

            return Unit.Value;
        }
    }
}