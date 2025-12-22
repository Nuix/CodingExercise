using InvestmentPerformanceWebApi.DbInfrastructure.Models;
using InvestmentPerformanceWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InvestmentPerformanceWebApi.Api.ViewModels.Investments;

namespace InvestmentPerformanceWebApi.Api.Controllers
{
    [ApiController]
    [Route("users/{userid}/investments")]
    public class UserInvestmentsController : ControllerBase
    {
        private readonly ILogger<UserInvestmentsController> _logger;
        private readonly IMapper _mapper;
        public IInvestmentService _investmentService;

        public UserInvestmentsController(ILogger<UserInvestmentsController> logger, IInvestmentService investmentService, IMapper mapper)
        {
            _logger = logger;
            _investmentService = investmentService;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<InvestmentSummaryView>>> GetInvestmentsForUser(
            int userId)
        {

            try
            {
                var domainInvestments = await _investmentService.GetInvestmentsForUserAsync(userId);
                var summaryDtos = _mapper.Map<IEnumerable<InvestmentSummaryView>>(domainInvestments);

                return Ok(summaryDtos);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Resource not found for userId {userId}");
                return NotFound(new { message = ex.Message });

            }
            catch (Exception ex)
            {
                return HandleException(ex, userId);
            }

        }

        [HttpGet("{investmentId}")]
        public async Task<ActionResult<Investment>> GetInvestmentForUser(int userId, int investmentId)
        {
            try
            {
                var domainInvestment = await _investmentService.GetInvestmentForUserAsync(userId, investmentId);
                var detailedInvestmentDto = _mapper.Map<InvestmentDetailView>(domainInvestment);

                return Ok(detailedInvestmentDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Resource not found for userId: {userId} and investmentId: {investmentId}");
                return NotFound(new {message = ex.Message});

            }
            catch (Exception ex)
            {
                return HandleException(ex, userId);

            }
        }

        private ActionResult HandleException(Exception ex, int userId)
        {
            var traceId = HttpContext.TraceIdentifier;
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            var endpointPath = HttpContext.Request.Path;

            _logger.LogError(ex, $"ERROR: {controllerName} - {actionName}. Error reference TraceId: {traceId}");
            return StatusCode(500, $"Error accessing {endpointPath} for UserId: {userId}. Reference TraceId: {traceId}");

        }
    }
}
