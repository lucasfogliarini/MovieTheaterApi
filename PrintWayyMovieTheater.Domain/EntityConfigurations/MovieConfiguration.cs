using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintWayyMovieTheater.Domain.Entities;

namespace PrintWayyMovieTheater.Domain.EntityConfigurations
{
    internal sealed class MovieCofiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(e => e.Title).IsRequired();
        }
    }
}
