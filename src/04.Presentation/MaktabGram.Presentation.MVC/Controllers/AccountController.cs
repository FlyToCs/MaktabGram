using MaktabGram.Domain.ApplicationServices.FollowAgg;
using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.FollowerAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Presentation.MVC.Database;
using MaktabGram.Presentation.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApplicationService userApplicationService;
        private readonly IFollowerApplicationService followApplicationService;


        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var loginResullt = userApplicationService.Login(model.Mobile, model.Password);

            if (loginResullt.IsSuccess)
            {

                InMemoryDatabase.OnlineUser = new OnlineUser
                {
                    Id = loginResullt.Data.Id,
                    IsAdmin = loginResullt.Data.IsAdmin,
                    Username = loginResullt.Data.Username
                };

                if (loginResullt.Data!.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                {
                    return RedirectToAction("Index", "Post");
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

            var registerResult = userApplicationService.Register(userModel);

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

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = 3; // InMemoryDatabase.OnlineUser.Id;
            var profile = userApplicationService.GetProfile(userId,0);

            if (profile is null)
            {
                //...........
            }

            return View(profile);
        }

        [HttpGet]
        public IActionResult Search()
        {
            var userId = 3;

            var results = userApplicationService.Search(string.Empty, userId);
            return View(results);
        }

        [HttpPost]
        public IActionResult Search(string username)
        {
            var userId = 3;

            var results = userApplicationService.Search(username, userId);
            return View(results);
        }


        public IActionResult Follow(int id)
        {
            var userId = 3;
            followApplicationService.Follow(userId, id);

            return RedirectToAction("Search");
        }

        public IActionResult UnFollow(int id)
        {
            var userId = 3;
            followApplicationService.UnFollow(3, id);
            return RedirectToAction("Search");
        }


    }
}
