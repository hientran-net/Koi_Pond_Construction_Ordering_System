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
        //private readonly IKhachHangService _khachHangService; // Thêm service khách hàng
        private readonly IDuAnService _duAnService; // Thêm service dự án

        public EditsModel(
            IDonDatHangService donDatHangService,
            //IKhachHangService khachHangService,
            IDuAnService duAnService)
        {
            _donDatHangService = donDatHangService;
            //_khachHangService = khachHangService;
            _duAnService = duAnService;
        }

        [BindProperty]
        public DonDatHang DonDatHang { get; set; } = default!;

        public SelectList CustomersSelectList { get; set; } // Thêm SelectList cho khách hàng
        public SelectList ProjectsSelectList { get; set; } // Thêm SelectList cho dự án

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

            //// Lấy danh sách khách hàng và tạo SelectList
            //var customers = await _khachHangService.GetAllCustomers();
            //CustomersSelectList = new SelectList(customers, "MaKhachHang", "TenKhachHang", DonDatHang.MaKhachHang);

            // Lấy danh sách dự án và tạo SelectList
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
                    //// Cần tạo lại SelectList khi validation fail
                    //var customers = await _khachHangService.GetAllCustomers();
                    //CustomersSelectList = new SelectList(customers, "MaKhachHang", "TenKhachHang", DonDatHang.MaKhachHang);

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

                // Thêm validation cho khách hàng và dự án
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