using Microsoft.EntityFrameworkCore;
using System;

namespace Peacock.Database
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Alien",
                    ReleaseYear = 1979
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Thing",
                    ReleaseYear = 1982
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Matrix",
                    ReleaseYear = 1999
                },
                new Movie
                {
                    Id = 4,
                    Title = "Interstellar",
                    ReleaseYear = 2014
                });
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
