using System;

namespace API.Models;

public interface IUserAuthenticate
{
    string Username { get; set; }
    string Password { get; set; }
}
