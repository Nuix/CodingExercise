using System.Threading.Tasks;
using InvestmentPerformance.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformance.Web.Controllers
{
    [ApiController]
    [Route("performance")]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentsService _investmentsService;
        
        public InvestmentsController(IInvestmentsService investmentsRepository)
        {
            _investmentsService = investmentsRepository;
        }
        
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetInvestmentByUser(int userId)
            => Ok(await _investmentsService.GetInvestmentsByUserIdAsync(userId));
        
        [HttpGet("users/{userId}/investments/{investmentId}")]
        public async Task<IActionResult> GetInvestmentDetails(int userId, int investmentId)
            => Ok( await _investmentsService.GetInvestmentsByUserIdAndInvestmentIdAsync(userId, investmentId));
    }
}