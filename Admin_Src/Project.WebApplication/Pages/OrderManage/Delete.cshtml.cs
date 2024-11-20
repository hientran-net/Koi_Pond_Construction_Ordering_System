using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.OrderManage
{
    public class DeleteModel : PageModel
    {
        private readonly IDonDatHangService _orderService;

        public DeleteModel(IDonDatHangService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public DonDatHang Order { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _orderService.GetOrderById(id);
            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (Order.MaDonDatHang == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        await _orderService.DeleteOrder(Order.MaDonDatHang);
        //        TempData["SuccessMessage"] = "Xóa đơn đặt hàng thành công!";
        //        return RedirectToPage("./Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa đơn đặt hàng.";
        //        return RedirectToPage("./Index");
        //    }
        //}


        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                var result = await _orderService.DeleteOrder(id);
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
