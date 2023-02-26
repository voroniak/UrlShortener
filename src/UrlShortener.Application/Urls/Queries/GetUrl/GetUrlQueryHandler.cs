using AutoMapper;
using MediatR;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Urls.Queries.GetUrl
{
    public class GetUrlQueryHandler : IRequestHandler<GetUrlQuery, UrlManagmentDto>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;
        public GetUrlQueryHandler(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }
        public async Task<UrlManagmentDto> Handle(GetUrlQuery request, CancellationToken cancellationToken)
        {
            UrlManagement? url = await _urlRepository.GetByShortUrlAsync(request.ShortUrl);
            if (url == null)
                throw new ArgumentNullException(nameof(url), $"Url with short url {request.ShortUrl} does not exist.");

            return _mapper.Map<UrlManagmentDto>(url);
        }
    }
}
