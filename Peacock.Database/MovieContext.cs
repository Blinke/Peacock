using Microsoft.EntityFrameworkCore;
using System;

namespace Peacock.Database
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Movie> Movies { get; set; }
    }
}
