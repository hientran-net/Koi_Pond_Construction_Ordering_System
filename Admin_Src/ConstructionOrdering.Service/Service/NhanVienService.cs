using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Interface;
using ConstructionOrdering.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOrdering.Service.Service
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;

        public NhanVienService(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        public async Task<IEnumerable<NhanVien>> GetAllNhanVienAsync()
        {
            return await _nhanVienRepository.GetAllAsync();
        }

        public async Task<NhanVien?> GetNhanVienByIdAsync(string maNhanVien)
        {
            return await _nhanVienRepository.GetByIdAsync(maNhanVien);
        }

        public async Task<NhanVien> CreateNhanVienAsync(NhanVien nhanVien)
        {
            return await _nhanVienRepository.AddAsync(nhanVien);
        }

        public async Task UpdateNhanVienAsync(NhanVien nhanVien)
        {
            await _nhanVienRepository.UpdateAsync(nhanVien);
        }

        public async Task DeleteNhanVienAsync(string maNhanVien)
        {
            await _nhanVienRepository.DeleteAsync(maNhanVien);
        }
    }
}
