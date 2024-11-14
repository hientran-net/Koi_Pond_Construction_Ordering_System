using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            HttpContext.SignOutAsync();
        }

        public void OnPost()
        {
            
        }
    }
}
