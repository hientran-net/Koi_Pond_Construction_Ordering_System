using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.WebApplication.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly INhanVienService _nhanVienService;

        public IndexModel(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        public IList<NhanVien> NhanViens { get; set; } = default!;

        public async Task OnGetAsync()
        {
            NhanViens = (List<NhanVien>)await _nhanVienService.GetAllNhanVienAsync();
        }
    }
}
