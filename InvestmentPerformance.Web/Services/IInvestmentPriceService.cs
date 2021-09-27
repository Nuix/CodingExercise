using System.Threading.Tasks;
using InvestmentPerformance.Web.Enums;

namespace InvestmentPerformance.Web.Services
{
    public interface IInvestmentPriceService
    {
        Task<double> GetPriceAsync(string tickerSymbol, InvestmentType investmentType);
    }
}