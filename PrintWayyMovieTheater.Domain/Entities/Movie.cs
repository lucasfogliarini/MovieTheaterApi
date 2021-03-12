using System.Collections.Generic;

namespace PrintWayyMovieTheater.Domain.Entities
{
    public class Movie : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Duration in minutes
        /// </summary>
        public int Duration { get; set; }
        public string Banner { get; set; }
        public List<MovieSession> Sessions { get; set; }
    }
}
