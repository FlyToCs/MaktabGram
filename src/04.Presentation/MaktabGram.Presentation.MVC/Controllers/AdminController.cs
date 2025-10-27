using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Presentation.MVC.Database;
using MaktabGram.Services.UserAgg;
using Microsoft.AspNetCore.Mvc;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        public AdminController()
        {
            userService = new UserService();
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
