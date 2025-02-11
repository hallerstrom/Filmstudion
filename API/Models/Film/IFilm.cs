using System;

namespace API.Models;

public interface IFilm
{
    int FilmId { get; set; }
    string Title { get; set; }
    int ReleaseYear { get; set; }
    int FilmStudioId { get; set; }
    ICollection<FilmCopy> FilmCopies { get; set; }

}
