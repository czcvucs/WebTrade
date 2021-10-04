using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebTrade.Application.Constants;
using WebTrade.Domain.Interfaces;

namespace WebTrade.Application.Trades.DeleteTrade
{
    public class DeleteTradeCommand : IRequest<Unit>
    {
        public Guid TradeId { get; set; }
    }

    public class DeleteTradeCommandHandler : IRequestHandler<DeleteTradeCommand, Unit>
    {
        private readonly ITradeRepository _tradeRepository;

        public DeleteTradeCommandHandler(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<Unit> Handle(DeleteTradeCommand request, CancellationToken cancellationToken)
        {
            var existingTrade = await _tradeRepository.GetTradeById(request.TradeId, cancellationToken);
            if (existingTrade == null)
            {
                throw new Exception(ExceptionMessages.TradeNotFound);
            }

            await _tradeRepository.DeleteTrade(existingTrade, cancellationToken);

            return Unit.Value;
        }
    }
}
