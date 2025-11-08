using MaktabGram.Domain.PostAgg.Contracts;
using MaktabGram.Domain.PostAgg.Dtos;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Presentation.MVC.Database;
using MaktabGram.Presentation.MVC.Models;
using MaktabGram.Services.PostAgg;
using MaktabGram.Services.UserAgg;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController()
        {
            postService = new PostService();
        }
        public IActionResult Index()
        {
            var posts = postService.GetFeedPosts();
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

            var result = postService.Create(model);

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
