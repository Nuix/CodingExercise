using InvestmentWebApi.Data.Api;
using InvestmentWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InvestmentController : ControllerBase {
  private readonly ILogger<InvestmentController> _logger;
  private readonly IPortfolioManager _portfolioManager;

  public InvestmentController(ILogger<InvestmentController> logger, IPortfolioManager portfolioManager) {
    _logger = logger;
    _portfolioManager = portfolioManager;
  }

  [HttpGet("ForUser/{userId}")]
  public IEnumerable<InvestmentSummary> GetInvestments(int userId) {
    return _portfolioManager.GetInvestments(userId);
  }

  [HttpGet("Detailed/{investmentId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvestmentDetails))]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public IActionResult GetInvestmentDetails(int investmentId) {
    var investmentDetails = _portfolioManager.GetInvestmentDetails(investmentId);
    return investmentDetails == null
        ? NotFound()
        : Ok(investmentDetails);
  }
}