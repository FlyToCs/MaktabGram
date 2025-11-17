using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Account
{
    public class LoginViewModel
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel(IUserApplicationService userApplicationService, ICookieService cookieService) : BasePageModel
    {
        [BindProperty]
        public LoginViewModel Model { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            if (UserIsLoggedIn())
            {
                 return RedirectToPage("/Account/Profile");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var loginResullt = userApplicationService.Login(Model.Mobile, Model.Password);

            if (loginResullt.IsSuccess)
            {

                cookieService.Set("Id", loginResullt.Data.Id.ToString());
                cookieService.Set("IsAdmin", loginResullt.Data.IsAdmin ? "1" : "0");
                cookieService.Set("Username", loginResullt.Data.Username);

                return RedirectToPage("/Account/Profile");
            }
            else
            {
                Message = loginResullt.Message;
            }

            return Page();
        }
    }
}
