using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebApplication2.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public string userName { get; set; }
        [BindProperty]
        public string password { get; set; }    
        public void OnGet()
        {
        }

        public void OnPost()
        {
            List<Claim> lst = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Name,userName)
            };

            ClaimsIdentity ci = new ClaimsIdentity(lst, 
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults
                .AuthenticationScheme
                );
            ClaimsPrincipal cp = new ClaimsPrincipal(ci);
            HttpContext.SignInAsync(cp);
        }
    }
}
