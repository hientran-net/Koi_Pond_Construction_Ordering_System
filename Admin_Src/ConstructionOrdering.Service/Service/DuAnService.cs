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
    public class DuAnService : IDuAnService
    {
        private readonly IDuAnRepository _duAnRepository;

        public DuAnService(IDuAnRepository duAnRepository)
        {
            _duAnRepository = duAnRepository;
        }

        async Task<bool> IDuAnService.AddProject(DuAn duAn)
        {
            return await _duAnRepository.AddProject(duAn);
        }

        async Task<bool> IDuAnService.DeleteProject(int id)
        {
            return await _duAnRepository.DeleteProject(id);
        }

        async Task<List<DuAn>> IDuAnService.GetAllProject()
        {
            return await _duAnRepository.GetAllProjects();
        }

        async Task<DuAn> IDuAnService.GetProjectById(int id)
        {
            return await _duAnRepository.GetProjectById(id);
        }

        async Task<bool> IDuAnService.UpdateProject(DuAn duAn)
        {
            return await _duAnRepository.UpdateProject(duAn);
        }
    }
}
