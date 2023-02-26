using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShortener.Application.Common.Interfaces.Context;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Settings;

namespace UrlShortener.Infrastructure.Persistence
{
    public class MongoDbContext : IDbContext
    {
        public MongoDbContext(IOptions<UrlDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            UrlManagements = mongoDatabase.GetCollection<UrlManagement>(options.Value.UrlsCollectionName);
        }
        public IMongoCollection<UrlManagement> UrlManagements { get; }
    }
}
