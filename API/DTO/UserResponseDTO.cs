using System;

namespace API.DTO;

public class UserResponseDTO
{
    public string UserName { get; set; }
    public string Role { get; set; }
    public string UserId { get; set; }
    public FilmStudioDTO? FilmStudio { get; set; }

}
