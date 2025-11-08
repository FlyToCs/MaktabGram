using MaktabGram.Domain.ApplicationServices.PostAgg;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Services.PostAgg;
using MaktabGram.Presentation.MVC.Database;
using MaktabGram.Presentation.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostApplicationService postApplicationService;

        public PostController()
        {
            postApplicationService = new PostApplicationService();
        }
        public IActionResult Index()
        {
            var posts = postApplicationService.GetFeedPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create(CreatePostInputDto? model)
        {
            return View( model);
        }

        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostInputDto model)
        {
            model.UserId = InMemoryDatabase.OnlineUser.Id;

            var result = postApplicationService.Create(model);

            if(result.IsSuccess)
            {
                return RedirectToAction("Index", model);
            }
            else
            {
                ViewBag.Error = result.Message;
                return RedirectToAction("Create", model);
            }
               
        }
    }
}
