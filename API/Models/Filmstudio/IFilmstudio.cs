using System;

namespace API.Models;

public interface IFilmstudio
{   
    int FilmStudioId { get; set; }
    string City { get; set; }
    ICollection<Film> Films { get; set; }
    ICollection<FilmCopy> RentedFilmCopies { get; set; }

}
