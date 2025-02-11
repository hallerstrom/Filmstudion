using System;

namespace API.Models;

public interface IUser
{
    string Username { get; set; }
    string Role { get; set; }
    int UserId { get; set; }

}
