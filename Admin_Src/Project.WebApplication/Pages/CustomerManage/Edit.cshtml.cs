using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.CustomerManage
{
    public class EditModel : PageModel
    {
        private readonly IKhachHangService _khachHangService;

        public EditModel(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        [BindProperty]
        public KhachHang KhachHang { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _khachHangService.GetCustomerById(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            KhachHang = khachhang;
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
                var result = await _khachHangService.UpdateCustomer(KhachHang);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin khách hàng thành công!";
                    return RedirectToPage("./Index");
                }

                TempData["ErrorMessage"] = "Cập nhật thông tin khách hàng thất bại!";
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
