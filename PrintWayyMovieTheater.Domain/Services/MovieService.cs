using Microsoft.EntityFrameworkCore;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Repositories;
using System.Collections.Generic;
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
            ValidateTitleExistence(movie.Title);

            _movieTheaterDbRepository.Add(movie);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }
        public int Update(Movie movie)
        {
            var movieDb = Get(movie.Id);

            if (movie.Title != movieDb.Title)
            {
                ValidateTitleExistence(movie.Title);
            }

            movieDb.Title = movie.Title;
            movieDb.Duration = movie.Duration;
            movieDb.Banner = movie.Banner;
            movieDb.Description = movie.Description;

            _movieTheaterDbRepository.Update(movieDb);
            var changes = _movieTheaterDbRepository.Commit();

            return changes;
        }
        public int Delete(int movieId)
        {
            var movie = Get(movieId);
            var hasSessions = movie.Sessions.Any();

            if (hasSessions)
            {
                var message = $"You can't remove a movie with sessions.";
                throw new ValidationException(message);
            }

            _movieTheaterDbRepository.Delete(movie);
            var changes = _movieTheaterDbRepository.Commit();
            return changes;
        }
        public Movie Get(int movieId)
        {
            var movie = _movieTheaterDbRepository.Query<Movie>().Include(e=>e.Sessions).FirstOrDefault(e => e.Id == movieId);
            if (movie == null)
            {
                var message = $"There is no Movie with the id '{movieId}'";
                throw new ValidationException(message);
            }
            return movie;
        }
        public IEnumerable<Movie> GetMovies(int skip, int take = 10)
        {
            var movies = _movieTheaterDbRepository.Query<Movie>().Skip(skip).Take(take);
            return movies;
        }

        private void ValidateTitleExistence(string movieTitle)
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
