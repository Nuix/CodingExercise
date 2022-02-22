using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuixAPI.Database;
using NuixAPI.Models;

namespace NuixAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly InvDbContext _invDbContext;

        public UsersController(InvDbContext invDbContext)
        {
            _invDbContext = invDbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return _invDbContext.Users.ToList();
        }

        [HttpGet(), Route("{id}/investments")]
        public async Task<IActionResult> GetUserInvestments(long id)
        {
            var investments = _invDbContext.Investments.Where(i => i.UserID == id).Select(i => new { i.InvestmentID, i.Name }).ToList();
            if (investments == null || investments.Count == 0) { return NotFound(); }

            return Ok(investments);
        }

        [HttpGet(), Route("{id}/investments/{investmentID}")]
        public async Task<IActionResult> GetInvestmentDetail(long id, long investmentID)
        {
            var investment =  _invDbContext.Investments.FirstOrDefault(i => i.UserID == id && i.InvestmentID == investmentID);
            if (investment == null) { return NotFound(); }

            return Ok(investment);
        }
    }
}