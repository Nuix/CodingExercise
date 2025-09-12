namespace InvestmentPerformance.Models
{
    public class Investment
    {
        public int ID { get; set; }
        public int UserID { get; set; } 
        public int StockID { get; set; }
        public int SharesOwned { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Stock Stock { get; set; }
    }
}
