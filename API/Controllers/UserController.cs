using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
            private readonly AppDbcontext _context;

    public UserController(AppDbcontext context)
    {
        _context = context;
    }

    // POST api/user/register
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] User user)
    {
        if (user == null) return BadRequest("Invalid user data");

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new { user.UserName, user.Role, user.UserId });
    }

    // POST api/user/authenticate
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] IUserAuthenticate request)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == request.UserName && u.Password == request.Password);
        if (user == null) return Unauthorized();

        // Genererar JWT-token

        return Ok(new 
        { 
            user.UserName, 
            user.Role, 
            user.UserId 
        });
    }

    }
}
