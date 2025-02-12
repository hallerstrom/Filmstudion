using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API;

//Ärver från Identity för att använda användarhantering
public class AppDbcontext : IdentityDbContext<User>
{
    //Konstruktor
    public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    //DBSet för tabeller i databasen
    public DbSet<Film> Films { get; set; } 
    public DbSet<FilmCopy> FilmCopies { get; set; } 
    public DbSet<Filmstudio> Filmstudios { get; set; }

    //Konfig av relationer i databasen
    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);

    //     // Film / Filmstudio
    //     builder.Entity<Film>()
    //         .HasOne(f => f.FilmStudio)
    //         .WithMany(fs => fs.Films)
    //         .HasForeignKey(f => f.FilmStudioId);

    //     // FilmCopy / Film
    //     builder.Entity<FilmCopy>()
    //         .HasOne(fc => fc.Film)
    //         .WithMany(f => f.FilmCopies)
    //         .HasForeignKey(fc => fc.FilmId);

    //     // FilmCopy / Filmstudio
    //     builder.Entity<FilmCopy>()
    //         .HasOne(fc => fc.RentedByFilmStudio)
    //         .WithMany(fs => fs.RentedFilmCopies)
    //         .HasForeignKey(fc => fc.RentedByFilmStudioId);
    // // User / Filmstudio
    //     builder.Entity<User>()
    //         .HasOne(u => u.Filmstudio)
    //         .WithMany(fs => fs.Users)
    //         .HasForeignKey(u => u.Id);
    // }





}
