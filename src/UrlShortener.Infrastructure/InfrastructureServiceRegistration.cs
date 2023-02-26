using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Common.Interfaces.Context;
using UrlShortener.Application.Common.Interfaces.Infrastructure;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Infrastructure.Persistence;
using UrlShortener.Infrastructure.Repositories;
using UrlShortener.Infrastructure.ShortUrl;
namespace UrlShortener.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IDbContext, MongoDbContext>();
            services.AddScoped<IUrlRepository, UrlMongoRepository>();
            services.AddTransient<IShortUrlCreator, ShortUrlCreator>();

            return services;
        }
    }
}
