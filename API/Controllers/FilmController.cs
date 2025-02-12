using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using API;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    { 
    private readonly AppDbcontext _context;

    public FilmController(AppDbcontext context)
    {
        _context = context;
    }

    // POST api/film
    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult CreateFilm([FromBody] Film film)
    {
        if (film == null) return BadRequest("Invalid film data");

        _context.Films.Add(film);
        _context.SaveChanges();

        return Ok(film);
    }

    // GET api/film
    [HttpGet]
    public IActionResult GetAllFilms()
    {
        var films = _context.Films.ToList();
        return Ok(films);
    }

    // GET api/film/5
    [HttpGet("{id}")]
    public IActionResult GetFilm(int id)
    {
        var film = _context.Films.FirstOrDefault(f => f.FilmId == id);
        if (film == null) return NotFound("Film not found");

        return Ok(film);
    }
    }
}