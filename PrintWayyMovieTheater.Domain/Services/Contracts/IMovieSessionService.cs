using PrintWayyMovieTheater.Domain.Entities;

namespace PrintWayyMovieTheater.Domain.Services
{
    public interface IMovieSessionService
    {
        int Create(MovieSession movieSession);
        int Delete(int movieSessionId);
    }
}
