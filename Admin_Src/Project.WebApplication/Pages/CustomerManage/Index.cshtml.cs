using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.CustomerManage
{
    public class IndexModel : PageModel
    {
        private readonly IKhachHangService _khachHangService;

        public IndexModel(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        public IList<KhachHang> KhachHang { get; set; } = default!;

        public async Task OnGetAsync()
        {
            KhachHang = await _khachHangService.GetAllCustomers();
        }
    }
}
