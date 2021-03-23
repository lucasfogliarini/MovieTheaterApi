using FluentValidation;
using PrintWayyMovieTheater.Domain.Entities;
using System;

namespace PrintWayyMovieTheater.Domain.Validation
{
    public class MovieSessionValidator : AbstractValidator<MovieSession>
    {
        public MovieSessionValidator()
        {
            RuleFor(x => x.PresentationStart).GreaterThan(DateTime.Now);
            RuleFor(x => x.TicketPrice).GreaterThan(0);
            RuleFor(x => x.MovieId).NotEmpty();
            RuleFor(x => x.Audio).NotEmpty();
            RuleFor(x => x.MotionGraphics).NotEmpty();
        }
    }
}
