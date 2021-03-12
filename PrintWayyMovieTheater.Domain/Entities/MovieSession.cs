﻿using System;

namespace PrintWayyMovieTheater.Domain.Entities
{
    public class MovieSession : IEntity
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public MovieRoom Room { get; set; }
        public DateTime Presentation { get; set; }
        public decimal TicketPrice { get; set; }
        public MotionGraphics MotionGraphics { get; set; }
        public MovieAudio Audio { get; set; }
    }
}