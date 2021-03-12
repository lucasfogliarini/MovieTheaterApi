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
    public class MovieServiceTest
    {
        readonly IMovieService _movieService;
        public MovieServiceTest()
        {
            var serviceProvider = new ServiceCollection()
                                           .AddAllServices()
                                           .BuildServiceProvider();
            _movieService = serviceProvider.GetService<IMovieService>();
        }

        [Fact]
        public void Create_ShouldExpectedChanges()
        {
            //Given
            var movie = new Movie
            {
                Title = "Forrest Gump",
                Duration = 144,
                Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                Banner = "https://en.wikipedia.org/wiki/File:Forrest_Gump_poster.jpg#/media/File:Forrest_Gump_poster.jpg"
            };
            var expectedChanges = 1;

            //When
            var changes = _movieService.Create(movie);

            //Then
            Assert.Equal(expectedChanges, changes);
        }

        [Fact]
        public void Create_ShouldThrowException_WhenAlreadyExists()
        {
            //Given
            var movie = new Movie
            {
                Title = "Forrest Gump",
                Duration = 144,
                Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                Banner = "https://en.wikipedia.org/wiki/File:Forrest_Gump_poster.jpg#/media/File:Forrest_Gump_poster.jpg"
            };

            //When
            _movieService.Create(movie);
            void action() => _movieService.Create(movie);

            //Then
            Assert.Throws<ValidationException>(action);
        }
    }
}
