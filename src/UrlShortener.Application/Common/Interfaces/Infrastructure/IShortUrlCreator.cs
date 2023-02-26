using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Interfaces.Infrastructure
{
    public interface IShortUrlCreator
    {
        UrlManagement CreateShortUrl(string url);
    }
}
