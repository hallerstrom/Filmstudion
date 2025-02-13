using System;
using API.Models.Film;

namespace API.Interfaces;

public interface IFilmStudio
{
    int FilmStudioId { get; set; }
    string Name { get; set; }
    string City { get; set; }
    List<FilmCopy> RentedFilmCopies { get; set; }
}
