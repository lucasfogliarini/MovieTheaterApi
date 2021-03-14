using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PrintWayyMovieTheater.Domain;
using PrintWayyMovieTheater.Domain.Entities;
using PrintWayyMovieTheater.Domain.Services;

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
            services.AddControllers();
            services.AddAllServices();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrintWayyMovieTheater.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMovieRoomService movieRoomService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrintWayyMovieTheater.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedMovieRooms(movieRoomService);
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
    }
}
