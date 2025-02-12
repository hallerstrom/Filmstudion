using System;

namespace API.Models;

public class Filmstudio : IFilmstudio
{
    public int FilmStudioId { get; set; }
    public required string Name { get; set; }
    public required string City { get; set; }
    // public List <FilmCopy> RentedFilmCopies { get; set; } = new List<FilmCopy>();
}
