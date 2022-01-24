using InvestmentPerformanceWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformanceWebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var holdings = await _userService.GetUsersAsync();
            return Ok(holdings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user data");
            return Problem("Error getting user data");
        }
    }
}

