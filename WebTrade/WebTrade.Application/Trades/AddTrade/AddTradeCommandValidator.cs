using FluentValidation;

namespace WebTrade.Application.Trades.AddTrade
{
    public class AddTradeCommandValidator : AbstractValidator<AddTradeCommand>
    {
        public AddTradeCommandValidator()
        {
            RuleFor(x => x.TradeQuantity).NotNull().NotEmpty().WithErrorCode("invalid");
        }
    }
}
