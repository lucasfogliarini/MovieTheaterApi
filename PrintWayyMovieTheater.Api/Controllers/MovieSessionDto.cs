using System;

namespace PrintWayyMovieTheater.Domain.Entities
{
    public class MovieSessionDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string RoomName { get; set; }
        public DateTime PresentationStart { get; set; }
        public DateTime PresentationEnd { get; internal set; }
        public decimal TicketPrice { get; set; }
        public MotionGraphics MotionGraphics { get; set; }
        public MovieAudio Audio { get; set; }
    }
}