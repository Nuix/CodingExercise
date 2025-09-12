using AutoMapper;
using InvestmentPerformance.DTOs;
using InvestmentPerformance.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentsController(IMapper mapper, IInvestmentRepository investmentRepository)
        {
            _mapper = mapper;
            _investmentRepository = investmentRepository;
        }

        // GET: api/Investments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestmentDto>>> GetInvestments()
        {
            var userId = GetUserId();

            if (!userId.HasValue)
            {
                return BadRequest("UserID header is required.");
            }

            var investments = await _investmentRepository.GetInvestmentsByUserId(userId.Value);

            return _mapper.Map<List<InvestmentDto>>(investments);
        }

        // GET: api/Investments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentDetailsDto>> GetInvestment(int id)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return BadRequest("UserID header is required.");
            }

            var investment = await _investmentRepository.GetInvestmentById(id);

            if (investment == null)
            {
                return NotFound();
            }

            if (investment.UserID != userId)
            {
                return Unauthorized(); // 403 would be better here
            }

            var investmentDto = _mapper.Map<InvestmentDetailsDto>(investment);

            return investmentDto;
        }

        private int? GetUserId()
        {
            HttpContext.Request.Headers.TryGetValue("UserID", out var userIdHeader);

            if (string.IsNullOrEmpty(userIdHeader) || !int.TryParse(userIdHeader, out var userId))
            {
                return null;
            }

            return userId;
        }
    }
}
