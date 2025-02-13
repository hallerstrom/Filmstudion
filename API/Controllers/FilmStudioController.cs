using System.Security.Claims;
using API.Data;
using API.DTO;
using API.Interfaces;
using API.Models.Filmstudio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FilmStudioController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 3. POST /api/filmstudio/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterFilmStudio([FromBody] RegisterFilmStudioDto model)
        {
            var user = new IdentityUser { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Skapa filmstudio
            var filmStudio = new FilmStudio
            {
                Name = model.Name,
                City = model.City
            };

            _context.FilmStudios.Add(filmStudio);
            await _context.SaveChangesAsync();

            // Lägg till filmstudio-rollen och filmstudio-id till användaren
            await _userManager.AddClaimAsync(user, new Claim("role", "filmstudio"));
            await _userManager.AddClaimAsync(user, new Claim("filmStudioId", filmStudio.FilmStudioId.ToString()));

            var roleClaim = (await _userManager.GetClaimsAsync(user))
                            .FirstOrDefault(c => c.Type == "role")?.Value;

            return Ok(new 
            {
                user.Id,
                user.UserName,
                Role = roleClaim,
                FilmStudioId = filmStudio.FilmStudioId.ToString(),
                FilmStudioName = filmStudio.Name,
                FilmStudioCity = filmStudio.City
            });
        }

        // 8. GET /api/filmstudio/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetFilmStudio(int id)
        {
            var filmStudio = await _context.FilmStudios
            .Include(fs => fs.RentedFilmCopies)
            .FirstOrDefaultAsync(fs => fs.FilmStudioId == id);

            if (filmStudio == null)
            {
                return NotFound();
            }

            var userRole = User.FindFirst("role")?.Value;
            var userFilmStudioId = User.FindFirst("filmStudioId")?.Value;

            if (userRole?.ToLower() == "admin" || userFilmStudioId == id.ToString())
            {
                return Ok(filmStudio);
            }
            else
            {
                return Ok(new { filmStudio.FilmStudioId, filmStudio.Name });
            }
        }

        // 7. GET /api/filmstudios
        [HttpGet("/api/filmstudios")]
        public async Task<IActionResult> GetFilmStudios()
        {
            var filmStudios = await _context.FilmStudios.ToListAsync();

            var userRole = User.FindFirst("role")?.Value;

            if (User.Identity.IsAuthenticated && userRole?.ToLower() == "admin")
            {
                return Ok(filmStudios);
            }
            else
            {
                var result = filmStudios.Select(fs => new { fs.FilmStudioId, fs.Name });
                return Ok(result);
            }
        }
    }
}

