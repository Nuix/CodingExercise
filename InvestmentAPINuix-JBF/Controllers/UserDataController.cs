using InvestmentAPINuix_JBF.Classes;
using InvestmentAPINuix_JBF.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAPINuix_JBF.API
{
    [ApiController]
    [Route("~/api/[controller]")]
    public class UserDataController: Controller
    {
        private InvestmentAPIDatabase _dbContext;
        private readonly ILogger<UserDataController> _logger;

        public UserDataController(InvestmentAPIDatabase dbContext, ILogger<UserDataController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // api/userdata
        [HttpGet("")]
        public ActionResult<User> GetAllUsers()
        {
            try
            {
                List<User> users = UserHelper.GetAllUsers(_dbContext);
                if (users == null)
                {
                    //Log that no users were found
                    _logger.LogWarning("No Users Found");
                    return NoContent();
                }
                else
                {
                    
                    return Ok(users);
                }
            }

            catch (Exception ex)
            {
                //Log The Error
                _logger.LogError(ex, "An error occured while fetching the users");


                APIError error = new APIError{ 
                    Status = 500,
                    Error = "An error occured when loading the users",
                    ErrorMessage = ex.Message,
                    Timestamp = DateTime.Now,
                };

                return StatusCode(500, error);

            }

        }

    }
}