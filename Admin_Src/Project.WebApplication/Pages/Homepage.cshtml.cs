using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages
{
    public class HomepageModel : PageModel
    {
        private readonly UserService _userService;

        public HomepageModel(UserService userService)
        {
            _userService = userService;
        }

        public string Username { get; private set; }

        public void OnGet(int userId)
        {
            Username = _userService.GetUserNameById(userId);
        }
    }
}
