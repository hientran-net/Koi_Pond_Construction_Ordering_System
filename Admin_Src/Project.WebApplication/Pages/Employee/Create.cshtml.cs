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
            // Tạo mã nhân viên tự động
            string nextId = await GenerateNextEmployeeId();
            NhanVien.MaNhanVien = nextId;
            return Page();
        }

        //private async Task<string> GenerateNextEmployeeId()
        //{
        //    try
        //    {
        //        var employees = await _nhanVienService.GetAllEmployees();
        //        int count = employees.Count + 1;
        //        return $"NV_{count:D2}"; // D2 sẽ format số thành 2 chữ số, thêm 0 vào trước nếu cần
        //    }
        //    catch
        //    {
        //        return "NV_01"; // Trường hợp chưa có nhân viên hoặc có lỗi
        //    }
        //}

        private async Task<string> GenerateNextEmployeeId()
        {
            try
            {
                // Lấy toàn bộ dự án từ database
                var projects = await _nhanVienService.GetAllEmployees();

                // Nếu chưa có dự án nào
                if (!projects.Any())
                {
                    return "NV_01";
                }

                // Lấy danh sách các số đã sử dụng
                var existingNumbers = projects
                    .Select(p => int.Parse(p.MaNhanVien.Split('_')[1]))
                    .OrderBy(x => x)
                    .ToList();

                // Tìm số còn thiếu đầu tiên
                for (int i = 1; i <= existingNumbers.Count + 1; i++)
                {
                    if (!existingNumbers.Contains(i))
                    {
                        return $"NV_{i:D2}";
                    }
                }

                // Trường hợp dự phòng (không khả thi nhưng vẫn an toàn)
                return $"NV_{existingNumbers.Count + 1:D2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating project ID: {ex.Message}");
                return "NV_01"; // Trường hợp có lỗi
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