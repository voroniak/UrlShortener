using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Urls.Commands.CreateUrl;
using UrlShortener.Application.Urls.Queries.GetAllUrls;
using UrlShortener.Application.Urls.Queries.GetUrl;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
                              IMediator mediator,
                              IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(CreateUrlModel urlModel)
        {
            _logger.LogInformation("Checking if the model state is valid.");
            if (!ModelState.IsValid)
            {
                return View("Index", urlModel);
            }

            _logger.LogInformation("Creating CreateUrlCommand.");
            CreateUrlCommand createUrlCommand = new CreateUrlCommand
            {
                Url = urlModel.Url
            };

            _logger.LogInformation("Sending a request to create a short URL.");

            var shortUrl = await _mediator.Send(createUrlCommand);

            ViewBag.UrlSchemeAndHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            ViewBag.ShortenedUrl = $"{shortUrl}";

            _logger.LogInformation($"Short URL {shortUrl} for URL {urlModel.Url} created.");

            return View();
        }

        [HttpGet("/{path:required}", Name = "RedirectTo")]
        public async Task<IActionResult> RedirectTo(string path)
        {
            _logger.LogInformation($"Sending a request to get a original URL for {path}.");

            var getUrlQuery = new GetUrlQuery(path);
            var createUrlModel = await _mediator.Send(getUrlQuery);

            _logger.LogInformation($"Redirecting to {createUrlModel.Url}.");

            return Redirect(createUrlModel.Url);
        }

        public async Task<IActionResult> AllUrls()
        {
            _logger.LogInformation($"Sending a request to get all URLs.");

            var getAllUrlsQuery = new GetAllUrlsQuery();
            var allUrls = _mapper.Map<IEnumerable<GetUrlModel>>(await  _mediator.Send(getAllUrlsQuery));

            allUrls
                .ToList()
                .ForEach(u => u.ShortUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{u.ShortUrl}");

            _logger.LogInformation("Get all URLs request completed.");

            return View("AllUrls",allUrls);
        }

        public IActionResult TechTaskDetails()
        {
            return View("TechTaskDetails");
        }
    }
}