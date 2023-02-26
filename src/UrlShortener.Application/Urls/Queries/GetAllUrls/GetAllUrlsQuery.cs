using MediatR;
using UrlShortener.Application.Common.Dtos;

namespace UrlShortener.Application.Urls.Queries.GetAllUrls
{
    public class GetAllUrlsQuery : IRequest<IEnumerable<UrlManagmentDto>>
    {

    }
}
