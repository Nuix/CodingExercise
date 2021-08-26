using Nuix.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nuix.Common.Services
{
  public interface IInvestmentService
  {
    Task<InvestmentDetails> GetUserInvestmentDetails(long userId, long investmentId);
    Task<IEnumerable<InvestmentDetailsLight>> GetUserInvestments(long userId);
  }
}