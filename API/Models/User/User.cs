using System;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class User : IdentityUser
{
    public int UserId { get; set; }
    public string Role { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int FilmStudioId { get; set; }

}
