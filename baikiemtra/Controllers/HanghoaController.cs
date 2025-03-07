using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HangHoaManagement.Data;
using HangHoaManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangHoaManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly GoodDbContext _context;

        public HangHoaController(GoodDbContext context)
        {
            _context = context;
        }

        // GET: api/HangHoa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<hang_hoa>>> GetHangHoa()
        {
            return await _context.hang_hoa.ToListAsync();
        }

        // GET: api/HangHoa/ABC123456
        [HttpGet("{id}")]
        public async Task<ActionResult<hang_hoa>> GetHangHoa(string id)
        {
            var hangHoa = await _context.hang_hoa.FindAsync(id);

            if (hangHoa == null)
            {
                return NotFound();
            }

            return hangHoa;
        }

        // GET: api/HangHoa/mahanghoa?ma=ABC123456
        [HttpGet("mahanghoa")]
        public async Task<ActionResult<hang_hoa>> GetHangHoaByQuery([FromQuery] string ma)
        {
            var hangHoa = await _context.hang_hoa.FirstOrDefaultAsync(h => h.ma_hanghoa == ma);

            if (hangHoa == null)
            {
                return NotFound();
            }

            return hangHoa;
        }

        // PUT: api/HangHoa/ABC123456
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHangHoa(string id, hang_hoa hangHoa)
        {
            if (id != hangHoa.ma_hanghoa)
            {
                return BadRequest();
            }

            _context.Entry(hangHoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HangHoaExists(id))
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

        // POST: api/HangHoa
        [HttpPost]
        public async Task<ActionResult<hang_hoa>> PostHangHoa(hang_hoa hangHoa)
        {
            _context.hang_hoa.Add(hangHoa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HangHoaExists(hangHoa.ma_hanghoa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHangHoa", new { id = hangHoa.ma_hanghoa }, hangHoa);
        }

        // DELETE: api/HangHoa/ABC123456
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHangHoa(string id)
        {
            var hangHoa = await _context.hang_hoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            _context.hang_hoa.Remove(hangHoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HangHoaExists(string id)
        {
            return _context.hang_hoa.Any(e => e.ma_hanghoa == id);
        }
    }
}