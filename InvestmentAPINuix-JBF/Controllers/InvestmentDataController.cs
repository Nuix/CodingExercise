using InvestmentAPINuix_JBF.Classes;
using InvestmentAPINuix_JBF.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAPINuix_JBF.API
{
    [ApiController]
    [Route("~/api/[controller]")]
    public class InvestmentDataController: Controller
    {
        private InvestmentAPIDatabase _dbContext;
        private readonly ILogger<InvestmentDataController> _logger;

        public InvestmentDataController(InvestmentAPIDatabase dbContext, ILogger<InvestmentDataController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;   
        }

        // api/investmentdata/userId
        [HttpGet("{userId}")]
        public ActionResult<Investment> GetAllInvestmentsForUser(int userId)
        {
            try
            {
                List<Investment> investment = InvestmentHelper.GetInvestmentByUser(_dbContext, userId);
                if (investment == null || !investment.Any())
                {
                    _logger.LogWarning("No Investments Found for UserId: " + userId);
                    return NoContent();
                }
                else
                {
                    return Ok(investment);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when searching for invesments for UserId: " + userId);
                APIError error = new APIError{ 
                    Status = 500,
                    Error = "An error occured when searching for  investments for UserId: " + userId,
                    ErrorMessage = ex.Message,
                    Timestamp = DateTime.Now,
                };

                return StatusCode(500, error);

            }

        }
        // api/investmentdata/nameIdOnly/userId
        [HttpGet("nameIdOnly/{userId}")]
        public ActionResult<Investment> GetNameIdFromOfInvestment(int userId)
        {
            try
            {
                List<Object> investment = InvestmentHelper.GetOnlyInvestmentById(_dbContext, userId);
                if (investment == null)
                {
                    _logger.LogWarning("No Investments Found for UserId: " + userId);
                    return NoContent();
                }
                else
                {
                    return Ok(investment);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when searching for invesments for UserId: " + userId);
                APIError error = new APIError{ 
                    Status = 500,
                    Error = "An error occured when searching for  investments for UserId: " + userId,
                    ErrorMessage = ex.Message,
                    Timestamp = DateTime.Now,
                };

                return StatusCode(500, error);

            }

        }
        // api/investmentdata/details/invId
        [HttpGet("details/{invId}")]
        public ActionResult<Investment> GetDetailsForInvestment(int invId)
        {
            try
            {
                List<Investment> investment = InvestmentHelper.GetInvestmentDetailsById(_dbContext, invId);
                if (investment == null)
                {
                    _logger.LogWarning("No Investment Details Found with this id: " + invId);
                    return NoContent();
                }
                else
                {
                    return Ok(investment);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when searching for details on investment id: " + invId);
                APIError error = new APIError{ 
                    Status = 500,
                    Error = "An error occured when searching for details for investment id: " + invId,
                    ErrorMessage = ex.Message,
                    Timestamp = DateTime.Now,
                };

                return StatusCode(500, error);

            }

        }

    }
}