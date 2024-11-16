using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOrdering.Service.Interface
{
    public interface INhanVienService
    {
        Task<IEnumerable<NhanVien>> GetAllNhanVienAsync();
        Task<NhanVien?> GetNhanVienByIdAsync(string maNhanVien);
        Task<NhanVien> CreateNhanVienAsync(NhanVien nhanVien);
        Task UpdateNhanVienAsync(NhanVien nhanVien);
        Task DeleteNhanVienAsync(string maNhanVien);
    }
}
