using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcProduct.UserInterface.Controllers;

public class HelloWorldController : Controller
{
    
    public IActionResult Index()
    {
        return View("~/UserInterface/Views/HelloWorld/Index.cshtml");
    }

    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View("~/UserInterface/Views/HelloWorld/Welcome.cshtml");
    }
}