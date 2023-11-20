using System.ComponentModel.DataAnnotations;

namespace InvestmentPerformanceWebAPI.Models;

public class InvestmentInfo
{
    [Key]
    public required int InvestmentId { get; set; }
    public required int UserId { get; set; }
    public required int ShareNumber { get; set; }
    public required float BoughtPrice { get; set; }
    public required float CurrentPrice { get; set; }
    public required bool ShortTerm { get; set; }

    public float CostBasis { get{return BoughtPrice/ShareNumber;}}
    public float CurrentValue { get{return ShareNumber*CurrentPrice;}}
    public float TotalGain { get{return CurrentValue - (BoughtPrice * ShareNumber);}}

}
