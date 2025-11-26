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
    public class ProfileModel(IUserApplicationService userApplicationService,
        IFollowerApplicationService followerApplicationService) : BasePageModel
    {
        public GetUserProfileDto Profile { get; set; }

        public void OnGet(int? userId)
        {
            Profile = userApplicationService.GetProfile((int)userId, GetUserId());
        }

        public IActionResult OnGetFollow(int id)
        {
            followerApplicationService.Follow(GetUserId(), id);
            return RedirectToPage("/Account/Profile", new  { userId = id });
        }

        public IActionResult OnGetUnFollow(int id)
        {
            followerApplicationService.UnFollow(GetUserId(), id);
            return RedirectToPage("/Account/Profile", new { userId = id });
        }
    }
}
