using System;

namespace API.Models;

public interface IUserAuthenticate
{
    string UserName { get; set; }
    string Password { get; set; }
}
