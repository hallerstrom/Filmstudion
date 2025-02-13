using API.Models.Film;
using API.Models.Filmstudio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace API.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set; }
        public DbSet<FilmStudio> FilmStudios { get; set; }

        //Konstruktor
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { 
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeda en filmstudio
            modelBuilder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 1, 
                Name = "Test FilmStudio",
                City = "Test City"
            });

            // Seeda 3 filmer
            modelBuilder.Entity<Film>().HasData(
                new Film { FilmId = 1, Title = "Film 1", Description = "Beskrivning av Film 1" },
                new Film { FilmId = 2, Title = "Film 2", Description = "Beskrivning av Film 2" },
                new Film { FilmId = 3, Title = "Film 3", Description = "Beskrivning av Film 3" }
            );

            // Seeda filmkopior 
            modelBuilder.Entity<FilmCopy>().HasData(
                new FilmCopy { FilmCopyId = 1, FilmId = 1, IsRented = false },
                new FilmCopy { FilmCopyId = 2, FilmId = 1, IsRented = false },
                new FilmCopy { FilmCopyId = 3, FilmId = 1, IsRented = false },
                new FilmCopy { FilmCopyId = 4, FilmId = 2, IsRented = false },
                new FilmCopy { FilmCopyId = 5, FilmId = 2, IsRented = false },
                new FilmCopy { FilmCopyId = 6, FilmId = 2, IsRented = false },
                new FilmCopy { FilmCopyId = 7, FilmId = 3, IsRented = false },
                new FilmCopy { FilmCopyId = 8, FilmId = 3, IsRented = false },
                new FilmCopy { FilmCopyId = 9, FilmId = 3, IsRented = false }
            );
        }
    }
}
