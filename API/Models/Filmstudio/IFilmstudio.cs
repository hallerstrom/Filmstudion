using System;

namespace API.Models;

public interface IFilmstudio
{   
    int FilmStudioId { get; set; }
    string Name { get; set; }
    string City { get; set; }
    // List<FilmCopy> RentedFilmCopies { get; set; }

}
