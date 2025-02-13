using API.Interfaces;

namespace API.Models.Film;

public class FilmCopy : IFilmCopy
{
    public int FilmCopyId { get; set; }
    public int FilmId { get; set; }
    public bool IsRented { get; set; }
}
