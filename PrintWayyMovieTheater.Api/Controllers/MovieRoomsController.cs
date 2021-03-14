using Microsoft.AspNetCore.Mvc;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieRoomsController : ControllerBase
    {
        private readonly IMovieRoomService _movieRoomService;

        public MovieRoomsController(IMovieRoomService movieRoomService)
        {
            _movieRoomService = movieRoomService;
        }

        [HttpGet]
        public IEnumerable<MovieRoom> Get()
        {
            return _movieRoomService.Get();
        }
    }
}
