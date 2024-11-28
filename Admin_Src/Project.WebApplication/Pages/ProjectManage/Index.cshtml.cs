using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.ProjectManage
{
    public class IndexModel : PageModel
    {
        private readonly IDuAnService _duAnService;

        public IndexModel(IDuAnService duAnService)
        {
            _duAnService = duAnService;
        }

        public List<DuAn> DuAns { get; set; } = new List<DuAn>();

        public async Task OnGetAsync()
        {
            try
            {
                Console.WriteLine("Getting projects in Index page..."); 
                DuAns = await _duAnService.GetAllProject();
                Console.WriteLine($"Loaded {DuAns.Count} projects in Index page"); 

                foreach (var project in DuAns)
                {
                    Console.WriteLine($"Project ID: {project.MaDuAn}, Name: {project.TenDuAn}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading projects: {ex.Message}");
                DuAns = new List<DuAn>();
            }
        }

    }
}
