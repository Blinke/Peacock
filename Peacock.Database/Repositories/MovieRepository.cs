using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.Database.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> GetById(int id);

        Task<IEnumerable<Movie>> ListMovies();

        Task<IEnumerable<Movie>> SearchByTitle(string searchString);

        Task AddMovie(string title, int releaseYear);

        Task AddMovies(IEnumerable<Movie> movies);

    }

    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        private static bool _dataSeeded = false;

        public MovieRepository(MovieContext context)
        {
            _context = context;

            if(!_dataSeeded)
            {
                _context.Database.EnsureCreated();
                _dataSeeded = true;
            }
        }


        public Task AddMovie(string title, int releaseYear)
        {
            throw new NotImplementedException();
        }

        public async Task AddMovies(IEnumerable<Movie> movies)
        {
            await _context.AddRangeAsync(movies);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.FindAsync<Movie>(id);
        }

        public async Task<IEnumerable<Movie>> SearchByTitle(string searchString)
        {
            return await _context.Movies.Where(m => m.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> ListMovies()
        {
            return await _context.Movies.ToListAsync();
        }
    }
}
