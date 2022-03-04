using InvestmentPerformance.Business;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformance.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserInvestmentsController : ControllerBase
    {
        private readonly IBusinessService _service;
        private readonly ILogger<UserInvestmentsController> _logger;

        public UserInvestmentsController(IBusinessService service, ILogger<UserInvestmentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("/get-user-investments/{userId}")]
        public async Task<IActionResult> GetUserInvestmentsByUser(int userId)
        {
            try
            {
                return Ok(await _service.GetUserInvestmentsByUser(userId));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("/get-investment-details/{investmentId}")]
        public async Task<IActionResult> GetUserInvestmentsDetails(int investmentId)
        {
            try
            {
                return Ok(await _service.GetUserInvestmentsDetails(investmentId));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }

        }

    }
}
