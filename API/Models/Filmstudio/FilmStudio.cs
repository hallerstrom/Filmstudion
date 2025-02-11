using System;

namespace API.Models;

public class Filmstudio : IFilmstudio
{
    public int FilmStudioId { get; set; }
    public required string Name { get; set; }
    public required string City { get; set; }
    public ICollection<Film> Films { get; set; } = new List<Film>();
    public ICollection<FilmCopy> RentedFilmCopies { get; set; } = new List <FilmCopy>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
