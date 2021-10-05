using FluentValidation;

namespace WebTrade.Application.Market.UpdateMarket
{
    public class UpdateMarketCommandValidator : AbstractValidator<UpdateMarketCommand>
    {
        public UpdateMarketCommandValidator()
        {
            RuleFor(x => x.NewMarketPrice).NotNull().NotEmpty().WithErrorCode("invalid");
        }
    }
}
