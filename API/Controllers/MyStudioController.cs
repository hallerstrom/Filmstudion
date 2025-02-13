using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class MyStudioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MyStudioController(AppDbContext context)
        {
            _context = context;
        }

        // 15. GET /api/mystudio/rentals – Hämta aktuella uthyrningar för inloggad filmstudio
        [HttpGet("rentals")]
        [Authorize]
        public async Task<IActionResult> GetRentals()
        {
            var role = User.FindFirst("role")?.Value;
            var userStudioId = User.FindFirst("filmStudioId")?.Value;
            if (role?.ToLower() != "filmstudio")
            {
                return Unauthorized();
            }

            var rentals = await _context.FilmCopies.Where(fc => fc.IsRented).ToListAsync();
            return Ok(rentals);
        }
    }
}
