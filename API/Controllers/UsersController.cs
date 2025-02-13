using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTO;
using API.Interfaces;
using API.Models.Filmstudio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public UsersController(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        // 4. POST /api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
        {
            var user = new IdentityUser { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (model.IsAdmin)
            {
                await _userManager.AddClaimAsync(user, new Claim("role", "admin"));
            }

            var roleClaim = (await _userManager.GetClaimsAsync(user))
                                .FirstOrDefault(c => c.Type == "role")?.Value;
            return Ok(new { user.Id, user.UserName, Role = roleClaim });
        }

        // 5. POST /api/users/authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticateDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Hämta användarens roll (kan vara filmstudio eller admin)
                var roleClaim = (await _userManager.GetClaimsAsync(user))
                                    .FirstOrDefault(c => c.Type == "role")?.Value ?? "filmstudio";

                var filmStudioIdClaim = "";
                IFilmStudio filmStudio = null;  // Fixa.

                if (roleClaim.ToLower() == "filmstudio")
                {
                    //Fixa
                    filmStudioIdClaim = "1"; 
                    filmStudio = new FilmStudio
                    {
                        FilmStudioId = 1, 
                        Name = "StudioNamn",
                        City = "Stad"
                    };
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("SuperDuperUltraMegaSecureJWTSecretKeyThatIsLongEnough!");
        
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "http://localhost:5199",
                    Audience = "http://localhost:5199", 
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("role", roleClaim),
                        new Claim("userId", user.Id),
                        new Claim("filmStudioId", filmStudioIdClaim)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                if (roleClaim.ToLower() == "filmstudio")
                {
                    return Ok(new 
                    {
                        user.Id,
                        user.UserName,
                        Role = roleClaim,
                        Token = tokenString,
                        FilmStudioId = filmStudio.FilmStudioId,
                        FilmStudio = filmStudio // Skicka filmstudio data
                    });
                }

                // Om admin
                return Ok(new { user.Id, user.UserName, Role = roleClaim, Token = tokenString });
            }
            return Unauthorized();
        }

    }
}

