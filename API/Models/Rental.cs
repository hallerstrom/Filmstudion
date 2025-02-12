using System;

namespace API.Models;

public class Rental
{
    public int RentalId { get; set; }
    public int FilmCopyId { get; set; }
    public int StudioId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Nullable om filmen inte är returnerad ännu

    public FilmCopy FilmCopy { get; set; }
    public Filmstudio Filmstudio { get; set; } 

}
