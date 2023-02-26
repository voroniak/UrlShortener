using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using UrlShortener.Application.Common.Exceptions;
using UrlShortener.Application.Common.Interfaces.Infrastructure;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Urls.Commands.CreateUrl
{
    public class CreateUrlCommandHandler : IRequestHandler<CreateUrlCommand, string>
    {
        private readonly ILogger<CreateUrlCommandHandler> _logger;
        private readonly IUrlRepository _urlRepository;
        private readonly IShortUrlCreator _shortUrlCreator;

        public CreateUrlCommandHandler(ILogger<CreateUrlCommandHandler> logger,
                                       IUrlRepository urlRepository,
                                       IShortUrlCreator shortUrlCreator)
        {
            _logger = logger;
            _urlRepository = urlRepository;
            _shortUrlCreator = shortUrlCreator;
        }

        public async Task<string> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            UrlManagement? url = await _urlRepository.GetByLongUrlAsync(request.Url);
            if (url != null)
            {
                _logger.LogInformation($"Short URL {url.ShortUrl} for URL {url.Url} already exists.");

                return url.ShortUrl;
            }

            var retry = Policy.Handle<UrlCreationException>()
               .RetryAsync(
                retryCount: 10,
                onRetry: (exception, retryCount, context) =>
                {
                    _logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                }
                );

            UrlManagement newUrl = await retry.ExecuteAsync(async () => await CreateShortUrl(request.Url));
            await _urlRepository.InsertAsync(newUrl);

            _logger.LogInformation($"Short URL {newUrl.ShortUrl} for URL {newUrl.Url} created.");

            return newUrl.ShortUrl;
        }

        private async Task<UrlManagement> CreateShortUrl(string url)
        {
            UrlManagement newUrl;
            UrlManagement urlToChech;

            newUrl = _shortUrlCreator.CreateShortUrl(url);
            urlToChech = await _urlRepository.GetByShortUrlAsync(newUrl.ShortUrl);

            _logger.LogInformation("Checking for created short URL, if it is already used by another URL.");

            if (urlToChech != null)
            {
                _logger.LogError($"Short URL with name {newUrl.ShortUrl} is already used by another URL.");

                throw new UrlCreationException($"Short URL with name {newUrl.ShortUrl} is already used by another URL.");
            }

            return newUrl;
        }
    }
}