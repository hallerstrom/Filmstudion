using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        // Konstruktor
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        // Registrera användare (filmstudio)
        // POST api/auth/register/filmstudio
        [HttpPost("register/filmstudio")]
        public async Task<IActionResult> RegisterFilmstudioAsync([FromBody] RegisterFilmStudioDto dto)
        {
            try
            {
                var user = await _authService.RegisterUserAsync(dto.Username, dto.Password, "Filmstudio");
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Registrera användare (admin)
        // POST api/auth/register/admin
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterUserDto dto)
        {
            var user = await _authService.RegisterUserAsync(dto.Username, dto.Password, "admin");
            return user != null ? Ok(new { user.UserName, user.Role }) : BadRequest();
        }

        // Autentisera användare
        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] AuthenticateUserDto dto)
        {
            var user = await _authService.AuthenticateUserAsync(dto.Username, dto.Password);
            return user != null ? Ok(user) : Unauthorized();
        } 
    }
}
