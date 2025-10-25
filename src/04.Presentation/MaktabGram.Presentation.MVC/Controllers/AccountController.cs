using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Presentation.MVC.Database;
using MaktabGram.Presentation.MVC.Models;
using MaktabGram.Services.UserAgg;
using Microsoft.AspNetCore.Mvc;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController()
        {
            userService = new UserService();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var loginResullt = userService.Login(model.Mobile, model.Password);

            if (loginResullt.IsSuccess)
            {
                if (loginResullt.Data!.IsAdmin)
                {
                    InMemoryDatabase.OnlineUser = new OnlineUser
                    {
                        Id = loginResullt.Data.Id,
                        IsAdmin = loginResullt.Data.IsAdmin,
                        Username = loginResullt.Data.Username
                    };
                     
                    return RedirectToAction("Index", "Admin");
                }
                {
                    // Redirect to profile
                }
            }
            else
            {
                ViewBag.Error = loginResullt.Message;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var userModel = new RegisterUserInputDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mobile = model.Mobile,
                Password = model.Password,
                Username = model.Username,
            };

            var registerResult = userService.Register(userModel);

            if (registerResult.IsSuccess)
            {
                //.....
            }
            else
            {
                ViewBag.Error = registerResult.Message;
            }

            return View(model);
        }
    }
}
