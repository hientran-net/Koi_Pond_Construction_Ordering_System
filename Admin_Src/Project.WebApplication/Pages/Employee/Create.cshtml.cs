using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using ConstructionOrdering.Service.Service;
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

        [BindProperty]
        public NhanVien NhanVien { get; set; } = new NhanVien();

        public async Task<IActionResult> OnGetAsync()
        {
            string nextId = await GenerateNextEmployeeId();
            NhanVien.MaNhanVien = nextId;
            return Page();
        }


        private async Task<string> GenerateNextEmployeeId()
        {
            try
            {
                var projects = await _nhanVienService.GetAllEmployees();

                if (!projects.Any())
                {
                    return "NV_01";
                }

                var existingNumbers = projects
                    .Select(p => int.Parse(p.MaNhanVien.Split('_')[1]))
                    .OrderBy(x => x)
                    .ToList();

                for (int i = 1; i <= existingNumbers.Count + 1; i++)
                {
                    if (!existingNumbers.Contains(i))
                    {
                        return $"NV_{i:D2}";
                    }
                }

                return $"NV_{existingNumbers.Count + 1:D2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating project ID: {ex.Message}");
                return "NV_01";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var existingEmployee = await _nhanVienService.GetEmployeeById(NhanVien.MaNhanVien);
                if (existingEmployee != null)
                {
                    ModelState.AddModelError("", "Mã nhân viên đã tồn tại!");
                    return Page();
                }

                var result = await _nhanVienService.AddEmployee(NhanVien);
                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
                    return RedirectToPage("./Index");
                }

                ModelState.AddModelError("", "Thêm nhân viên thất bại!");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                return Page();
            }
        }
    }
}