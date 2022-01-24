using InvestmentPerformanceWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformanceWebApi.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class HoldingController : ControllerBase
{
    private readonly IHoldingService _holdingService;
    private readonly ILogger<HoldingController> _logger;

    public HoldingController(IHoldingService holdingService, ILogger<HoldingController> logger)
    {
        _holdingService = holdingService;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetHoldings(int userId)
    {
        try
        {
            var userExists = await _holdingService.UserExistsAsync(userId);
            if (!userExists)
                return NotFound($"UserId {userId} was not found");

            var holdings = await _holdingService.GetHoldingsAsync(userId);
            return Ok(holdings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting holdings data");
            return Problem("Error getting holdings data");
        }
    }

    [HttpGet("{userId}/{id}")]
    public async Task<IActionResult> GetHoldingDetails(int userId, string id)
    {
        try
        {
            var userExists = await _holdingService.UserExistsAsync(userId);
            if (!userExists)
                return NotFound($"UserId {userId} was not found");

            var holding = await _holdingService.GetHoldingDetailsAsync(userId, id);
            if (holding == null)
                return NotFound($"InvestmentId {id} was not found");

            return Ok(holding);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting holding item data");
            return Problem("Error getting holding item data");
        }
    }
}
