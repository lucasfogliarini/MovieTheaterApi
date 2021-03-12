using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrintWayyMovieTheater.Domain.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieTheaterDbRepository _movieTheaterDbRepository;

        public MovieService(IMovieTheaterDbRepository movieTheaterDbRepository)
        {
            _movieTheaterDbRepository = movieTheaterDbRepository;
        }

        public int Create(Movie movie)
        {
            ValidateExistence(movie.Title);

            _movieTheaterDbRepository.Add(movie);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }

        public int Update(Movie movie)
        {
            var movieDb = _movieTheaterDbRepository.Query<Movie>().FirstOrDefault(e => e.Id == movie.Id);
            if (movieDb == null)
            {
                var message = $"There is no Movie with the id '{movie.Id}'";
                throw new ValidationException(message);
            }

            if (movie.Title != movieDb.Title)
            {
                ValidateExistence(movie.Title);
            }

            movieDb.Title = movie.Title;
            movieDb.Duration = movie.Duration;
            movieDb.Banner = movie.Banner;
            movieDb.Description = movie.Description;

            _movieTheaterDbRepository.Update(movieDb);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }

        private void ValidateExistence(string movieTitle)
        {
            var movieExists = _movieTheaterDbRepository.Query<Movie>().Any(e => e.Title == movieTitle);
            if (movieExists)
            {
                var message = $"There is already a Movie with the Title '{movieTitle}'";
                throw new ValidationException(message);
            }
        }
    }
}
