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
    public class MovieRoomServiceTest
    {
        readonly IMovieRoomService _movieRoomService;
        public MovieRoomServiceTest()
        {
            var serviceProvider = new ServiceCollection()
                                           .AddAllServices()
                                           .BuildServiceProvider();
            _movieRoomService = serviceProvider.GetService<IMovieRoomService>();
        }


        [Fact]
        public void Create_ShouldExpectedChanges()
        {
            //Given
            var room = new MovieRoom
            {
                Name = "Room 3D",
                Seats = 50
            };
            var expectedChanges = 1;
            
            //When
            var changes = _movieRoomService.Create(room);

            //Then
            Assert.Equal(expectedChanges, changes);
        }

        [Fact]
        public void Create_ShouldThrowException_WhenAlreadyExists()
        {
            //Given
            var room = new MovieRoom
            {
                Name = "Room 2D",
                Seats = 50
            };
            _movieRoomService.Create(room);

            //When
            void action() => _movieRoomService.Create(room);

            //Then
            Assert.Throws<ValidationException>(action);
        }

        [Fact]
        public void Get_ShouldExpectedCount()
        {
            //Given
            var expectedCount = 3;
            SeedRooms(expectedCount);

            //When
            var rooms = _movieRoomService.Get();

            //Then
            Assert.True(expectedCount <= rooms.Count());
        }

        private void SeedRooms(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                _movieRoomService.Create(new MovieRoom()
                {
                    Name = "Room " + i,
                    Seats = 50 + i
                });
            }
        }
    }
}
