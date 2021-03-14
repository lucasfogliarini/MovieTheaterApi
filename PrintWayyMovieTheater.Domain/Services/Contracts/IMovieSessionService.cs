using PrintWayyMovieTheater.Domain.Entities;
using System.Collections.Generic;

namespace PrintWayyMovieTheater.Domain.Services
{
    public interface IMovieSessionService
    {
        int Create(MovieSession movieSession);
        int Delete(int movieSessionId);
        IEnumerable<MovieSession> GetSessions(int skip, int take = 10);
    }
}
