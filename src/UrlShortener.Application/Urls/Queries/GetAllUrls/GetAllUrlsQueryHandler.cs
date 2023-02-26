using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Common.Dtos;
using UrlShortener.Application.Common.Interfaces.Repositories;

namespace UrlShortener.Application.Urls.Queries.GetAllUrls
{
    public class GetAllUrlsQueryHandler : IRequestHandler<GetAllUrlsQuery, IEnumerable<UrlManagmentDto>>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUrlsQueryHandler> _logger;

        public GetAllUrlsQueryHandler(IUrlRepository urlRepository,
                                      IMapper mapper,
                                      ILogger<GetAllUrlsQueryHandler> logger)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UrlManagmentDto>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Querying all URLs.");

            var allUrls = await _urlRepository.GetAllUrlsAsync();

            return _mapper.Map<IEnumerable<UrlManagmentDto>>(allUrls);
        }
    }
}
