using System;
using API.Models;

namespace API.Repositories.Interfaces;

public interface IFilmRepository
{
    public Task<Film> GetByIdAsync (int id);
    Task AddAsync (Film film);
    Task UpdateAsync (Film film);
}
