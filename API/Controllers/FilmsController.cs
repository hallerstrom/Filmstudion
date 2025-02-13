using API.Data;
using API.DTO;
using API.Interfaces;
using API.Models.Film;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmsController(AppDbContext context)
        {
            _context = context;
        }

        // 6. POST /api/films – Lägg till ny film (endast admin)
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDto model)
        {
            var film = new Film
            {
                Title = model.Title,
                Description = model.Description,
            };

            // Skapa angivet antal exemplar
            for (int i = 0; i < model.NumberOfCopies; i++)
            {
                film.FilmCopies.Add(new FilmCopy { IsRented = false });
            }

            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return Ok(film);
        }

        // 9. GET /api/films – Hämta alla filmer
        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            var films = await _context.Films.Include(f => f.FilmCopies).ToListAsync();

            if (User.Identity.IsAuthenticated)
            {
                return Ok(films);
            }
            else
            {
                var result = films.Select(f => new { f.FilmId, f.Title, f.Description });
                return Ok(result);
            }
        }

        // 10. GET /api/films/{id} – Hämta en enskild film
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            var film = await _context.Films.Include(f => f.FilmCopies)
                                           .FirstOrDefaultAsync(f => f.FilmId == id);
            if (film == null) return NotFound();

            if (User.Identity.IsAuthenticated)
            {
                return Ok(film);
            }
            else
            {
                return Ok(new { film.FilmId, film.Title, film.Description });
            }
        }

        // 11 & 12?. PUT /api/films/{id} – Uppdatera film - admin
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] CreateFilmDto model)
        {
            var film = await _context.Films.Include(f => f.FilmCopies)
                                           .FirstOrDefaultAsync(f => f.FilmId == id);
            if (film == null) return NotFound();

            film.Title = model.Title;
            film.Description = model.Description;

            await _context.SaveChangesAsync();
            return Ok(film);
        }

        // 13. POST /api/films/rent?id={id}&studioid={studioid} - filmstudio
        [HttpPost("rent")]
        [Authorize(Roles = "filmstudio")]
        public async Task<IActionResult> RentFilm([FromQuery] int id, [FromQuery] int studioid)
        {
            var role = User.FindFirst("role")?.Value;
            var userStudioId = User.FindFirst("filmStudioId")?.Value;

            if (role?.ToLower() != "filmstudio" || userStudioId != studioid.ToString())
            {
                return Unauthorized();
            }

            var film = await _context.Films.Include(f => f.FilmCopies)
                                           .FirstOrDefaultAsync(f => f.FilmId == id);
            if (film == null)
                return StatusCode(409, "Film hittas inte");

            var availableCopy = film.FilmCopies.FirstOrDefault(fc => !fc.IsRented);
            if (availableCopy == null)
                return StatusCode(409, "Inga lediga exemplar!");

            if (film.FilmCopies.Any(fc => fc.IsRented))
                return StatusCode(403, "Du har redan hyrt denna film!");

            availableCopy.IsRented = true;
            await _context.SaveChangesAsync();
            return Ok("Filmen hyrdes!");
        }

        // 14. POST /api/films/return?id={id}&studioid={studioid} 
        [HttpPost("return")]
        [Authorize]
        public async Task<IActionResult> ReturnFilm([FromQuery] int id, [FromQuery] int studioid)
        {
            var role = User.FindFirst("role")?.Value;
            var userStudioId = User.FindFirst("filmStudioId")?.Value;

            if (role?.ToLower() != "filmstudio" || userStudioId != studioid.ToString())
            {
                return Unauthorized();
            }

            var film = await _context.Films.Include(f => f.FilmCopies)
                                           .FirstOrDefaultAsync(f => f.FilmId == id);
            if (film == null)
                return StatusCode(409, "Film hittas inte");

            var rentedCopy = film.FilmCopies.FirstOrDefault(fc => fc.IsRented);
            if (rentedCopy == null)
                return StatusCode(409, "Du har inte hyrt denna film");

            rentedCopy.IsRented = false;
            await _context.SaveChangesAsync();
            return Ok("Du lämnade tillbaka filmen!");
        }
}
