using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.ProjectManage
{
    public class CreateModel : PageModel
    {
        private readonly IDuAnService _duAnService;

        public CreateModel(IDuAnService duAnService)
        {
            _duAnService = duAnService;
        }

        [BindProperty]
        public DuAn DuAn { get; set; } = new DuAn();


        //private async Task<string> GenerateNextProjectId()
        //{
        //    try
        //    {
        //        var today = DateTime.Now;
        //        var yearMonth = today.ToString("yyyyMM");
        //        var projects = await _duAnService.GetAllProject();

        //        // Lọc các dự án trong tháng hiện tại
        //        var projectsThisMonth = projects
        //            .Where(p => p.MaDuAn.Contains(yearMonth))
        //            .ToList();

        //        if (!projectsThisMonth.Any())
        //        {
        //            return $"DA_{yearMonth}";
        //        }

        //        // Tìm số sequence lớn nhất trong tháng
        //        int maxSequence = projectsThisMonth
        //            .Select(p => int.Parse(p.MaDuAn.Split('_')[2]))
        //            .Max();

        //        return $"DA_{yearMonth}_{(maxSequence + 1):D3}";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error generating project ID: {ex.Message}");
        //        return $"DA_{DateTime.Now:yyyyMM}";
        //    }
        //}

        private async Task<string> GenerateNextProjectId()
        {
            try
            {
                // Lấy toàn bộ dự án từ database
                var projects = await _duAnService.GetAllProject();

                // Nếu chưa có dự án nào
                if (!projects.Any())
                {
                    return "DA_01";
                }

                // Lấy danh sách các số đã sử dụng
                var existingNumbers = projects
                    .Select(p => int.Parse(p.MaDuAn.Split('_')[1]))
                    .OrderBy(x => x)
                    .ToList();

                // Tìm số còn thiếu đầu tiên
                for (int i = 1; i <= existingNumbers.Count + 1; i++)
                {
                    if (!existingNumbers.Contains(i))
                    {
                        return $"DA_{i:D2}";
                    }
                }

                // Trường hợp dự phòng (không khả thi nhưng vẫn an toàn)
                return $"DA_{existingNumbers.Count + 1:D2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating project ID: {ex.Message}");
                return "DA_01"; // Trường hợp có lỗi
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string nextId = await GenerateNextProjectId();
            DuAn.MaDuAn = nextId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    Console.WriteLine($"Validation errors: {errors}");
                    return Page();
                }

                // Set additional properties
                DuAn.NgayThemDuAn = DateTime.Now;
                DuAn.ThemBoi = User.Identity?.Name ?? "System";
                DuAn.NgayChinhSua = DateTime.Now;
                DuAn.ChinhSuaBoi = User.Identity?.Name ?? "System";

                var result = await _duAnService.AddProject(DuAn);
                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm dự án thành công!";
                    return RedirectToPage("/ProjectManage/Index"); // Đảm bảo path chính xác
                }

                ModelState.AddModelError("", "Không thể thêm dự án. Vui lòng kiểm tra lại thông tin.");
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception when adding project: {ex}");
                ModelState.AddModelError("", $"Lỗi khi thêm dự án: {ex.Message}");
                return Page();
            }
        }
    }
}