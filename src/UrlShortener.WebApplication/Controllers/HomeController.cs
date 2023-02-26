using AutoMapper;
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
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper)
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
        public async Task<ActionResult> Index(UrlModel urlModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", urlModel);
            }
            CreateUrlCommand createUrlCommand = new CreateUrlCommand
            {
                Url = urlModel.Url
            };

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