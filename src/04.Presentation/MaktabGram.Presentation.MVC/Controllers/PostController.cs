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
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostInputDto model)
        {
            model.UserId = 3; //InMemoryDatabase.OnlineUser.Id;

            postService.Create(model);
            return View();
        }
    }
}
