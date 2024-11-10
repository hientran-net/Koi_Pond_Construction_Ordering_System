using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AccountService _accountService;

        public LoginModel(AccountService accountService)
        {
            _accountService = accountService;
        }


        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (_accountService.VerifyPassword(Username, Password))
            {
                // Đăng nhập thành công
                return RedirectToPage("/Homepage");
            }

            // Nếu đăng nhập không thành công
            ErrorMessage = "Invalid login attempt.";
            return Page();
        }
    }
}
