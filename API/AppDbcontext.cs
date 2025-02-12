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
    public DbSet<FilmCopy> RentedFilmCopies { get; set; }
    public DbSet<Rental> Rentals { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Film>().HasData(
            new Film { FilmId = 1, Title = "The Shawshank Redemption", AvailableCopies = 5},
            new Film { FilmId = 2, Title = "The Godfather", AvailableCopies = 3 }
        );

    modelBuilder.Entity<Film>()
        .HasMany(f => f.FilmCopies)
        .WithOne()
        .HasForeignKey(c => c.FilmId);

    // Seed data för FilmCopy (separat)
    modelBuilder.Entity<FilmCopy>().HasData(
        new FilmCopy { FilmCopyId = 1, FilmId = 1, IsRented = false }, 
        new FilmCopy { FilmCopyId = 2, FilmId = 1, IsRented = false }, 
        new FilmCopy { FilmCopyId = 3, FilmId = 2, IsRented = false }
    );  
    }




}
