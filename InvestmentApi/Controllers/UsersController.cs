// Controllers/UsersController.cs
using InvestmentApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.Users.ToListAsync());
    }
