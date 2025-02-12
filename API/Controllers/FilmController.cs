using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using API;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Testar!");
    }

    [HttpPost("testpost")] // Notera den specifika routen!
    public IActionResult TestPost()
    {
        return Ok("Testar POST!");
    }


    // POST api/film
    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult CreateFilm([FromBody] Film film)
    {
        if (film == null) return BadRequest("Invalid film data");

        _context.Films.Add(film);
        _context.SaveChanges(); 
        
        for (int i = 0; i < film.AvailableCopies; i++) 
        {
            var filmCopy = new FilmCopy { FilmId = film.FilmId };
            _context.FilmCopies.Add(filmCopy); 
        }
        _context.SaveChanges();

        return Ok(film);
    }

    // GET api/film
    [HttpGet]
    public IActionResult GetAllFilms()
    {
        var films = _context.Films.Include(f => f.FilmCopies).ToList();
        return Ok(films);
    }

    // GET api/film/5
    [HttpGet("{id}")]
    public IActionResult GetFilm(int id)
    {
        var film = _context.Films.Include(f => f.FilmCopies).FirstOrDefault(f => f.FilmId == id);
        if (film == null) return NotFound("Film not found");

        return Ok(film);
    }

    [HttpPost("rent")]
    [Authorize(Roles = "filmstudio")]
    public async Task<IActionResult> RentFilm([FromQuery] int id)
    {
        // 1. Enklare hämtning av studioid från Claims (ingen felkontroll)
        var studioid = int.Parse(User.FindFirstValue("studioid")); // Förutsätter att claim finns och är giltigt

        // 2. Hämta filmen (inkludera FilmCopies)
        var film = await _context.Films
            .Include(f => f.FilmCopies)
            .FirstOrDefaultAsync(f => f.FilmId == id);

        if (film == null) return NotFound(); // Enklare felhantering

        // 3. Hitta en ledig kopia (ingen felkontroll)
        var ledigKopia = film.FilmCopies.FirstOrDefault(c => !c.IsRented); // Anpassa IsRented till ditt fältnamn
        if (ledigKopia == null) return Conflict(); // Enklare felhantering

        // 4. Skapa uthyrningen (ingen felkontroll)
        var rental = new Rental  // Se till att du har en Rental-entitet
        {
            FilmCopyId = ledigKopia.FilmCopyId,
            StudioId = studioid,
            RentalDate = DateTime.Now
        };

        ledigKopia.IsRented = true; // Uppdatera filmkopian
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        return Ok(); // Enkelt OK-svar
    }
    }
    
}