using MainSite.Repositories.Entities;
using MainSite.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainSite.WebApplication.Pages
{
    public class DuAnModel : PageModel
    {
        private readonly IDuAnService _duAnService;

        public DuAnModel(IDuAnService duAnService)
        {
            _duAnService = duAnService;
        }

        public List<DuAn> DuAns { get; set; } = new List<DuAn>();

        public async Task OnGetAsync()
        {
            try
            {
                DuAns = await _duAnService.GetAllProject();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading projects: {ex.Message}");
                DuAns = new List<DuAn>();
            }
            
        }
    }
}
