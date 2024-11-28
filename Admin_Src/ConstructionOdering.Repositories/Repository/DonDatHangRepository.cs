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
    public class DonDatHangRepository : IDonDatHangRepository
    {
        private readonly AdminDbConsturctionOderingSystemContext _dbContext;
        public DonDatHangRepository(AdminDbConsturctionOderingSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<bool> IDonDatHangRepository.AddOrder(DonDatHang donDatHang)
        {
            try
            {
                await _dbContext.DonDatHangs.AddAsync(donDatHang);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        async Task<bool> IDonDatHangRepository.DeleteOrder(string id)
        {
            var order = await _dbContext.DonDatHangs.FindAsync(id);
            if (order != null)
            {
                _dbContext.DonDatHangs.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<List<DonDatHang>> IDonDatHangRepository.GetAllOrders()
        {
            return await _dbContext.DonDatHangs.ToListAsync();
        }

        async Task<DonDatHang> IDonDatHangRepository.GetOrderById(string id)
        {
            return await _dbContext.DonDatHangs.FindAsync(id);
        }

        async Task<bool> IDonDatHangRepository.UpdateOderInfo(DonDatHang donDatHang)
        {
            try
            {
                _dbContext.DonDatHangs.Update(donDatHang);
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
