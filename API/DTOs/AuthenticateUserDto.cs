using System;
using API.Models;

namespace API.DTOs;

//  DTO för att autentisera användare
public class AuthenticateUserDto : IUserAuthenticate
{
    public string Username { get; set; }
    public string Password { get; set; }
}
