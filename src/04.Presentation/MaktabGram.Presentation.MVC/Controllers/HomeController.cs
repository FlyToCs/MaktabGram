using MaktabGram.Presentation.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var temp = Request;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
