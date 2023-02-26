using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Common.Dtos;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Urls.Queries.GetUrl
{
    public class GetUrlQueryHandler : IRequestHandler<GetUrlQuery, UrlManagmentDto>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUrlQueryHandler> _logger;

        public GetUrlQueryHandler(IUrlRepository urlRepository,
                                  IMapper mapper,
                                  ILogger<GetUrlQueryHandler> logger)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UrlManagmentDto> Handle(GetUrlQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Querying for URL with short URL {request.ShortUrl}.");

            UrlManagement? url = await _urlRepository.GetByShortUrlAsync(request.ShortUrl);
            if (url == null)
            {
                _logger.LogError($"Url with short url {request.ShortUrl} does not exist.");
                throw new ArgumentException(nameof(request),
                                                $"Url with short url {request.ShortUrl} does not exist.");
            }

            return _mapper.Map<UrlManagmentDto>(url);
        }
    }
}