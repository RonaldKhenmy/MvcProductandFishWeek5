using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcProduct.UserInterface.Models;

namespace MvcProduct.UserInterface.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/")] //GET: /
        public IActionResult Index()
        {
            return View("~/UserInterface/Views/Home/Index.cshtml");
        }

        // GET: /Privacy
        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View("~/UserInterface/Views/Home/Privacy.cshtml");
        }

        //[HttpGet("Error")] // GET: /Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("~/UserInterface/Views/Error/Index.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
