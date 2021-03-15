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
                Title = "The Matrix",
                Duration = 136,
                Description = "When a beautiful stranger leads computer hacker Neo to a forbidding underworld, he discovers the shocking truth--the life he knows is the elaborate deception of an evil cyber-intelligence.",
                Banner = ""
            };
            _movieService.Create(movie);

            //When
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
                Title = "Soul",
                Duration = 100,
                Description = "After landing the gig of a lifetime, a New York jazz pianist suddenly finds himself trapped in a strange land between Earth and the afterlife.",
                Banner = ""
            };
            var expectedChanges = 1;
            _movieService.Create(movie);

            //When
            var changes = _movieService.Update(movie);

            //Then
            Assert.Equal(expectedChanges, changes);
        }

        [Fact]
        public void Delete_ShouldExpectedChanges()
        {
            //Given
            var movie = new Movie
            {
                Title = "The Green Mile",
                Duration = 189,
                Description = "The lives of guards on Death Row are affected by one of their charges: a black man accused of child murder and rape, yet who has a mysterious gift.",
                Banner = ""
            };
            var expectedChanges = 1;
            _movieService.Create(movie);

            //When
            var changes = _movieService.Delete(movie.Id);

            //Then
            Assert.Equal(expectedChanges, changes);
        }

        [Fact]
        public void Delete_ShouldThrowException_WhenItHasSessions()
        {
            //Given
            var movie = new Movie
            {
                Title = "Saving Private Ryan",
                Duration = 169,
                Description = "Following the Normandy Landings, a group of U.S. soldiers go behind enemy lines to retrieve a paratrooper whose brothers have been killed in action.",
                Banner = "",
                Sessions = new List<MovieSession>
                {
                    new MovieSession
                    {
                        MotionGraphics = MotionGraphics.ThreeDimensions,
                        PresentationStart = DateTime.Now.AddDays(20),
                        Room = new MovieRoom
                        {
                            Name = "Room 1",
                            Seats = 2
                        }
                    }
                }
            };
            _movieService.Create(movie);

            //When
            void action() => _movieService.Delete(movie.Id);

            //Then
            Assert.Throws<ValidationException>(action);
        }

        [Fact]
        public void GetMovies_ShouldExpectedCount()
        {
            //Given
            SeedMovies();
            var skip = 0;
            var expectedCount = 10;

            //When
            var movies = _movieService.GetMovies(skip, expectedCount);

            //Then
            Assert.Equal(expectedCount, movies.Count());
        }

        private void SeedMovies(int count = 15)
        {
            for (int i = 1; i <= count; i++)
            {
                _movieService.Create(new Movie()
                {
                    Title = "Movie "+ i,
                    Duration = 120 + i,
                    Description = "In summary ... " + i
                });
            }
        }
    }
}
