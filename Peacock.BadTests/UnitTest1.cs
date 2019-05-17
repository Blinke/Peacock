using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Peacock.API;
using Peacock.API.Controllers.Movies;
using Peacock.Database;
using Peacock.Database.Repositories;

namespace Peacock.BadTests
{
    [TestFixture]
    public class When_requesting_a_movie
    {
        private MoviesController Controller;
        private Movie InputMovie;
        private API.Controllers.Movies.Models.Movie OutputMovie;


        [OneTimeSetUp]
        public void Init()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Movie, API.Controllers.Movies.Models.Movie>();
            }).CreateMapper();

            var dbContextOptions = new DbContextOptionsBuilder<MovieContext>().UseInMemoryDatabase("TestDb").Options;
            var context = new MovieContext(dbContextOptions);

            InputMovie = new Movie
            {
                ReleaseYear = 2019,
                Title = "Life of Robin"
            };

            var repository = new MovieRepository(context);

            //repository.AddMovie("Life of Robin", 2019).GetAwaiter().GetResult();

            Controller = new MoviesController(mapper, repository);

            OutputMovie = Controller.GetById(1).GetAwaiter().GetResult().Value;

            var allMovies = Controller.GetAll().GetAwaiter().GetResult().Value;
        }


        [Test]
        public void Then_ReleaseYear_is_mapped()
        {


            Assert.AreEqual(InputMovie.ReleaseYear, OutputMovie.ReleaseYear);
        }

        [Test]
        public void Then_Title_is_mapped()
        {
            Assert.AreEqual(InputMovie.Title, OutputMovie.Title);
        }
    }
}
