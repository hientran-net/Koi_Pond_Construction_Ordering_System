using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        private readonly INhanVienService _nhanVienService;

        public DeleteModel(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [BindProperty]
        public NhanVien NhanVien { get; set; } = new NhanVien();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _nhanVienService.GetEmployeeById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            NhanVien = nhanVien;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy nhân viên!";
                return RedirectToPage("./Index");
            }

            try
            {
                var result = await _nhanVienService.DeleteEmployee(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa nhân viên thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Xóa nhân viên thất bại!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}