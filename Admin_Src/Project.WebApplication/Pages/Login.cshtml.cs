using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Project.WebApplication.Pages
{
    public class loginModel : PageModel
    {
        private readonly AccountService _accountService;

        public loginModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public string userName { get; set; }
        [BindProperty]
        public string password { get; set; }    
        
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            bool isValidUser = _accountService.VerifyPassword(userName, password);
            if (!isValidUser)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            List<Claim> lst = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Name, userName)
            };

            ClaimsIdentity ci = new ClaimsIdentity(lst, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal cp = new ClaimsPrincipal(ci);
            await HttpContext.SignInAsync(cp);

            // Chuyển hướng sau khi đăng nhập thành công
            return RedirectToPage("/Index");
        }
    }
}
