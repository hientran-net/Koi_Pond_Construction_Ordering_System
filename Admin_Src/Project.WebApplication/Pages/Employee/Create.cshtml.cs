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

        [BindProperty]
        public NhanVien NhanVien { get; set; } = new NhanVien();

        public async Task<IActionResult> OnGetAsync()
        {
            // Tạo mã nhân viên tự động
            string nextId = await GenerateNextEmployeeId();
            NhanVien.MaNhanVien = nextId;
            return Page();
        }

        private async Task<string> GenerateNextEmployeeId()
        {
            try
            {
                var employees = await _nhanVienService.GetAllEmployees();
                int count = employees.Count + 1;
                return $"NV_{count:D2}"; // D2 sẽ format số thành 2 chữ số, thêm 0 vào trước nếu cần
            }
            catch
            {
                return "NV_01"; // Trường hợp chưa có nhân viên hoặc có lỗi
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
                // Kiểm tra xem mã đã tồn tại chưa
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