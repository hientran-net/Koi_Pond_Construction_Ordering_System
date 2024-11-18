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
                // Lấy toàn bộ dự án từ database
                var projects = await _khachHangService.GetAllCustomers();

                // Nếu chưa có dự án nào
                if (!projects.Any())
                {
                    return "KH_01";
                }

                // Lấy danh sách các số đã sử dụng
                var existingNumbers = projects
                    .Select(p => int.Parse(p.MaKhachHang.Split('_')[1]))
                    .OrderBy(x => x)
                    .ToList();

                // Tìm số còn thiếu đầu tiên
                for (int i = 1; i <= existingNumbers.Count + 1; i++)
                {
                    if (!existingNumbers.Contains(i))
                    {
                        return $"KH_{i:D2}";
                    }
                }

                // Trường hợp dự phòng (không khả thi nhưng vẫn an toàn)
                return $"KH_{existingNumbers.Count + 1:D2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating project ID: {ex.Message}");
                return "KH_01"; // Trường hợp có lỗi
            }
        }

        //private async Task<string> GenerateNextCustomerId()
        //{
        //    try
        //    {
        //        var customerId = await _khachHangService.GetAllCustomers();
        //        int count = customerId.Count + 1;
        //        return $"KH_{count:D2}"; // D2 sẽ format số thành 2 chữ số, thêm 0 vào trước nếu cần
        //    }
        //    catch
        //    {
        //        return "KH_01"; // Trường hợp chưa có nhân viên hoặc có lỗi
        //    }
        //}

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
