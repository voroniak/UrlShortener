using MediatR;

namespace UrlShortener.Application.Urls.Commands.CreateUrl
{
    public class CreateUrlCommand : IRequest<string>
    {
        public string Url { get; set; }
    }
}