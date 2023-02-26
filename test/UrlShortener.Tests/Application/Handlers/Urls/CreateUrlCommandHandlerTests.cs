using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Application.Common.Interfaces.Infrastructure;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Application.Urls.Commands.CreateUrl;
using UrlShortener.Domain.Entities;
using Xunit;

namespace UrlShortener.Tests.Application.Handlers.Urls
{
    public class CreateUrlCommandHandlerTests
    {
        private readonly Mock<ILogger<CreateUrlCommandHandler>> _logger;
        private readonly Mock<IUrlRepository> _urlRepository;
        private readonly Mock<IShortUrlCreator> _shortUrlCreator;

        public CreateUrlCommandHandlerTests()
        {
            _logger = new Mock<ILogger<CreateUrlCommandHandler>>();
            _urlRepository = new Mock<IUrlRepository>();
            _shortUrlCreator = new Mock<IShortUrlCreator>();
        }

        [Fact]
        public async Task CreateShortUrlIfExist_ReturnsShortUrl()
        {
            string url = "https://www.google.com.ua/";
            string shortUrl = "qwerty12345";
            CreateUrlCommand createUrlCommand = CreateCreateUrlCommand(url);
            UrlManagement urlManagement = CreateUrlManagement(url, shortUrl);
            _urlRepository.Setup(ur => ur.GetByLongUrlAsync(It.IsAny<string>()))
                          .ReturnsAsync(urlManagement);


            var createUrlCommandHandler = new CreateUrlCommandHandler(_logger.Object,
                                                                      _urlRepository.Object,
                                                                      _shortUrlCreator.Object);
            var result = await createUrlCommandHandler.Handle(createUrlCommand, It.IsAny<CancellationToken>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateShortNewUrl_ReturnsShortUrl()
        {
            string url = "https://www.google.com.ua/";
            string shortUrl = "qwerty12345";
            CreateUrlCommand createUrlCommand = CreateCreateUrlCommand(url);
            UrlManagement urlManagement = CreateUrlManagement(url, shortUrl);
            _urlRepository.Setup(ur => ur.GetByLongUrlAsync(It.IsAny<string>()))
                          .ReturnsAsync(() => null);
            _shortUrlCreator.Setup(suc => suc.CreateShortUrl(It.IsAny<string>()))
                            .Returns(urlManagement);
            _urlRepository.Setup(ur => ur.GetByShortUrlAsync(It.IsAny<string>()))
                          .ReturnsAsync(() => null);
            _urlRepository.Setup(ur => ur.InsertAsync(It.IsAny<UrlManagement>()));

            var createUrlCommandHandler = new CreateUrlCommandHandler(_logger.Object,
                                                                      _urlRepository.Object,
                                                                      _shortUrlCreator.Object);
            var result = await createUrlCommandHandler.Handle(createUrlCommand, It.IsAny<CancellationToken>());

            Assert.NotNull(result);
            Assert.Equal(shortUrl, result);
        }

        private UrlManagement CreateUrlManagement(string url, string shortUrl) => new UrlManagement
        {
            Url = url,
            ShortUrl= shortUrl
        };

        private CreateUrlCommand CreateCreateUrlCommand(string url) => new CreateUrlCommand
        {
            Url = url
        };
    }
}
