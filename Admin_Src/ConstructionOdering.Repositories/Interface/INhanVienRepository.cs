using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOdering.Repositories.Interface
{
    public interface INhanVienRepository
    {

        Task<bool> AddEmployee(NhanVien nhanVien);
        Task<List<NhanVien>> GetAllEmployee();
        Task<NhanVien> GetEmployeeById(string id);
        Task<bool> UpdateEmployee(NhanVien nhanVien);
        Task<bool> DeleteEmployee(string maNhanVien);
    }
}
