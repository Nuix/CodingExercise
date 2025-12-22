using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace InvestmentPerformanceWebApi.Api.ViewModels.Investments
{
    public class InvestmentDetailView
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public int Shares { get; set; }
        public decimal CostBasis { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal TotalGainOrLoss { get; set; }
        public string Term { get; set; } = string.Empty;
    }

}
