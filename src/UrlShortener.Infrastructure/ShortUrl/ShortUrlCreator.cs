using Microsoft.Extensions.Logging;
using UrlShortener.Application.Common.Interfaces.Infrastructure;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.ShortUrl
{
    public class ShortUrlCreator : IShortUrlCreator
    {
        private readonly ILogger<ShortUrlCreator> _logger;
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        public ShortUrlCreator(ILogger<ShortUrlCreator> logger)
        {
            _logger = logger;
        }

        public UrlManagement CreateShortUrl(string url)
        {
            Random random = new Random();
            IEnumerable<string>? a = Enumerable.Repeat(CHARS, 10);
            IEnumerable<char>? b = a.Select(str => str[random.Next(CHARS.Length)]);
            string shortUrl = new string
                (
                Enumerable.Repeat(CHARS, 10)
                .Select(str => str[random.Next(CHARS.Length)]).ToArray()
                );
            _logger.LogInformation($"Creating short url {shortUrl} for url {url}");

            return new UrlManagement
            {
                Url = url,
                ShortUrl = shortUrl
            };
        }
    }
}
