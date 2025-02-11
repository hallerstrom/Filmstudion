using System;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class User : IdentityUser
{
    public string Role { get; set; }
    public int FilmStudioId { get; set; }
    public Filmstudio Filmstudio { get; set; }
}
