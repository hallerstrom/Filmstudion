using System;

namespace API.Models;


public class Film : IFilm
{ 
    public int FilmId { get; set; }
    public required string Title { get; set; }
    public int AvailableCopies { get; set; }
    public List<FilmCopy> FilmCopies { get; set; } = new List<FilmCopy>();

}
