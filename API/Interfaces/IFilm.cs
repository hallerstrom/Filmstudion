using System;
using API.Models.Film;

namespace API.Interfaces;

public interface IFilm
{
    int FilmId { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    List<FilmCopy> FilmCopies { get; set; }
}
