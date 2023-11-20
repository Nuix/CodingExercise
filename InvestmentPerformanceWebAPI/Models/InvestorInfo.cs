using System.ComponentModel.DataAnnotations;

namespace InvestmentPerformanceWebAPI.Models;

public class InvestorInfo
{
    [Key]
    public required int UserId { get; set; }
    public required string Name { get; set; }
}