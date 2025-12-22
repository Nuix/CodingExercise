
namespace InvestmentPerformanceWebApi.DbInfrastructure.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Shares { get; set; }
        public decimal CostBasis { get; set; } //price of 1 share of stock at time it was purchased
        public decimal CurrentPrice { get; set; } // current price of 1 share of stock
        public DateTime PurchasedAtUtc { get; set; } // can be used to calculate the Term
        public DateTime CreatedAtUtc { get; set; } 

    }
}
