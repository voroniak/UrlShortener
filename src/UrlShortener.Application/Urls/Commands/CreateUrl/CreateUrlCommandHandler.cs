using MediatR;
using Microsoft.Extensions.Logging;
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
                _logger.LogInformation($"Short url {url.ShortUrl} for url {url.Url} already exists.");
                
                return url.ShortUrl;
            }


            UrlManagement newUrl;
            UrlManagement urlToChech;
            do
            {
                newUrl = _shortUrlCreator.CreateShortUrl(request.Url);
                urlToChech = await _urlRepository.GetByShortUrlAsync(newUrl.ShortUrl);
                _logger.LogInformation("Checking for created short url, if it is already used by another url.");
            }
            while (urlToChech != null);

            await _urlRepository.InsertAsync(newUrl);

            _logger.LogInformation($"Short url {newUrl.ShortUrl} for url {newUrl.Url} created.");

            return newUrl.ShortUrl;
        }
    }
}
