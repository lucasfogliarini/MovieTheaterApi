using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrintWayyMovieTheater.Domain.Services
{
    public class MovieRoomService : IMovieRoomService
    {
        private readonly IMovieTheaterDbRepository _movieTheaterDbRepository;

        public MovieRoomService(IMovieTheaterDbRepository movieTheaterDbRepository)
        {
            _movieTheaterDbRepository = movieTheaterDbRepository;
        }

        public int Create(MovieRoom movieRoom)
        {
            ValidateNameExistence(movieRoom.Name);

            _movieTheaterDbRepository.Add(movieRoom);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }

        public IEnumerable<MovieRoom> Get()
        {
            var rooms = _movieTheaterDbRepository.Query<MovieRoom>().ToList();
            return rooms;
        }

        private void ValidateNameExistence(string roomName)
        {
            var roomExists = _movieTheaterDbRepository.Query<MovieRoom>().Any(e => e.Name == roomName);
            if (roomExists)
            {
                var message = $"There is already a Room with the Name '{roomName}'";
                throw new ValidationException(message);
            }
        }
    }
}
