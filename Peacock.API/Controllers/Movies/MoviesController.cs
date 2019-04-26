using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Peacock.API.Controllers.Movies.Models;
using Peacock.Database.Repositories;

namespace Peacock.API.Controllers.Movies
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var dbMovie = await _movieRepository.GetById(id);

            if(dbMovie == null)
            {
                return NotFound($"Movie with id {id} not found.");
            }

            return Ok(_mapper.Map<Movie>(dbMovie));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Movie>>> SearchByTitle(string title)
        {
            var dbMovies = await _movieRepository.SearchByTitle(title);

            if(!dbMovies.Any())
            {
                return NotFound($"No movies found matching search {title}");
            }

            return Ok(dbMovies.Select(m =>_mapper.Map<Movie>(m)).ToList());
        }
    }
}
