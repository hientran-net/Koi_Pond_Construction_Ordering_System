using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOrdering.Service.Interface
{
    public interface IDonDatHangService
    {
        Task<bool> AddOrder(DonDatHang donDatHang);
        Task<List<DonDatHang>> GetAllOrders();
        Task<DonDatHang> GetOrderById(string id);
        Task<bool> UpdateOrderInfo(DonDatHang donDatHang);
        Task<bool> DeleteOrder(string id);
    }
}
