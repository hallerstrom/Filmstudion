using System;
using API.Models;

namespace API.DTOs;

public class FilmDto : IFilm
{
    public int FilmId { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public int FilmStudioId { get; set;}
    public ICollection<FilmCopy> FilmCopies { get; set; }

}
