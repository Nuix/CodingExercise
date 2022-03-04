using InvestmentPerformance.Business;
using InvestmentPerformance.Business.Models;
using InvestmentPerformance.Business.Models.APIResponses;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformance.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserInvestmentsController : ControllerBase
    {
        private readonly IBusinessService _service;

        public UserInvestmentsController(IBusinessService service)
        {
            _service = service;
        }

        [HttpGet("/get-user-investments/{userId}")]
        public async Task<GetUserInvestmentsByUserResponse> GetUserInvestmentsByUser(int userId)
        {
            return await _service.GetUserInvestmentVMs(userId);
        }

        [HttpGet("/get-investment-details/{investmentId}")]
        public async Task<GetUserInvestmentsDetailsResponse> GetUserInvestmentsDetails(int investmentId)
        {
            return await _service.GetUserInvestmentsDetails(investmentId);            
        }

    }
}
