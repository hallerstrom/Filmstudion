using System;

namespace API.Models;

public interface IFilmCopy
{
    int FilmCopyId { get; set; }
    int FilmId { get; set; }
    bool IsRented  { get; set; }

}
