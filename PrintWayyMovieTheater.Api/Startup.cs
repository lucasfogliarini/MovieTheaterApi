using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PrintWayyMovieTheater.Domain;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;
using System;

namespace PrintWayyMovieTheater.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAllServices();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrintWayyMovieTheater.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            IMovieRoomService movieRoomService, 
            IMovieService movieService,
            IMovieSessionService movieSessionService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrintWayyMovieTheater.Api v1"));
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedMovieRooms(movieRoomService);
            SeedMovies(movieService);
            SeedSessions(movieSessionService);
        }

        private void SeedMovieRooms(IMovieRoomService movieRoomService, int count = 5)
        {
            for (int i = 1; i <= count; i++)
            {
                movieRoomService.Create(new MovieRoom()
                {
                    Name = "Room " + i,
                    Seats = 50 + i
                });
            }
        }
        private void SeedMovies(IMovieService movieService, int count = 15)
        {
            for (int i = 1; i <= count; i++)
            {
                movieService.Create(new Movie()
                {
                    Title = "Movie " + i,
                    Duration = 120 + i,
                    Description = "In summary ... " + i
                });
            }
        }
        private void SeedSessions(IMovieSessionService movieSessionService, int count = 15)
        {
            for (int i = 1; i <= count; i++)
            {
                var movieSession = new MovieSession
                {
                    MovieId = new Random().Next(1, 15),
                    RoomId = new Random().Next(1, 5),
                    TicketPrice = 30 + i,
                    Audio = (MovieAudio)new Random().Next(1, 3),
                    MotionGraphics = (MotionGraphics)new Random().Next(2,4),
                    PresentationStart = DateTime.Now.AddDays(i),
                };
                movieSessionService.Create(movieSession);
            }
        }
    }
}
