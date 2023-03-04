using System;
using InvestmentPerformance.Models;

namespace InvestmentPerformance.Services
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsersAsync();
        public Task<List<InvestmentDetails>> GetInvestmentDetailsAsync();
        public Task<List<Investment>> GetInvestmentByIdAsync(Guid id);
        public Task<List<InvestmentDetails>> GetInvestmentDetailsAsync(Guid userid, Guid investmentId);
    }
}

