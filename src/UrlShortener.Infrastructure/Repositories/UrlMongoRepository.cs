using MongoDB.Driver;
using UrlShortener.Application.Common.Interfaces.Context;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Repositories
{
    public class UrlMongoRepository : IUrlRepository
    {
        private readonly IDbContext _dbContext;
        public UrlMongoRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UrlManagement>> GetAllUrlsAsync()
        {
            return await _dbContext
                           .UrlManagements
                           .Find(u => true)
                           .ToListAsync();
        }

        public async Task<UrlManagement> GetByLongUrlAsync(string longUrl)
        {
            return await _dbContext
                           .UrlManagements
                           .Find(url => url.Url == longUrl)
                           .FirstOrDefaultAsync();
        }

        public async Task<UrlManagement> GetByShortUrlAsync(string shortUrl)
        {
            return await _dbContext
                           .UrlManagements
                           .Find(url => url.ShortUrl == shortUrl)
                           .FirstOrDefaultAsync();
        }

        public async Task InsertAsync(UrlManagement urlManagement)
        {
            await _dbContext
                .UrlManagements
                .InsertOneAsync(urlManagement);
        }

    }
}
