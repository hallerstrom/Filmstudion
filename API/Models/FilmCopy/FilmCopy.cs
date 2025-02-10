using System;

namespace API.Models;

public class FilmCopy
{
    public int FilmCopyId { get; set; }
    public int FilmId { get; set; }
    public Film Film { get; set; }
    public int RentedByFilmStudioId { get; set; }
    public Filmstudio RentedByFilmStudio { get; set; }


}
