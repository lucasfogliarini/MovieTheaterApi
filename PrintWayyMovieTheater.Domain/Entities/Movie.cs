namespace PrintWayyMovieTheater.Domain.Entities
{
    public class Movie : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Banner { get; set; }
    }
}
