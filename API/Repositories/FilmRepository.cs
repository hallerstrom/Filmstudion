using System;
using API.Models;

namespace API.Repositories;

public class FilmRepository
{
    private readonly AppDbcontext _context;

    // Konstruktor
    public FilmRepository(AppDbcontext context)
    {
        _context = context;
    }

    // Operationer för att hämta data från databasen
    public async Task<Film> GetByIdAsync(int id) => await _context.Films.FindAsync(id);
    public async Task AddAsync (Film film) => await _context.Films.AddAsync(film);
    public async Task UpdateAsync(Film film) => await _context.SaveChangesAsync();
}
