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
            var movieExists = _movieTheaterDbRepository.Query<Movie>().Any(e => e.Title == movie.Title);
            if (movieExists)
            {
                var message = $"There is already a Movie with the Title '{movie.Title}'";
                throw new ValidationException(message);
            }

            _movieTheaterDbRepository.Add(movie);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }
    }
}
