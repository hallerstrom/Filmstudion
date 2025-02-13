using System;

namespace API.Interfaces;

public interface ICreateFilm
{
    string Title { get; set; }
    string Description { get; set; }
    int NumberOfCopies { get; set; }
}