using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class InvestmentPerformanceController : ControllerBase
    {
        private readonly IInvestmentPerformanceService _investmentPerformanceService;
        private readonly ILogger<InvestmentPerformanceController> _logger;
        public InvestmentPerformanceController(IInvestmentPerformanceService investmentPerformanceService, ILogger<InvestmentPerformanceController> logger) {
            _investmentPerformanceService = investmentPerformanceService;
            _logger = logger;
        }

        [HttpGet("user/{userId}")] //api/investments/user
        ///<summary>
        ///Get Investments list for a given user Id
        ///</summary>
        ///<returns> a list of type UserInvestment DTO</returns>
        public ActionResult<List<UserInvestment>> GetInvestmentslist(int userId)
        {     
            try
            {
                if(userId <= 0) 
                    throw new ArgumentOutOfRangeException("user id parameter should be greater than 0");

                var investmentsList = _investmentPerformanceService.GetInvestmentsListByUserId(userId);

                if(investmentsList == null || !investmentsList.Any())
                {
                    _logger.LogWarning($"No data found for user id: {userId}");
                    return NotFound();
                }
                return StatusCode((int)HttpStatusCode.OK, investmentsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet("{investmentId}")] //api/investments/
        ///<summary>
        ///Get the details list for a given investment Id
        ///</summary>
        ///<returns> a list of type UserInvestment DTO</returns>
        public ActionResult<List<object>> GetInvestmentDetails(int investmentId)
        {
            try
            {
                if (investmentId <= 0)
                    throw new ArgumentOutOfRangeException("user id parameter should be greater than 0");

                var investmentDetails = _investmentPerformanceService.GetInvestmentDetailsById(investmentId);
                if(investmentDetails == null || !investmentDetails.Any())
                {
                    _logger.LogWarning($"No data found for investment id: {investmentId}");
                    return NotFound();
                }
                return StatusCode((int)HttpStatusCode.OK, investmentDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }      
        }
    }
}
