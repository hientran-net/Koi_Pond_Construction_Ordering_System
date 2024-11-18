using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.CustomerManage
{
    public class DeleteModel : PageModel
    {
        private readonly IKhachHangService _khachHangService;

        public DeleteModel(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        [BindProperty]
        public KhachHang KhachHang { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _khachHangService.GetCustomerById(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            KhachHang = khachHang;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _khachHangService.DeleteCustomer(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa thông tin khách hàng thành công!";
                return RedirectToPage("./Index");
            }

            TempData["ErrorMessage"] = "Xóa thông tin khách hàng thất bại!";
            return Page();
        }
    }
}