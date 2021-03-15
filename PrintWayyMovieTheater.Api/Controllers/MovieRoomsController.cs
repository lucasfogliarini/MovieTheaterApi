using Microsoft.AspNetCore.Mvc;
using PrintWayyMovieTheater.Domain.Services;

namespace PrintWayyMovieTheater.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieRoomsController : BaseController
    {
        private readonly IMovieRoomService _movieRoomService;

        public MovieRoomsController(IMovieRoomService movieRoomService)
        {
            _movieRoomService = movieRoomService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Try(() =>
            {
                var rooms =_movieRoomService.Get();
                return Ok(rooms);
            });
        }
    }
}
