using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Account
{
    public class ProfileModel (IUserApplicationService userApplicationService) : BasePageModel
    {
        public GetUserProfileDto Profile { get; set; }

        public void OnGet()
        {
            Profile = userApplicationService.GetProfile(GetUserId());
        }
    }
}
