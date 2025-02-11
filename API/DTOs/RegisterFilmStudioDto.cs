using System;
using API.Models;

namespace API.DTOs;

public class RegisterFilmStudioDto :IRegisterFilmStudio
{
    public string Name { get; set; }
    public string City { get; set; }

}
