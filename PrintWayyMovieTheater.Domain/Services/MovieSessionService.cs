using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Repositories;
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

            _movieTheaterDbRepository.Add(movieSession);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }
        public int Delete(int movieSessionId)
        {
            var movieSession = Get(movieSessionId);

            _movieTheaterDbRepository.Delete(movieSession);
            var changes = _movieTheaterDbRepository.Commit();
            return changes;
        }

        private MovieSession Get(int movieSessionId)
        {
            var movieSession = _movieTheaterDbRepository.Query<MovieSession>().FirstOrDefault(e => e.Id == movieSessionId);
            if (movieSession == null)
            {
                var message = $"There is no MovieSession with the id '{movieSession.Id}'";
                throw new ValidationException(message);
            }
            return movieSession;
        }
    }
}
