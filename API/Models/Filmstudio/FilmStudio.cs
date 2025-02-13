using System;
using API.Interfaces;
using API.Models.Film;

namespace API.Models.Filmstudio;

    public class FilmStudio : IFilmStudio
    {
        public int FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; } = new();
    }
