using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOrdering.Service.Interface
{
    public interface IKhachHangService
    {
        Task<List<KhachHang>> GetAllCustomers();
        Task<KhachHang> GetCustomerById(string id);
        Task<bool> AddCustomer(KhachHang khachHang);
        Task<bool> UpdateCustomer(KhachHang khachHang);
        Task<bool> DeleteCustomer(string id);
        Task<bool> CustomerExists(string id);
    }
}
