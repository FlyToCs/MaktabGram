using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Account
{
    public class ViewModel
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel (IUserApplicationService userApplicationService,ICookieService cookieService) : BasePageModel
    {
        [BindProperty]
        public ViewModel Model { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var loginResullt = userApplicationService.Login(Model.Mobile, Model.Password);

            var userid = GetUserId();

            if (loginResullt.IsSuccess)
            {

                cookieService.Set("Id", loginResullt.Data.Id.ToString());
                cookieService.Set("IsAdmin", loginResullt.Data.IsAdmin ? "1" : "0");
                cookieService.Set("Username", loginResullt.Data.Username);

                if (loginResullt.Data!.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                {
                    return RedirectToAction("Index", "Post");
                }
            }
            else
            {
                Message = loginResullt.Message;
            }

            return Page();
        }
    }
}
