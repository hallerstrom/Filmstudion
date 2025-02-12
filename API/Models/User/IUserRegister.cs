using System;

namespace API.Models;

public interface IUserRegister
{
    string UserName { get; set; }
    string Password { get; set; }
    string Role { get; set; }

}
