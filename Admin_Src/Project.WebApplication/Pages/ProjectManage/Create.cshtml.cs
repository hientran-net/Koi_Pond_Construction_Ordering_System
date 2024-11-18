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


        private async Task<string> GenerateNextProjectId()
        {
            try
            {
                var projectid = await _duAnService.GetAllProject();
                int count = projectid.Count + 1;
                return $"DA_{count:D2}"; // D2 sẽ format số thành 2 chữ số, thêm 0 vào trước nếu cần
            }
            catch
            {
                return "DA_01"; // Trường hợp chưa có nhân viên hoặc có lỗi
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