using System;
using API.Interfaces;

namespace API.DTO;

public class UserAuthenticateDto : IUserAuthenticate
{
    public string Username { get; set; }
    public string Password { get; set; }
}
