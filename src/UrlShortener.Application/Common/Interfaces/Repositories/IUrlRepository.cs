using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Interfaces.Repositories
{
    public interface IUrlRepository
    {
        Task<UrlManagement> GetByLongUrlAsync(string longUrl);
        Task<UrlManagement> GetByShortUrlAsync(string shortUrl);
        Task InsertAsync(UrlManagement urlManagement);
    }
}
