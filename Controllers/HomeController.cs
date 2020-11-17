using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Labs.Models;

namespace Labs.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Car");
        }
        public IActionResult About()
        {
            return Content("О сайте");
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
