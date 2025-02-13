using System;
using API.Interfaces;

namespace API.DTO;

public class UserAuthenticateDto : IUserAuthenticate
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
