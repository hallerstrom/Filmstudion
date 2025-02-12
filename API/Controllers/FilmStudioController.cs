using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTO;
using Microsoft.AspNetCore.Identity;


namespace API.Controllers
{
    [Route("api/filmstudio")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly AppDbcontext _context;
        private readonly UserManager<User> _userManager;

    public FilmStudioController(AppDbcontext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // POST api/filmstudio/register
    [HttpPost("register")]
    public async Task<IActionResult> RegisterFilmStudio(RegisterFilmStudioDTO model)
    {
        var filmStudio = new Filmstudio
        {
            Name = model.Name,
            City = model.City
        };

        _context.Filmstudios.Add(filmStudio);
        await _context.SaveChangesAsync();

        var user = new User
        {
            UserName = model.UserName,
            FilmStudioId = filmStudio.FilmStudioId,
            Role = "filmstudio"
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, "filmstudio");

        var response = new UserResponseDTO
        {
            UserName = user.UserName,
            Role = user.Role,
            UserId = user.Id,
            FilmStudio = new FilmStudioDTO
            {
                FilmStudioId = filmStudio.FilmStudioId,
                Name = filmStudio.Name,
                City = filmStudio.City
            }
        };

        return Ok(response);
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
    
    // api/filmstudio/test
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Testar!");
    }
}
}

