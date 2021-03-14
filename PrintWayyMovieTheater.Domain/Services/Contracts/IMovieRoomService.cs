using PrintWayyMovieTheater.Domain.Entities;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Domain.Services
{
    public interface IMovieRoomService
    {
        IEnumerable<MovieRoom> Get();
        int Create(MovieRoom movieRoom);
    }
}
