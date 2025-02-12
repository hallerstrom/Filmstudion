using System;

namespace API.Models;

public interface IUser
{
    string Password { get; set; }
    string UserName { get; set; }
    string Role { get; set; }
    int UserId { get; set; }
    int FilmStudioId { get; set; }
}
