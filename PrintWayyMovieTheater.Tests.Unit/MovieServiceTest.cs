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
                Title = "Matrix",
                Duration = 136,
                Description = "When a beautiful stranger leads computer hacker Neo to a forbidding underworld, he discovers the shocking truth--the life he knows is the elaborate deception of an evil cyber-intelligence.",
                Banner = ""
            };

            //When
            _movieService.Create(movie);
            void action() => _movieService.Create(movie);

            //Then
            Assert.Throws<ValidationException>(action);
        }

        [Fact]
        public void Update_ShouldExpectedChanges()
        {
            //Given
            var movie = new Movie
            {
                Id = 1,
                Title = "Soul",
                Duration = 100,
                Description = "After landing the gig of a lifetime, a New York jazz pianist suddenly finds himself trapped in a strange land between Earth and the afterlife.",
                Banner = ""
            };
            var expectedChanges = 1;

            //When
            _movieService.Create(movie);
            var changes = _movieService.Update(movie);

            //Then
            Assert.Equal(expectedChanges, changes);
        }
    }
}
