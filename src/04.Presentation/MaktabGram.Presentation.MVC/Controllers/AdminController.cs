using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Services.UserAgg;
using MaktabGram.Presentation.MVC.Database;
using Microsoft.AspNetCore.Mvc;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserApplicationService userApplicationService;
        public AdminController()
        {
            userApplicationService = new UserApplicationService();
        }

        public IActionResult Index()
        {
            var user = InMemoryDatabase.OnlineUser;

            if (user is not null && user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Unautorize");
            }
        }

        [HttpGet]
        public IActionResult Unautorize()
        {
            return View();
        }

    }
}
