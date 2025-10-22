using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Domain.UserAgg.Dtos;
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
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var loginResullt = userService.Login(model.Mobile, model.Password);

            if(loginResullt.IsSuccess)
            {
                //......
            }
            else
            {
                ViewBag.Error = loginResullt.Message;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }


        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
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
