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
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AdminDbConsturctionOderingSystemContext _context;

        public KhachHangRepository(AdminDbConsturctionOderingSystemContext context)
        {
            _context = context;
        }

        public async Task<List<KhachHang>> GetAllCustomers()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public async Task<KhachHang> GetCustomerById(string id)
        {
            return await _context.KhachHangs.FindAsync(id);
        }

        public async Task<bool> AddCustomer(KhachHang khachHang)
        {
            try
            {
                await _context.KhachHangs.AddAsync(khachHang);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomer(KhachHang khachHang)
        {
            try
            {
                _context.KhachHangs.Update(khachHang);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            var customer = await _context.KhachHangs.FindAsync(id);
            if (customer != null)
            {
                _context.KhachHangs.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CustomerExists(string id)
        {
            return await _context.KhachHangs.AnyAsync(e => e.MaKhachHang == id);
        }
    }

}
