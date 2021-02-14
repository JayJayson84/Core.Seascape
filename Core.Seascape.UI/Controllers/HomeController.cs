using Core.Seascape.BLL.Models;
using Core.Seascape.BLL.Services;
using Core.Seascape.UI.Models.Forms.Seascape;
using Core.Seascape.UI.Models.Layouts;
using Core.Seascape.UI.Models.Pages;
using Core.Seascape.UI.Models.Partials;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Core.Seascape.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly ISeascapeService _seascapeService;
        private readonly IStyleService _styleService;

        public HomeController(
            IWebHostEnvironment environment,
            LinkGenerator linkGenerator,
            IMapper mapper,
            ISeascapeService seascapeService,
            IStyleService styleService)
        {
            _environment = environment;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _seascapeService = seascapeService;
            _styleService = styleService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var wwwPath = _environment.WebRootPath;
            var imageGuid = Guid.NewGuid();

            var stylesheetName = $"{imageGuid}.min.css";
            var stylesheetPath = Path.Combine(wwwPath, "css", "generated", stylesheetName);

            var imageData = _seascapeService.GenerateBase64Data(1920, 1080);
            _styleService.CreateBase64AnimatedCss(imageData, stylesheetPath);

            var relativeStylesheetPath = $"/css/generated/{stylesheetName}";

            var actionUrl = _linkGenerator.GetPathByAction("GenerateImage", "Home");

            var animatedSeascapeImage = new AnimatedSeascapeImageViewModel()
            {
                Guid = imageGuid.ToString()
            };

            return View(
            new LayoutModel<IndexViewModel>(new IndexViewModel()
            {
                AnimatedSeascapeImage = animatedSeascapeImage
            })
            {
                SeascapeForm = new SeascapeForm(actionUrl)
                {
                    AnimatedSeascapeImage = animatedSeascapeImage
                },
                StylesheetPaths = new[] { relativeStylesheetPath }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateImage([Bind(Prefix = "SeascapeForm")] SeascapeForm model)
        {
            var wwwPath = _environment.WebRootPath;
            var imageGuid = Guid.NewGuid();

            var seascapeOptions = _mapper.Map<SeascapeOptions>(model);
            var seascapeModel = new SeascapeModel(1920, 1080, seascapeOptions);

            var stylesheetName = $"{imageGuid}.min.css";
            var stylesheetPath = Path.Combine(wwwPath, "css", "generated", stylesheetName);

            var imageData = _seascapeService.GenerateBase64Data(seascapeModel);
            if (imageData == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Oh no! An error occurred trying to create an image. Have another go, or try changing the attribute values.");
            }

            _styleService.CreateBase64AnimatedCss(imageData, stylesheetPath);

            var viewModel = new AnimatedSeascapeImageViewModel()
            {
                Guid = imageGuid.ToString()
            };

            _styleService.ScheduleCleanup(wwwPath, model.AnimatedSeascapeImage.Guid);

            return PartialView("_AnimatedSeascapeImage", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
