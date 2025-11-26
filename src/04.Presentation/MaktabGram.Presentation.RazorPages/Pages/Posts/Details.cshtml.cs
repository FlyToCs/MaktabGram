using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Posts
{
    public class DetailsModel (IPostApplicationService postApplicationService) : PageModel
    {
        public GetPostDetailsDto Post { get; set; }

        public void OnGet(int id)
        {
            Post = postApplicationService.GetPostDetails(id);
        }
    }
}
