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
        Task<bool> AddEmployee(NhanVien nhanVien);
        Task<List<NhanVien>> GetAllEmployees();
        Task<NhanVien> GetEmployeeById(string id);
        Task<bool> UpdateEmployee(NhanVien nhanVien);
        Task<bool> DeleteEmployee(string id);
    }
}
