using MaktabGram.Domain.ApplicationServices.PostAgg;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Posts
{
    public class IndexModel (IPostApplicationService postApplicationService): BasePageModel
    {
        public List<GetPostForFeedsDto> Feeds { get; set; }
        public void OnGet()
        {
            Feeds = postApplicationService.GetFeedPosts(GetUserId());
        }
    }
}
