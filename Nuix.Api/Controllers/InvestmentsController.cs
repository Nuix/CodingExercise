using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nuix.Common.Models;
using Nuix.Common.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nuix.Api.Controllers
{
  [Route("api")]
  [ApiController]
  public class InvestmentsController : ControllerBase
  {
    private readonly Lazy<ILogger<InvestmentsController>> _logger;
    private readonly IInvestmentService _investmentService;

    public InvestmentsController(Lazy<ILogger<InvestmentsController>> logger,
      IInvestmentService investmentService)
    {
      _logger = logger;
      _investmentService = investmentService;
    }

    [Route("users/{userId}/investments/{investmentId}")]
    [HttpGet]
    public async Task<IActionResult> GetUserInvestment(long userId, long investmentId)
    {
      try
      {
        InvestmentDetails investmentDetails = await _investmentService.GetUserInvestmentDetails(userId, investmentId);
        if (investmentDetails != null)
        {
          return Ok(investmentDetails);
        }
        return NotFound();
      }
      catch(Exception ex)
      {
        _logger.Value.LogError(ex, $"An exception occurred while attempting to retrieve a user investment ({userId}/{investmentId}).");
        return StatusCode(500);
      }
    }

    [Route("users/{userId}/investments")]
    [HttpGet]
    public async Task<IActionResult> GetUserInvestments(long userId)
    {
      try
      {
        IEnumerable<InvestmentDetailsLight> investments = await _investmentService.GetUserInvestments(userId);
        return Ok(investments);
      }
      catch (Exception ex)
      {
        _logger.Value.LogError(ex, $"An exception occurred while attempting to retrieve a user's investments ({userId}).");
        return StatusCode(500);
      }
    }
  }
}