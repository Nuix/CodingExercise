using NUIX.InvestmentPerformance.API.DataTransferObjects;

namespace NUIX.InvestmentPerformance.API.Services
{
    public interface IInvestmentService
    {
        public List<InvestmentSummaryDTO> GetInvestmentsForUser(Guid userId);
        public InvestmentDetailDTO? GetInvestmentDetails(Guid userId, Guid investmentId);
    }
}
