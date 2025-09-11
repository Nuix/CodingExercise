using InvestmentPerformanceWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using InvestmentPerformanceWebApi.Services;

namespace InvestmentPerformanceWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InvestmentController(ILogger<InvestmentController> logger, IInvestmentService investmentService) : ControllerBase
{
    private readonly ILogger<InvestmentController> _logger = logger;
    private readonly IInvestmentService _investmentService = investmentService;
    
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<IReadOnlyList<InvestmentDto>>> Get(int userId, CancellationToken cancellationToken)
    {
        try
        {
            if (userId <= 0)
            {
                return BadRequest("UserId must be a positive integer.");
            }

            var investments = await _investmentService.GetInvestmentsByUserIdAsync(userId, cancellationToken);
            return Ok(investments);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Request cancelled while retrieving investments for user {UserId}", userId);
            return StatusCode(499, "Client Closed Request");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving investments for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving investments.");
        }
    }
    
    [HttpGet("{userId:int}/{investmentId:int}")]
    public async Task<ActionResult<InvestmentDetailDto>> GetDetails(int userId, int investmentId, CancellationToken cancellationToken)
    {
        try
        {
            if (userId <= 0 || investmentId <= 0)
            {
                return BadRequest("UserId and InvestmentId must be positive integers.");
            }

            var details = await _investmentService.GetInvestmentDetailsAsync(userId, investmentId, cancellationToken);
            if (details is null)
            {
                return NotFound();
            }

            return Ok(details);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Request cancelled while retrieving investment {InvestmentId} for user {UserId}", investmentId, userId);
            return StatusCode(499, "Client Closed Request");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving investment {InvestmentId} for user {UserId}", investmentId, userId);
            return StatusCode(500, "An error occurred while retrieving investment details.");
        }
    }
    
    [HttpGet("health")]
    public IActionResult Get()
    {
        return Ok("Investment API is running.");
    }
}