using System;
using API.Interfaces;

namespace API.DTO;

public class RegisterFilmStudioDto : IRegisterFilmStudio
{
    public required string Name { get; set; }
    public required string City { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
