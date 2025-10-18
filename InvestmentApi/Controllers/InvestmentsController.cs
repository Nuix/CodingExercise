using InvestmentApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestmentApi.Models;

namespace InvestmentApi.Controllers;

   [ApiController]
[Route("api/users/{userId:int}/[controller]")]
public class InvestmentsController : ControllerBase
{
    private readonly AppDbContext _db;
    public InvestmentsController(AppDbContext db) => _db = db;

    // GET: /api/users/1/investments
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var exists = await _db.Users.AnyAsync(u => u.Id == userId);
        if (!exists) return NotFound(new { message = $"User {userId} not found" });

        var items = await _db.Investments
            .Where(i => i.UserId == userId)
            .OrderBy ( i => i.Id)
            .Select(i => new {
                i.Id,
                i.Ticker,
                i.Units,
                i.CostBasis
            })
            .ToListAsync();

        return Ok(items);
    }
}

