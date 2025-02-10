using System;

namespace API.Models.FilmStudio;

public class FilmStudio
{
    public int FilmStudioId { get; set; }
    public required string Name { get; set; }
    public required string City { get; set; }
    public ICollection<FilmCopy> RentedFilmCopies { get; set; } = new List <FilmCopy>();
}
