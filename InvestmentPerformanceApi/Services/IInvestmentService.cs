namespace InvestmentPerformanceApi.Services
{
    public interface IInvestmentService
    {
        Task<string?> getUserInvestmentsAsync(int userId);
        Task<string?> getInvestmentDetails(int userId, int investmentId);
    }
}
