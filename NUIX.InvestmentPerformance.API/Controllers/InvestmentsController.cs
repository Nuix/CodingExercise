using Microsoft.AspNetCore.Mvc;
using NUIX.InvestmentPerformance.API.DataTransferObjects;
using NUIX.InvestmentPerformance.API.Services;

namespace NUIX.InvestmentPerformance.API.Controllers
{
    [ApiController]
    [Route("api/investments")]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentService investmentService;

        public InvestmentsController(IInvestmentService investmentService)
        {
            this.investmentService = investmentService;
        }

        [HttpGet]
        [Route("api/investments/get-investments")]
        public ActionResult<List<InvestmentSummaryDTO>> GetInvestments(Guid userInvestmentID)
        {
            var investments = investmentService.GetInvestmentsForUser(userInvestmentID);

            if (investments == null || investments.Count() == 0)
                return NotFound("Unable to retrieve data for userInvestmentID:" + userInvestmentID + ". Please try again using a valid userInvestmentID.");

            return Ok(investments);
        }

        [HttpGet("api/investments/get-user-investments-details")]
        public ActionResult<InvestmentDetailDTO> GetUserInvestmentDetails(Guid userInvestmentID, Guid investmentId)
        {
            var details = investmentService.GetInvestmentDetails(userInvestmentID, investmentId);
            if (details == null)
                return NotFound($"Unable to retrieve Investment Details for userInvestmentID:{userInvestmentID} and investmentID:{investmentId}. Please try again using a valid userInvestmentID and investmentID.");

            return Ok(details);
        }

        /// <summary>
        /// Adding this incase the dev team wants to see the middleware
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        //[HttpGet("throw")]
        //public IActionResult ThrowException()
        //{
        //    throw new Exception("This is a test exception");
        //}
    }
}
