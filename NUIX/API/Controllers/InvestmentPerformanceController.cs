using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class InvestmentPerformanceController : ControllerBase
    {
        private readonly IInvestmentPerformanceService _investmentPerformanceService;

        public InvestmentPerformanceController(IInvestmentPerformanceService investmentPerformanceService) {
            _investmentPerformanceService = investmentPerformanceService;
        }

        [HttpGet("user/{userId}")] //api/investments/user
        public ActionResult<List<UserInvestment>> GetInvestmentslist(int userId)
        {
            return _investmentPerformanceService.GetInvestmentsListByUserId(userId);
        }

        [HttpGet("{investmentId}")] //api/investments/
        public ActionResult<List<StockInvestmentDetail>> GetInvestmentDetails(int investmentId)
        {
            return _investmentPerformanceService.GetStockInvestmentDetails(investmentId);       
        }
    }
}
