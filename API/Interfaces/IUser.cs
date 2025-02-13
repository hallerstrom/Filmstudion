using System;

namespace API.Interfaces;

public interface IUser
{
    string UserId { get; set; }
    string Username { get; set; }
    string Role { get; set; }
}
