using Microsoft.EntityFrameworkCore;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrintWayyMovieTheater.Domain.Services
{
    public class MovieSessionService : IMovieSessionService
    {
        private readonly IMovieTheaterDbRepository _movieTheaterDbRepository;

        public MovieSessionService(IMovieTheaterDbRepository movieTheaterDbRepository)
        {
            _movieTheaterDbRepository = movieTheaterDbRepository;
        }

        public int Create(MovieSession movieSession)
        {
            var movie = _movieTheaterDbRepository.Query<Movie>().FirstOrDefault(e => e.Id == movieSession.MovieId);
            movieSession.PresentationEnd = movieSession.PresentationStart.AddMinutes(movie.Duration);

            var roomReserved = _movieTheaterDbRepository.Query<MovieSession>()
                                .Any(ms => ms.RoomId == movieSession.RoomId
                                && ((ms.PresentationStart <= movieSession.PresentationStart && movieSession.PresentationStart <= ms.PresentationEnd)
                                || (ms.PresentationStart <= movieSession.PresentationEnd && movieSession.PresentationEnd <= ms.PresentationEnd)));

            if (roomReserved)
            {
                var message = "You can't create a new session in this room, cause it has already been reserved.";
                throw new ValidationException(message);
            }


            _movieTheaterDbRepository.Add(movieSession);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }
        public int Delete(int movieSessionId)
        {
            var movieSession = Get(movieSessionId);
            var deadline = movieSession.PresentationStart.AddDays(-9).Date;
            if (DateTime.Today >= deadline)
            {
                var message = "You can't delete a session 9 days or less before it starts.";
                throw new ValidationException(message);
            }

            _movieTheaterDbRepository.Delete(movieSession);
            var changes = _movieTheaterDbRepository.Commit();
            return changes;
        }
        public IEnumerable<MovieSession> GetSessions(int skip, int take = 10)
        {
            var sessions = _movieTheaterDbRepository.Query<MovieSession>()
                                                    .Include(e => e.Movie)
                                                    .Include(e => e.Room)
                                                    .OrderBy(e => e.PresentationStart)
                                                    .Skip(skip).Take(take);

            return sessions;
        }

        private MovieSession Get(int movieSessionId)
        {
            var movieSession = _movieTheaterDbRepository.Query<MovieSession>().FirstOrDefault(e => e.Id == movieSessionId);
            if (movieSession == null)
            {
                var message = $"There is no MovieSession with the id '{movieSessionId}'";
                throw new ValidationException(message);
            }
            return movieSession;
        }
    }
}
