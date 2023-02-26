using MediatR;

namespace UrlShortener.Application.Urls.Queries.GetUrl
{
    public class GetUrlQuery : IRequest<UrlManagmentDto>
    {
        public string ShortUrl { get; set; }
        public GetUrlQuery(string shortUrl)
        {
            ShortUrl = shortUrl;
        }
    }
}