using Microsoft.Extensions.DependencyInjection;
using PrintWayyMovieTheater.Domain;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PrintWayyMovieTheater.Tests.Unit
{
    public class MovieSessionServiceTest
    {
        readonly IMovieSessionService _movieSessionService;
        readonly IMovieService _movieService;
        public MovieSessionServiceTest()
        {
            var serviceProvider = new ServiceCollection()
                                           .AddAllServices()
                                           .BuildServiceProvider();
            _movieSessionService = serviceProvider.GetService<IMovieSessionService>();
            _movieService = serviceProvider.GetService<IMovieService>();
        }

        [Fact]
        public void Create_ShouldExpectedChanges()
        {
            //Given
            var movieId = CreateMovie();
            var movieSession = new MovieSession
            {
                MovieId = movieId,
                MotionGraphics = MotionGraphics.ThreeDimensions,
                PresentationStart = DateTime.Now.AddDays(20),
                Room = new MovieRoom
                {
                    Name = "Room 1",
                    Seats = 50
                }
            };
            var expectedChanges = 2;

            //When
            var changes = _movieSessionService.Create(movieSession);

            //Then
            Assert.Equal(expectedChanges, changes);
        }

        private int CreateMovie()
        {
            var movie = new Movie
            {
                Title = "Life Is Beautiful",
                Duration = 116,
                Description = "When an open-minded Jewish librarian and his son become victims of the Holocaust, he uses a perfect mixture of will, humor, and imagination to protect his son from the dangers around their camp."
            };
            _movieService.Create(movie);
            return movie.Id;
        }
    }
}
