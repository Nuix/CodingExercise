using System;
using System.Collections.Generic;
using InvestmentPerformance.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvestmentPerformance.Api.Controllers;

[ApiController]
[Route("investments")]
public sealed class InvestmentController(IInvestmentService investmentService, ILogger<InvestmentController> logger)
    : ControllerBase
{
    /// <summary>
    /// Retrieves a list of investments for a given user
    /// </summary>
    /// <param name="id">Id of the user you want investment data for</param>
    /// <returns>List of UserInvestment record for the user</returns>
    [HttpGet("users/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<UserInvestmentRecord>> GetUserInvestments(string id)
    {
        try
        {
            var result = investmentService.GetInvestmentsByUserId(int.Parse(id));

            return Ok(result);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Retrieves the information of a given investment
    /// </summary>
    /// <param name="id">Id of the investment you want information on</param>
    /// <returns>Investment record for the given investment</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetInvestment(string id)
    {
        try
        {
            var result = investmentService.GetInvestmentById(int.Parse(id));

            if(result == null)
                return NotFound();

        
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }

    }
}