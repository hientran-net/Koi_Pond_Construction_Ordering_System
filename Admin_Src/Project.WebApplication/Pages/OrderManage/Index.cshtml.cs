using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.WebApplication.Pages.OrderManage
{
    public class IndexModel : PageModel
    {
        private readonly IDonDatHangService _donDatHangService;
        private readonly IKhachHangService _khachHangService;
        private readonly IDuAnService _duAnService;

        public IndexModel(
            IDonDatHangService donDatHangService,
            IKhachHangService khachHangService,
            IDuAnService duAnService)
        {
            _donDatHangService = donDatHangService;
            _khachHangService = khachHangService;
            _duAnService = duAnService;
        }

        public List<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
        public List<SelectListItem> KhachHangs { get; set; }
        public List<SelectListItem> DuAns { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchMaKhachHang { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchMaDuAn { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy danh sách khách hàng và dự án cho dropdown
            var khachHangs = await _khachHangService.GetAllCustomers();
            var duAns = await _duAnService.GetAllProject();

            KhachHangs = khachHangs.Select(c => new SelectListItem
            {
                Value = c.MaKhachHang,
                Text = $"{c.MaKhachHang} - {c.TenKhachHang}"
            }).ToList();

            DuAns = duAns.Select(p => new SelectListItem
            {
                Value = p.MaDuAn,
                Text = $"{p.MaDuAn} - {p.TenDuAn}"
            }).ToList();

            // Lấy toàn bộ đơn đặt hàng
            DonDatHangs = await _donDatHangService.GetAllOrders();

            // Lọc theo điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(SearchMaKhachHang))
            {
                DonDatHangs = DonDatHangs.Where(o => o.MaKhachHang == SearchMaKhachHang).ToList();
            }

            if (!string.IsNullOrEmpty(SearchMaDuAn))
            {
                DonDatHangs = DonDatHangs.Where(o => o.MaDuAn == SearchMaDuAn).ToList();
            }
        }
    }
}
