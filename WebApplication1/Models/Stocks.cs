namespace StockTracker2.Models
{
    public class Stocks
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Price { get; set; }
        public decimal ChangePercent { get; set; }
        public decimal? P_E { get; set; }
        public decimal EPS { get; set; }
        public decimal EPSGrowth { get; set; }
        public decimal DivYield { get; set; }
        public string Sector { get; set; }
        public string AnalystRating { get; set; }
    }
}
