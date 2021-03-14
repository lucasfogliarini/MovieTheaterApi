using Microsoft.AspNetCore.Mvc;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieSessionsController : BaseController
    {
        private readonly IMovieSessionService _movieSessionService;

        public MovieSessionsController(IMovieSessionService movieSessionService)
        {
            _movieSessionService = movieSessionService;
        }

        [HttpGet]
        public IActionResult Get(int skip)
        {
            return Try(() =>
            {
                int take = 10;
                take = skip + take;
                var sessions = _movieSessionService.GetSessions(skip, take);
                return Ok(sessions);
            });
        }

        [HttpDelete("{sessionId}")]
        public IActionResult Delete(int sessionId)
        {
            return Try(() =>
            {
                var changes = _movieSessionService.Delete(sessionId);
                return Ok(changes);
            });
        }
    }
}
