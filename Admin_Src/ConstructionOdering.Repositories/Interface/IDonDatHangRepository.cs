using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOdering.Repositories.Interface
{
    public interface IDonDatHangRepository
    {
        Task<bool> AddOrder(DonDatHang donDatHang);
        Task<List<DonDatHang>> GetAllOrders();
        Task<DonDatHang> GetOrderById(string id);
        Task<bool> UpdateOderInfo(DonDatHang donDatHang);
        Task<bool> DeleteOrder(string id);
    }
}
