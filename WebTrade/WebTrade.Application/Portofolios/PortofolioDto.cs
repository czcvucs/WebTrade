namespace WebTrade.Application.Portofolios
{
    public class PortofolioDto
    {
        public string HolderName { get; set; }
        public double PurchaseValue { get; set; }
        public double MarketValue { get; set; }
        public double ProfitLoss
        {
            get { return MarketValue - PurchaseValue; }
        }
    }
}
