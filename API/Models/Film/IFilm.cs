using System;

namespace API.Models;

public interface IFilm
{
    int FilmId { get; set; }
    string Title { get; set; }
    int AvailableCopies { get; set; }
    List<FilmCopy> FilmCopies { get; set; }

}
