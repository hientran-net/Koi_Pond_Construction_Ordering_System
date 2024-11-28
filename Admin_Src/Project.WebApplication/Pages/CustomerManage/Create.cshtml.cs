using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.CustomerManage
{
    public class CreateModel : PageModel
    {
        private readonly IKhachHangService _khachHangService;

        public CreateModel(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        [BindProperty]
        public KhachHang KhachHang { get; set; } = new KhachHang();

        public async Task<IActionResult> OnGetAsync()
        {
            string nextId = await GenerateNextCustomerId();
            KhachHang.MaKhachHang = nextId;
            return Page();
        }

        private async Task<string> GenerateNextCustomerId()
        {
            try
            {
                var projects = await _khachHangService.GetAllCustomers();
                if (!projects.Any())
                {
                    return "KH_01";
                }
                var existingNumbers = projects
                    .Select(p => int.Parse(p.MaKhachHang.Split('_')[1]))
                    .OrderBy(x => x)
                    .ToList();

                for (int i = 1; i <= existingNumbers.Count + 1; i++)
                {
                    if (!existingNumbers.Contains(i))
                    {
                        return $"KH_{i:D2}";
                    }
                }

                return $"KH_{existingNumbers.Count + 1:D2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating project ID: {ex.Message}");
                return "KH_01";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _khachHangService.AddCustomer(KhachHang);
            if (result)
            {
                TempData["SuccessMessage"] = "Thêm khách hàng thành công!";
                return RedirectToPage("./Index");
            }

            TempData["ErrorMessage"] = "Thêm khách hàng thất bại!";
            return Page();
        }
    }
}
