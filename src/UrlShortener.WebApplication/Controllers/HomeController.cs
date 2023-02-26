using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Urls.Commands.CreateUrl;
using UrlShortener.Application.Urls.Queries.GetUrl;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UrlModel urlModel)
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

            _logger.LogInformation("Sending a request to create a short url.");
            var shortUrl = await _mediator.Send(createUrlCommand);

            ViewBag.UrlSchemeAndHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            ViewBag.ShortenedUrl = $"{shortUrl}";

            return View();
        }

        [HttpGet("/{path:required}", Name = "RedirectTo")]
        public async Task<IActionResult> RedirectTo(string path)
        {
            var getUrlQuery = new GetUrlQuery(path);
            var createUrlModel = await _mediator.Send(getUrlQuery);

            return Redirect(createUrlModel.Url);
        }

        public IActionResult TechTaskDetails()
        {
            return View("TechTaskDetails");
        }
    }
}