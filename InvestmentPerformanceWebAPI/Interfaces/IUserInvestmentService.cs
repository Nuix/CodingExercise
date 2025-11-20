using InvestmentPerformanceWebAPI.DTOs;

namespace InvestmentPerformanceWebAPI.Interfaces
{
    /// <summary>
    /// Abstraction for querying investment data for users.
    /// </summary>
    public interface IUserInvestmentService
    {
        /// <summary>
        /// Returns a summary list of investments for the given user.
        /// </summary>
        Task<UserInvestmentsDTO> GetUserInvestmentsAsync(int userId);

        /// <summary>
        /// Returns detailed information for a single investment owned by the given user.
        /// Returns null if the investment is not found.
        /// </summary>
        Task<InvestmentDTO?> GetInvestmentAsync(int userId, int investmentId);
    }
}
