using Microsoft.AspNetCore.Mvc;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IEnumerable<Movie> Get(int skip)
        {
            int take = 10;
            take = skip + take;
            return _movieService.GetMovies(skip, take);
        }
    }
}
