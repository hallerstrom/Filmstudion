using System;
using API.Interfaces;

namespace API.Models.Film;

public class Film : IFilm
{
    public int FilmId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<FilmCopy> FilmCopies { get; set; } = new();
}
