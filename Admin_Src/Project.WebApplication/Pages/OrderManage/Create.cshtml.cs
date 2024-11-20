using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.WebApplication.Pages.OrderManage
{
    public class CreateModel : PageModel
    {
        private readonly IDonDatHangService _donDatHangService;
        private readonly IKhachHangService _khachHangService;
        private readonly IDuAnService _duAnService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(
            IDonDatHangService donDatHangService,
            IKhachHangService khachHangService,
            IDuAnService duAnService,
            ILogger<CreateModel> logger)
        {
            _donDatHangService = donDatHangService;
            _khachHangService = khachHangService;
            _duAnService = duAnService;
            _logger = logger;
            DonDatHang = new DonDatHang();
        }

        [BindProperty]
        public DonDatHang DonDatHang { get; set; }

        public List<SelectListItem> KhachHangs { get; set; }
        public List<SelectListItem> DuAns { get; set; }

        private async Task LoadDropDownData()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading dropdown data: {ex.Message}");
                throw;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await LoadDropDownData();
                DonDatHang.MaDonDatHang = await GenerateNextOrderId();
                return Page();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in OnGetAsync: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi tải trang. Vui lòng thử lại.");
                return Page();
            }
        }

        private async Task<string> GenerateNextOrderId()
        {
            try
            {
                var projects = await _donDatHangService.GetAllOrders();

                if (!projects.Any())
                {
                    return "DDA_01";
                }

                var existingNumbers = projects
                    .Select(p => int.Parse(p.MaDonDatHang.Split('_')[1]))
                    .OrderBy(x => x)
                    .ToList();

                for (int i = 1; i <= existingNumbers.Count + 1; i++)
                {
                    if (!existingNumbers.Contains(i))
                    {
                        return $"DDA_{i:D2}";
                    }
                }

                return $"DDA_{existingNumbers.Count + 1:D2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating project ID: {ex.Message}");
                return "DDA_01";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _logger.LogInformation("Starting OnPostAsync");
                _logger.LogInformation($"Form data: {System.Text.Json.JsonSerializer.Serialize(DonDatHang)}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState is invalid");
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            _logger.LogWarning($"Validation error: {error.ErrorMessage}");
                        }
                    }
                    await LoadDropDownData();
                    return Page();
                }

                if (DonDatHang.NgayDatHang == default)
                {
                    DonDatHang.NgayDatHang = DateTime.Now;
                }

                if (string.IsNullOrEmpty(DonDatHang.MaKhachHang) || string.IsNullOrEmpty(DonDatHang.MaDuAn))
                {
                    ModelState.AddModelError(string.Empty, "Vui lòng chọn khách hàng và dự án");
                    await LoadDropDownData();
                    return Page();
                }

                if (DonDatHang.NgayBatDauThiCong == default || DonDatHang.NgayKetThucThiCong == default)
                {
                    ModelState.AddModelError(string.Empty, "Vui lòng chọn ngày bắt đầu và kết thúc");
                    await LoadDropDownData();
                    return Page();
                }

                if (DonDatHang.NgayKetThucThiCong <= DonDatHang.NgayBatDauThiCong)
                {
                    ModelState.AddModelError(string.Empty, "Ngày kết thúc phải sau ngày bắt đầu");
                    await LoadDropDownData();
                    return Page();
                }

                var khachHang = await _khachHangService.GetCustomerById(DonDatHang.MaKhachHang);
                if (khachHang == null)
                {
                    ModelState.AddModelError(string.Empty, "Khách hàng không tồn tại");
                    await LoadDropDownData();
                    return Page();
                }

                var duAn = await _duAnService.GetProjectById(DonDatHang.MaDuAn);
                if (duAn == null)
                {
                    ModelState.AddModelError(string.Empty, "Dự án không tồn tại");
                    await LoadDropDownData();
                    return Page();
                }

                
                if (string.IsNullOrEmpty(DonDatHang.MaDonDatHang))
                {
                    DonDatHang.MaDonDatHang = await GenerateNextOrderId();
                }

                _logger.LogInformation($"Attempting to add order: {DonDatHang.MaDonDatHang}");
                var result = await _donDatHangService.AddOrder(DonDatHang);

                if (result)
                {
                    _logger.LogInformation("Order added successfully");
                    TempData["SuccessMessage"] = "Đơn đặt hàng đã được tạo thành công!";
                    return RedirectToPage("./Index");
                }

                ModelState.AddModelError(string.Empty, "Không thể tạo đơn đặt hàng");
                await LoadDropDownData();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in OnPostAsync: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
                await LoadDropDownData();
                return Page();
            }
        }
    }
}