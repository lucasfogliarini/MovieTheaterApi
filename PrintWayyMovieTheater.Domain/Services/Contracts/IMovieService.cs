using PrintWayyMovieTheater.Domain.Entities;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Domain.Services
{
    public interface IMovieService
    {
        int Create(Movie movie);
        int Update(Movie movie);
        int Delete(int movieId);
        Movie Get(int movieId);
        IEnumerable<Movie> GetMovies(int skip, int take = 10);
    }
}
