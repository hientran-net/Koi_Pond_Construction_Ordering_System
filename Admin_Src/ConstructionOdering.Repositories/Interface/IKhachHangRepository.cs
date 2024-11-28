using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOdering.Repositories.Interface
{
    public interface IKhachHangRepository
    {
        Task<List<KhachHang>> GetAllCustomers();
        Task<KhachHang> GetCustomerById(string id);
        Task<bool> AddCustomer(KhachHang khachHang);
        Task<bool> UpdateCustomer(KhachHang khachHang);
        Task<bool> DeleteCustomer(string id);
        Task<bool> CustomerExists(string id);
    }
}
