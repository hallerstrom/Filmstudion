using System;
using API.Interfaces;

namespace API.DTO;

public class CreateFilmDto : ICreateFilm
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int NumberOfCopies { get; set; }
}