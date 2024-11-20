using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.ProjectManage
{
    public class EditModel : PageModel
    {
        private readonly IDuAnService _duAnService;

        public EditModel(IDuAnService duAnService)
        {
            _duAnService = duAnService;
        }

        [BindProperty]
        public DuAn DuAn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var duAn = await _duAnService.GetProjectById(id);
            if (duAn == null)
            {
                return NotFound();
            }
            DuAn = duAn;
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
                DuAn.NgayChinhSua = DateTime.Now;
                DuAn.ChinhSuaBoi = User.Identity?.Name ?? "System";

                var result = await _duAnService.UpdateProject(DuAn);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật dự án thành công!";
                    return RedirectToPage("./Index");
                }

                ModelState.AddModelError("", "Không thể cập nhật dự án. Vui lòng thử lại.");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi cập nhật dự án: {ex.Message}");
                return Page();
            }
        }
    }
}