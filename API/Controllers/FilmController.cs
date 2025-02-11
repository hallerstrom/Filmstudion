using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService;
        
        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var film = await _filmService.GetByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> CreateFilmAsync(Film film)
        {
            var createdFilm = await _filmService.CreateFilmAsync(film);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdFilm.FilmId }, createdFilm);
        }
    }
}
