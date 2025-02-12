using System;

namespace API.Models;


public class Film : IFilm
{ 
    public int FilmId { get; set; }
    public required string Title { get; set; }
    public int AvailebleCopies { get; set; }
    public List<FilmCopy> FilmCopies { get; set; }

}
