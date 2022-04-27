using iTechArt.Common.Logging;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Surveys.WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}