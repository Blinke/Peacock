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

        Task AddMovie(string title, int releaseYear);

        Task AddMovies(IEnumerable<Movie> movies);
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
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
    }
}
