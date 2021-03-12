using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PrintWayyMovieTheater.Domain.Repositories
{
    public class MovieTheaterDbContext : DbContext
    {
        public MovieTheaterDbContext(DbContextOptions<MovieTheaterDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(thisAssembly);
        }
    }
}
