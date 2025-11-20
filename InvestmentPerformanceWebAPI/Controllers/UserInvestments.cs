using InvestmentPerformanceWebAPI.DTOs;
using InvestmentPerformanceWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformanceWebAPI.Controllers
{
    /// <summary>
    /// API controller that exposes endpoints for querying user investments and investment details.
    /// </summary>
    [ApiController]
    [Route("api/users/{userId:int}/investments")]
    public class UserInvestmentsController(ILogger<UserInvestmentsController> logger, IUserInvestmentService userInvestments) : ControllerBase
    {
        private readonly ILogger<UserInvestmentsController> _logger = logger;
        private readonly IUserInvestmentService _userInvestments = userInvestments;

        /// <summary>
        /// Gets a list of investments for a given user (id + name only).
        /// </summary>
        /// <param name="userId">Identifier of the user whose investments to retrieve.</param>
        [HttpGet]
        public async Task<ActionResult<UserInvestmentsDTO>> GetUserInvestments(int userId)
        {
            try
            {
                var dto = await _userInvestments.GetUserInvestmentsAsync(userId);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching investments for userId {userId}", userId);
                return Problem("Internal server error", statusCode: 500);
            }
        }

        /// <summary>
        /// Gets detailed information about a single investment for a given user.
        /// </summary>
        /// <param name="userId">Identifier of the owning user.</param>
        /// <param name="investmentId">Identifier of the investment to retrieve.</param>
        [HttpGet("{investmentId:int}")]
        public async Task<ActionResult<InvestmentDTO>> GetInvestment(int userId, int investmentId)
        {
            try
            {
                var dto = await _userInvestments.GetInvestmentAsync(userId, investmentId);

                if (dto is null)
                {
                    if (_logger.IsEnabled(LogLevel.Warning))
                    {
                        _logger.LogWarning("Investment {investmentId} for user {userId} not found", investmentId, userId);
                    }
                    return NotFound();
                }

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching investment {investmentId} for userId {userId}", investmentId, userId);
                return Problem("Internal server error", statusCode: 500);
            }
        }
    }
}
