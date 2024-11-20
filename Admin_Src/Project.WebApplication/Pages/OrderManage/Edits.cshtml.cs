using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.WebApplication.Pages.OrderManage
{
    public class EditsModel : PageModel
    {
        private readonly IDonDatHangService _donDatHangService;
        private readonly IDuAnService _duAnService;

        public EditsModel(
            IDonDatHangService donDatHangService,
            IDuAnService duAnService)
            {
                _donDatHangService = donDatHangService;
                _duAnService = duAnService;
            }

        [BindProperty]
        public DonDatHang DonDatHang { get; set; } = default!;

        public SelectList CustomersSelectList { get; set; }
        public SelectList ProjectsSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Mã đơn hàng không được để trống!";
                return RedirectToPage("./Index");
            }

            var dondathang = await _donDatHangService.GetOrderById(id);
            if (dondathang == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy đơn hàng!";
                return RedirectToPage("./Index");
            }

            DonDatHang = dondathang;

            var projects = await _duAnService.GetAllProject();
            ProjectsSelectList = new SelectList(projects, "MaDuAn", "TenDuAn", DonDatHang.MaDuAn);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var projects = await _duAnService.GetAllProject();
                    ProjectsSelectList = new SelectList(projects, "MaDuAn", "TenDuAn", DonDatHang.MaDuAn);

                    return Page();
                }

                var existingOrder = await _donDatHangService.GetOrderById(DonDatHang.MaDonDatHang);
                if (existingOrder == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đơn hàng cần cập nhật!";
                    return RedirectToPage("./Index");
                }

                if (existingOrder.MaDonDatHang != DonDatHang.MaDonDatHang)
                {
                    ModelState.AddModelError("", "Không được phép thay đổi mã đơn hàng!");
                    return Page();
                }

                if (string.IsNullOrEmpty(DonDatHang.MaKhachHang))
                {
                    ModelState.AddModelError("DonDatHang.MaKhachHang", "Vui lòng chọn khách hàng!");
                    return Page();
                }

                if (string.IsNullOrEmpty(DonDatHang.MaDuAn))
                {
                    ModelState.AddModelError("DonDatHang.MaDuAn", "Vui lòng chọn dự án!");
                    return Page();
                }

                var result = await _donDatHangService.UpdateOrderInfo(DonDatHang);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin đơn hàng thành công!";
                    return RedirectToPage("./Index");
                }

                TempData["ErrorMessage"] = "Cập nhật đơn hàng thất bại!";
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi cập nhật đơn hàng: {ex.Message}");
                return Page();
            }
        }
    }
}