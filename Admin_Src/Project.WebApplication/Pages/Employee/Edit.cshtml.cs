using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.Employee
{
    public class EditModel : PageModel
    {
        private readonly INhanVienService _nhanVienService;

        public EditModel(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [BindProperty]
        public NhanVien NhanVien { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var nhanvien = await _nhanVienService.GetEmployeeById(id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            NhanVien = nhanvien;
            return Page();
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
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Đảm bảo không thay đổi MaNhanVien
                if (existingEmployee.MaNhanVien != NhanVien.MaNhanVien)
                {
                    ModelState.AddModelError("", "Không được phép thay đổi mã nhân viên!");
                    return Page();
                }

                var result = await _nhanVienService.UpdateEmployee(NhanVien);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật nhân viên thành công!";
                    return RedirectToPage("./Index");
                }

                TempData["ErrorMessage"] = "Cập nhật nhân viên thất bại!";
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
