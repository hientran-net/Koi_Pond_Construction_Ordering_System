using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Project.WebApplication.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly INhanVienService _nhanVienService;

        public IndexModel(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        public List<NhanVien> NhanViens { get; set; } = new List<NhanVien>();

        public async Task OnGetAsync()
        {
            NhanViens = await _nhanVienService.GetAllEmployees();
        }
    }
}
