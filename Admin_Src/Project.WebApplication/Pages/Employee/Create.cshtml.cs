using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.Employee
{
    public class CreateModel : PageModel
    {
        private readonly INhanVienService _nhanVienService;

        public CreateModel(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NhanVien NhanVien { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _nhanVienService.AddEmployee(NhanVien);
            if (result)
            {
                TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
                return RedirectToPage("./Index");
            }

            TempData["ErrorMessage"] = "Thêm nhân viên thất bại!";
            return Page();
        }
    }
}