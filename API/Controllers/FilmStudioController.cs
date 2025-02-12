using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
            private readonly AppDbcontext _context;

    public FilmStudioController(AppDbcontext context)
    {
        _context = context;
    }

    // POST api/filmstudio/register
    [HttpPost("register")]
    public IActionResult RegisterFilmStudio([FromBody] Filmstudio filmStudio)
    {
        if (filmStudio == null) return BadRequest("Invalid data");

        _context.Filmstudios.Add(filmStudio);
        _context.SaveChanges();

        return Ok(filmStudio);
    }

    // GET api/filmstudio
    [HttpGet]
    public IActionResult GetFilmStudios()
    {
        var studios = _context.Filmstudios.Select(fs => new 
        {
            fs.FilmStudioId,
            fs.Name,
            fs.City
        }).ToList();

        return Ok(studios);
    }
    }
}
