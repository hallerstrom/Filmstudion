using System;
using API.Interfaces;

namespace API.DTO;

public class RegisterFilmStudioDto : IRegisterFilmStudio
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "filmstudio";
}
