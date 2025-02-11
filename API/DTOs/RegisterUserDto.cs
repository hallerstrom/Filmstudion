using System;
using API.Models;

namespace API.DTOs;

public class RegisterUserDto :IUserRegister
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

}
