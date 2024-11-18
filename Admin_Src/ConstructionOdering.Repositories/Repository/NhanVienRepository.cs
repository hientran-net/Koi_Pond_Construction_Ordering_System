using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOdering.Repositories.Repository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly AdminDbConsturctionOderingSystemContext _dbContext;

        public NhanVienRepository(AdminDbConsturctionOderingSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<bool> INhanVienRepository.AddEmployee(NhanVien nhanVien)
        {
            try
            {
                await _dbContext.NhanViens.AddAsync(nhanVien);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        async Task<bool> INhanVienRepository.DeleteEmployee(string maNhanVien)
        {
            var employee = await _dbContext.NhanViens.FindAsync(maNhanVien);
            if (employee != null)
            {
                _dbContext.NhanViens.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<List<NhanVien>> INhanVienRepository.GetAllEmployee()
        {
            return await _dbContext.NhanViens.ToListAsync();
        }

        async Task<NhanVien> INhanVienRepository.GetEmployeeById(string id)
        {
            return await _dbContext.NhanViens.FindAsync(id);
        }

        async Task<bool> INhanVienRepository.UpdateEmployee(NhanVien nhanVien)
        {
            try
            {
                _dbContext.NhanViens.Update(nhanVien);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
