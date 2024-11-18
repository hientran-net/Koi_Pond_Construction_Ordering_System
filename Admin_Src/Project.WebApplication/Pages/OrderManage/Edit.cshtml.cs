using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.WebApplication.Pages.OrderManage
{
    public class EditModel : PageModel
    {
        private readonly IDonDatHangService _orderService;
        private readonly IKhachHangService _customerService;
        private readonly IDuAnService _projectService;

        public EditModel(
            IDonDatHangService orderService,
            IKhachHangService customerService,
            IDuAnService projectService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _projectService = projectService;
        }

        [BindProperty]
        public DonDatHang Order { get; set; }
        public SelectList CustomerList { get; set; }
        public SelectList ProjectList { get; set; }

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

            var customers = await _customerService.GetAllCustomers();
            var projects = await _projectService.GetAllProject();

            CustomerList = new SelectList(customers, "MaKhachHang", "TenKhachHang");
            ProjectList = new SelectList(projects, "MaDuAn", "TenDuAn");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(Order.MaDonDatHang);
                return Page();
            }

            try
            {
                await _orderService.UpdateOrderInfo(Order);
                TempData["SuccessMessage"] = "Cập nhật đơn đặt hàng thành công!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật đơn đặt hàng.");
                await OnGetAsync(Order.MaDonDatHang);
                return Page();
            }
        }
    }
}
