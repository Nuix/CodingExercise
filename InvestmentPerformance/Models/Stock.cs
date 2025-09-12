namespace InvestmentPerformance.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string TickerSymbol { get; set; }
        public decimal CurrentPricePerShare { get; set; }
        public int TotalNumberOfShares { get; set; }

        public ICollection<Investment> Investments { get; set; } = new List<Investment>();
    }
}
