using System;
using API.Interfaces;

namespace API.Models.Film;

public class Film : IFilm
{
    public int FilmId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<FilmCopy> FilmCopies { get; set; } = new();
}
