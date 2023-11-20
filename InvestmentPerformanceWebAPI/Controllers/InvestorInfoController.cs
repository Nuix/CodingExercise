using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestmentPerformanceWebAPI.Models;

namespace InvestmentPerformanceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorInfoController(InvestorInfoContext context, InvestmentInfoContext contextInvestment) : ControllerBase
    {
        private readonly InvestmentInfoContext _contextInvestment = contextInvestment;
        private readonly InvestorInfoContext _context = context;



        // GET: api/InvestorInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestorInfo>>> GetInvestorInfo()
        {
            return await _context.InvestorInfo.ToListAsync();
        }

        // GET: api/InvestorInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetInvestorInfo(int id)
        {
            var investorInfo = await _context.InvestorInfo.FindAsync(id);
            if (investorInfo == null)
            {
                return NotFound();
            }
            //var investments = await _contextInvestment.InvestmentInfos.FindAsync(id);
            var investments = _contextInvestment.InvestmentInfos.Where(p => p.UserId == id);

            var list = new List<int>();
            foreach (var investment in investments)
            {
                list.Add(investment.InvestmentId);
            }
            var user = new User
            {
                Name = investorInfo?.Name,
                InvestmentList = list
            };



            return user;
        }

        // PUT: api/InvestorInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestorInfo(int id, InvestorInfo investorInfo)
        {
            if (id != investorInfo.UserId)
            {
                return BadRequest();
            }

            _context.Entry(investorInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestorInfoExists(id))
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

        // POST: api/InvestorInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvestorInfo>> PostInvestorInfo(InvestorInfo investorInfo)
        {
            _context.InvestorInfo.Add(investorInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvestorInfo", new { id = investorInfo.UserId }, investorInfo);
        }

        // DELETE: api/InvestorInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestorInfo(int id)
        {
            var investorInfo = await _context.InvestorInfo.FindAsync(id);
            if (investorInfo == null)
            {
                return NotFound();
            }

            _context.InvestorInfo.Remove(investorInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestorInfoExists(int id)
        {
            return _context.InvestorInfo.Any(e => e.UserId == id);
        }
    }
}
