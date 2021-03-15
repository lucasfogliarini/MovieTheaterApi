using Microsoft.AspNetCore.Mvc;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : BaseController
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetMovies(int? skip = null)
        {
            return Try(() =>
            {
                if (skip.HasValue)
                {
                    int take = 10;
                    take = skip.Value + take;
                    var movies = _movieService.GetMovies(skip.Value, take);
                    return Ok(movies);
                }
                else
                {
                    var movies = _movieService.GetMovies();
                    return Ok(movies);
                }
            });
        }

        [HttpDelete("{movieId}")]
        public IActionResult Delete(int movieId)
        {
            return Try(() =>
            {
                var changes = _movieService.Delete(movieId);
                return Ok(changes);
            });
        }

        [HttpGet("{movieId}")]
        public IActionResult Get(int movieId)
        {
            return Try(() =>
            {
                var movie = _movieService.Get(movieId);
                return Ok(movie);
            });
        }

        [HttpPut("{movieId}")]
        public IActionResult Put(int movieId, Movie movie)
        {
            return Try(() =>
            {
                movie.Id = movieId;
                var changes = _movieService.Update(movie);
                return Ok(changes);
            });
        }

        [HttpPost]
        public IActionResult Post(Movie movie)
        {
            return Try(() =>
            {
                var changes = _movieService.Create(movie);
                return Created("movies/"+movie.Id, changes);
            });
        }
    }
}
