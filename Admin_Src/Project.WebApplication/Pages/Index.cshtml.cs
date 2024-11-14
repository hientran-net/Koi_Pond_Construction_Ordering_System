using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            HttpContext.SignOutAsync();
        }
    }
}
