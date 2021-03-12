namespace PrintWayyMovieTheater.Domain.Entities
{
    public class MovieRoom : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
    }
}
