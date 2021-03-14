using Microsoft.AspNetCore.Mvc;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieSessionsController : ControllerBase
    {
        private readonly IMovieSessionService _movieSessionService;

        public MovieSessionsController(IMovieSessionService movieSessionService)
        {
            _movieSessionService = movieSessionService;
        }

        [HttpGet]
        public IEnumerable<MovieSession> Get(int skip)
        {
            int take = 10;
            take = skip + take;
            return _movieSessionService.GetSessions(skip, take);
        }
    }
}
