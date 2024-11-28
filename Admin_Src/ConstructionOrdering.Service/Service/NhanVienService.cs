using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Interface;
using ConstructionOrdering.Service.Interface;

namespace ConstructionOrdering.Service.Service
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;

        public NhanVienService(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        async Task<bool> INhanVienService.AddEmployee(NhanVien nhanVien)
        {
            return await _nhanVienRepository.AddEmployee(nhanVien);
        }

        async Task<bool> INhanVienService.DeleteEmployee(string id)
        {
            return await _nhanVienRepository.DeleteEmployee(id);
        }

        async Task<List<NhanVien>> INhanVienService.GetAllEmployees()
        {
            return await _nhanVienRepository.GetAllEmployee();
        }

        async Task<NhanVien> INhanVienService.GetEmployeeById(string id)
        {
            return await _nhanVienRepository.GetEmployeeById(id);
        }

        async Task<bool> INhanVienService.UpdateEmployee(NhanVien nhanVien)
        {
            return await _nhanVienRepository.UpdateEmployee(nhanVien);
        }
    }
}
