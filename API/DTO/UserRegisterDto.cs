using System;
using API.Interfaces;

namespace API.DTO;

public class UserRegisterDto : IUserRegister
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }
}
