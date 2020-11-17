using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Labs.Models;
using System;
using RestSharp;

namespace Labs.Controllers
{
    public class HomeController : Controller
    {
        [Obsolete]
        public HomeController()
        {
            /*
            if (Request.Cookies["Key"] != null)
            {
                var cookie = new HttpCookie()
                {
                    Name="Key",
                    Expires = DateTime.Now.AddDays(-1d)
                };

                Response.Cookies.Append("Key", null);
            }*/
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
