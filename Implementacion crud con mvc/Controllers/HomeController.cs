using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Implementacion_crud_con_mvc.Helpers;
using Implementacion_crud_con_mvc.Models;
using System.Diagnostics;

namespace Implementacion_crud_con_mvc.Controllers
{
    [LoginAuthorize]
    public class HomeController : Controller
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
