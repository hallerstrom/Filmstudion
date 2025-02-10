using System;

namespace API.Models;


public class Film
{ 
    public int FilmId { get; set; }
    public required string Title { get; set; }
    public required int ReleaseYear { get; set; }
    public int FilmStudioId { get; set; }
    public Filmstudio Filmstudio { get; set; }
    public ICollection<FilmCopy> FilmCopies { get; set; } = new List<FilmCopy>();

}
