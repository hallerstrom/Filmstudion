using System;
using API.Interfaces;

namespace API.DTO;

public class CreateFilmDto : ICreateFilm
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int NumberOfCopies { get; set; }
}