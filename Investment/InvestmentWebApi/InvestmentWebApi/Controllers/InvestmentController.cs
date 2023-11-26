using InvestmentWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentWebApi.Controllers; 

[ApiController]
[Route("[controller]")]
public class InvestmentController : ControllerBase {
  private readonly ILogger<InvestmentController> _logger;

  public InvestmentController(ILogger<InvestmentController> logger) {
    _logger = logger;
  }

  [HttpGet("investments/{userId}")]
  public IEnumerable<InvestmentSummary> GetInvestments(int userId) {
    return Enumerable.Range(1, userId).Select(i => new InvestmentSummary {
            InvestmentId = i,
            Name = "Share_" + i
        })
        .ToArray();
  }

  [HttpGet("investments/{userId}/{investmentId}")]
  public InvestmentDetails GetInvestmentDetails(int userId, int investmentId) {
    return new InvestmentDetails {
        InvestmentId = investmentId,
        Name = "Share_" + investmentId,
        ShareCount = 1,
        CostBasis = 1,
        CurrentValue = 1,
        CurrentPrice = 1,
        Term = InvestmentDetails.HoldingTerm.Short,
        TotalGainLoss = 1
    };
  }


}