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
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachHangRepository _khachHangRepository;

        public KhachHangService(IKhachHangRepository khachHangRepository)
        {
            _khachHangRepository = khachHangRepository;
        }

        public async Task<List<KhachHang>> GetAllCustomers()
        {
            return await _khachHangRepository.GetAllCustomers();
        }

        public async Task<KhachHang> GetCustomerById(string id)
        {
            return await _khachHangRepository.GetCustomerById(id);
        }

        public async Task<bool> AddCustomer(KhachHang khachHang)
        {
            return await _khachHangRepository.AddCustomer(khachHang);
        }

        public async Task<bool> UpdateCustomer(KhachHang khachHang)
        {
            return await _khachHangRepository.UpdateCustomer(khachHang);
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            return await _khachHangRepository.DeleteCustomer(id);
        }

        public async Task<bool> CustomerExists(string id)
        {
            return await _khachHangRepository.CustomerExists(id);
        }
    }
}
