using MaktabGram.Domain.ApplicationServices.PostAgg;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Posts
{
    public class CreateModel (IPostApplicationService postApplicationService) : BasePageModel
    {
        [BindProperty]
        public CreatePostInputDto Model { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost() 
        {
            Model.UserId = GetUserId();
            var result = postApplicationService.Create(Model);

            if (result.IsSuccess)
            {
                return RedirectToPage("/Account/Profile");
            }
            else
            {
                Message = result.Message;
                return Page();
            }

        }
    }
}
