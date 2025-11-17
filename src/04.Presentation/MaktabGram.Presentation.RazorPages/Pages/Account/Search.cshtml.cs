using MaktabGram.Domain.ApplicationServices.FollowAgg;
using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.FollowerAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Domain.Core.UserAgg.Entities;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Account
{
    public class SearchModel (IUserApplicationService userApplicationService,
        IFollowerApplicationService followerApplicationService) : BasePageModel
    {
        public List<SearchResultDto> SearchResult { get; set; }

        public void OnGet()
        {
            SearchResult = userApplicationService.Search(string.Empty, GetUserId());
        }


        [HttpPost]
        public void OnPost(string username)
        {
            SearchResult = userApplicationService.Search(username, GetUserId());
        }


        public IActionResult OnGetFollow(int id)
        {
            followerApplicationService.Follow(GetUserId(), id);
            return RedirectToPage("/Account/Search");
        }

        public IActionResult OnGetUnFollow(int id)
        {
            followerApplicationService.UnFollow(GetUserId(), id);
            return RedirectToPage("/Account/Search");
        }
    }
}
