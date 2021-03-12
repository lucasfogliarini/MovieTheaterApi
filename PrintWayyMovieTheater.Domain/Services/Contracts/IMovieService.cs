using PrintWayyMovieTheater.Domain.Entities;

namespace PrintWayyMovieTheater.Domain.Services
{
    public interface IMovieService
    {
        int Create(Movie movie);
        int Update(Movie movie);
        int Delete(int id);
    }
}
