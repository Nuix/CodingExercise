using System;
using Microsoft.AspNetCore.Mvc;
using InvestmentPerformance.Models;
using InvestmentPerformance.Services;

namespace InvestmentPerformance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public InvestController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("userid")]
        public async Task<IActionResult> GetInvestmentsById(Guid userid)
        {
            var users = await _userRepository.GetInvestmentByIdAsync(userid);

            if (users == null || !users.Any())
            {
                return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
            }

            return StatusCode(StatusCodes.Status200OK, users);

        }

        [HttpGet("userId/investmentId")]
        public async Task<IActionResult> GetInvestmentDetails(Guid userId, Guid investmentId)
        {
            var users = await _userRepository.GetInvestmentDetailsAsync(userId, investmentId);

            if (users == null || !users.Any())
            {
                return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
            }

            return StatusCode(StatusCodes.Status200OK, users);

        }


    }
}

