using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Interface;
using ConstructionOrdering.Service.Interface;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOrdering.Service.Service
{
    public class DonDatHangService : IDonDatHangService
    {
        private readonly IDonDatHangRepository _donDatHangRepository;

        public DonDatHangService(IDonDatHangRepository donDatHangRepository)
        {
            _donDatHangRepository = donDatHangRepository;
        }

        async Task<bool> IDonDatHangService.AddOrder(DonDatHang donDatHang)
        {
            return await _donDatHangRepository.AddOrder(donDatHang);
        }

        async Task<bool> IDonDatHangService.DeleteOrder(string id)
        {
            return await _donDatHangRepository.DeleteOrder(id);
        }

        async Task<List<DonDatHang>> IDonDatHangService.GetAllOrders()
        {
            return await _donDatHangRepository.GetAllOrders();
        }

        async Task<DonDatHang> IDonDatHangService.GetOrderById(string id)
        {
            return await _donDatHangRepository.GetOrderById(id);
        }

        async Task<bool> IDonDatHangService.UpdateOrderInfo(DonDatHang donDatHang)
        {
            return await _donDatHangRepository.UpdateOderInfo(donDatHang);
        }
    }
}
