using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOdering.Repositories.Repository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly AdminDbConsturctionOderingSystemContext _context;

        public NhanVienRepository(AdminDbConsturctionOderingSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            return await _context.Set<NhanVien>().ToListAsync();
        }

        public async Task<NhanVien?> GetByIdAsync(string maNhanVien)
        {
            return await _context.Set<NhanVien>().FindAsync(maNhanVien);
        }

        public async Task<NhanVien> AddAsync(NhanVien nhanVien)
        {
            await _context.Set<NhanVien>().AddAsync(nhanVien);
            await _context.SaveChangesAsync();
            return nhanVien;
        }

        public async Task UpdateAsync(NhanVien nhanVien)
        {
            _context.Set<NhanVien>().Update(nhanVien);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maNhanVien)
        {
            var nhanVien = await GetByIdAsync(maNhanVien);
            if (nhanVien != null)
            {
                _context.Set<NhanVien>().Remove(nhanVien);
                await _context.SaveChangesAsync();
            }
        }
    }
}
