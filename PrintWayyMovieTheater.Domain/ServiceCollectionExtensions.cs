﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrintWayyMovieTheater.Domain.Repositories;

namespace PrintWayyMovieTheater.Domain
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all type of services: logical services, repository services, DbContext, gateway services ...
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddAllServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogicalServices();
            serviceCollection.AddRepositories();
            serviceCollection.AddDbContext<MovieTheaterDbContext>(options => options.UseInMemoryDatabase("MovieTheaterDb"));
            return serviceCollection;
        }
        /// <summary>
        /// Add logical services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddLogicalServices(this IServiceCollection serviceCollection)
        {
        }
        /// <summary>
        /// Add repository services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMovieTheaterDbRepository, MovieTheaterDbRepository>();
        }
    }
}
