using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestmentPerformanceWebAPI.Models;

namespace InvestmentPerformanceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentInfoController : ControllerBase
    {
        private readonly InvestmentInfoContext _context;

        public InvestmentInfoController(InvestmentInfoContext context)
        {
            _context = context;
        }

        // GET: api/InvestmentInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestmentInfo>>> GetInvestmentInfos()
        {
            return await _context.InvestmentInfos.ToListAsync();
        }

        // GET: api/InvestmentInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentInfo>> GetInvestmentInfo(int id)
        {
            var investmentInfo = await _context.InvestmentInfos.FindAsync(id);

            if (investmentInfo == null)
            {
                return NotFound();
            }

            return investmentInfo;
        }

        // PUT: api/InvestmentInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestmentInfo(int id, InvestmentInfo investmentInfo)
        {
            if (id != investmentInfo.InvestmentId)
            {
                return BadRequest();
            }

            _context.Entry(investmentInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestmentInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InvestmentInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvestmentInfo>> PostInvestmentInfo(InvestmentInfo investmentInfo)
        {
            _context.InvestmentInfos.Add(investmentInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvestmentInfo), new { id = investmentInfo.InvestmentId }, investmentInfo);
        }

        // DELETE: api/InvestmentInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestmentInfo(int id)
        {
            var investmentInfo = await _context.InvestmentInfos.FindAsync(id);
            if (investmentInfo == null)
            {
                return NotFound();
            }

            _context.InvestmentInfos.Remove(investmentInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestmentInfoExists(int id)
        {
            return _context.InvestmentInfos.Any(e => e.InvestmentId == id);
        }
    }
}
