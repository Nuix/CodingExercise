using CodingExercise.Models;
using CodingExercise.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingExercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {

        private readonly IInvestmentService _investmentService;
        private readonly IUserService _userService;

        public InvestmentController(IUserService userService, IInvestmentService investmentService)
        {
            _investmentService = investmentService;
            _userService = userService;
        }

        [HttpGet("GetInvestment/{investmentId}")]
        // Given authentication and authorization, 401 and 403 would also be appropriate
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Investment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Investment> GetInvestment(int investmentId)
        {
            var investment = _investmentService.GetInvestment(investmentId);
            if (investment == null)
            {
                return new NotFoundResult();
            }

            return Ok(investment);
        }

        // Given more time, the results of this route should be paged
        [HttpGet("GetUserInvestments/{userId}")]
        // Given authentication and authorization, 401 and 403 would also be appropriate
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockInvestment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StockInvestment>> GetUserInvestments(int userId)
        {
            var user = _userService.GetUser(userId);
            if (user == null)
            {
                return new NotFoundResult();
            }

            return Ok(_investmentService.GetStockInvestmentsForUser(userId));
        }
    }
}