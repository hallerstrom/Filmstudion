using System;

namespace API.Interfaces;

public interface IFilmCopy
{
    int FilmCopyId { get; set; }
    int FilmId { get; set; }
    bool IsRented { get; set; }
}