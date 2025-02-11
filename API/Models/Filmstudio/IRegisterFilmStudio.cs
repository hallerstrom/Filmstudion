using System;

namespace API.Models;

public interface IRegisterFilmStudio
{
    string Name { get; set; }
    string City { get; set; }
//     ICollection<Film> Films { get; set; }
//     ICollection<FilmCopy> RentedFilmCopies { get; set; }
//     ICollection<User> Users { get; set; }
}
