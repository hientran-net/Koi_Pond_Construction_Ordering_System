using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.ProjectManage
{
    public class DeleteModel : PageModel
    {
        private readonly IDuAnService _duAnService;

        public DeleteModel(IDuAnService duAnService)
        {
            _duAnService = duAnService;
        }

        [BindProperty]
        public DuAn DuAn { get; set; } = new DuAn();

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                var result = await _duAnService.DeleteProject(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa dự án thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa dự án. Vui lòng thử lại.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xóa dự án: {ex.Message}";
            }

            return RedirectToPage("./Index");
        }
    }
}