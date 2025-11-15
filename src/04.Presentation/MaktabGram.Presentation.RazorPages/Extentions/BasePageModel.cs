using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Extentions
{
    public class BasePageModel : PageModel
    {
        public int GetUserId()
        {
            if (Request.Cookies.TryGetValue("Id", out var userIdStr) &&
                int.TryParse(userIdStr, out var userIdFromCookie))
            {
                return userIdFromCookie;
            }

           throw new Exception("User is not logged in.");
        }
    }
}
