using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace UrlShortener.Application.Urls.Commands.CreateUrl
{
    public class CreateUrlCommand : IRequest<string>
    {
        public string Url { get; set; } = string.Empty;
    }
}
