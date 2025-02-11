using System;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Services;

public class FilmService
{
    private readonly IFilmRepository _filmRepository;
    public FilmService(IFilmRepository filmRepository)
    {
        _filmRepository = filmRepository;
    }

    public async Task<Film> CreateFilmAsync(Film film)
    { 
        await _filmRepository.AddAsync(film);
        return film;
    }

    public async Task<Film> GetByIdAsync(int id)
    {
        return await _filmRepository.GetByIdAsync(id);
    }

}
