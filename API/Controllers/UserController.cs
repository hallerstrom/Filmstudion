using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using API.DTO;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
 [ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbcontext _context;

    public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbcontext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    // Registrera en admin
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        var user = new User
        {
            UserName = model.UserName,
            Role = model.IsAdmin ? "admin" : "filmstudio"
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, user.Role);

        var response = new UserResponseDTO
        {
            UserName = user.UserName,
            Role = user.Role,
            UserId = user.Id
        };

        return Ok(response);
    }

    // Autentisera en användare (admin eller filmstudio)
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(UserAuthenticateDTO model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Unauthorized();

        var token = GenerateJwtToken(user);

        var response = new UserResponseDTO
        {
            UserName = user.UserName,
            Role = user.Role,
            UserId = user.Id
        };

        if (user.Role == "filmstudio")
        {
            var filmStudio = await _context.Filmstudios.FindAsync(user.FilmStudioId);
            if (filmStudio != null)
            {
                response.FilmStudio = new FilmStudioDTO
                {
                    FilmStudioId = filmStudio.FilmStudioId,
                    Name = filmStudio.Name,
                    City = filmStudio.City
                };
            }
        }

        return Ok(new { Token = token, User = response });
    }

    private string GenerateJwtToken(User user)
{
    var key = Encoding.UTF8.GetBytes("supersecureandlongenoughkeythatis32bytes"); 
    
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim("UserId", user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role)
    };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
}

}
