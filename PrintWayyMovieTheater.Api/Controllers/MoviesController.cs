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
        public IActionResult Get(int skip)
        {
            return Try(() =>
            {
                int take = 10;
                take = skip + take;
                var movies = _movieService.GetMovies(skip, take);
                return Ok(movies);
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
    }
}
