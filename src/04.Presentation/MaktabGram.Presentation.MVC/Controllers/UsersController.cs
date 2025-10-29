using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Domain.UserAgg.Entities;
using MaktabGram.Services.UserAgg;
using Microsoft.AspNetCore.Mvc;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class UsersController  : Controller
    {
        private readonly IUserService userService;
        public UsersController()
        {
            userService = new UserService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = userService.GetUsersSummary();

            return View( users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(RegisterUserInputDto model)
        {
            userService.Register(model);
            return View("Index");
        }
        [HttpGet]
        public IActionResult Active(int userId)
        {
            userService.Active(userId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeActive(int userId)
        {
            userService.DeActive(userId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int userId)
        {
            var result = userService.GetUpdateUserDetails(userId);

            return View(result);
        }

        [HttpPost]
        public IActionResult Update(UpdateGetUserDto model)
        {
            var result = userService.Update(model.Id, model);
            if(result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
