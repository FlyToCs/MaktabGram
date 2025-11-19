using MaktabGram.Domain.ApplicationServices.PostAgg;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Posts
{
    public class IndexModel(IPostApplicationService postApplicationService) : BasePageModel
    {
        public List<GetPostForFeedsDto> Feeds { get; set; }
        public int PostCount { get; set; }

        [BindProperty]
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }


        public void OnGet(int nextPage, int id = 1)
        {
            Page = nextPage != 0 ? nextPage :  id;
            PageSize = 2;

            Feeds = postApplicationService.GetFeedPosts(GetUserId(), Page, PageSize);
            PostCount = postApplicationService.GetPostCount(GetUserId());

            TotalPage = PostCount / PageSize;

        }

        public IActionResult OnGetLike(int id)
        {
            postApplicationService.Like(GetUserId(), id);
            return RedirectToPage("/Posts/Feeds");
        }

        public IActionResult OnGetDisLike(int id)
        {
            postApplicationService.DisLike(GetUserId(), id);
            return RedirectToPage("/Posts/Feeds");
        }
    }
}
