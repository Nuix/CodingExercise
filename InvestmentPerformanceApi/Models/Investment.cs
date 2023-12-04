using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvestmentPerformanceApi.Models
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        public string StockName { get; set; }
        public int Shares { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal CurrentPricePerShare { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CurrentValue  => CurrentPricePerShare * Shares;
        public decimal TotalGainOrLoss => CurrentValue - (PurchasePrice * Shares);
        
        [JsonConverter(typeof(StringEnumConverter))]
        public Term Term => PurchaseDate <= DateTime.Today.AddYears(-1) ? Term.Long : Term.Short;


        // Navigation property for the User
        public User User { get; set; }
    }

    public enum Term
    {
        Short, Long
    }
}
