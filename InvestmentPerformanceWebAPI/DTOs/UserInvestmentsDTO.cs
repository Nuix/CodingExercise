namespace InvestmentPerformanceWebAPI.DTOs
{
    /// <summary>
    /// Response DTO representing a user's list of investments.
    /// </summary>
    public class UserInvestmentsDTO
    {
        public List<UserInvestmentSummaryDTO> Investments { get; set; } = [];
    }

    /// <summary>
    /// Summary view for a single investment, used in list endpoints.
    /// </summary>
    public class UserInvestmentSummaryDTO
    {
        public int InvestmentId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
