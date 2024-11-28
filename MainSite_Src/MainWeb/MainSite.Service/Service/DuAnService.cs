using MainSite.Repositories.Entities;
using MainSite.Repositories.Interface;
using MainSite.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSite.Service.Service
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

        async Task<bool> IDuAnService.DeleteProject(string id)
        {
            return await _duAnRepository.DeleteProject(id);
        }
        async Task<DuAn> IDuAnService.GetProjectById(string id)
        {
            return await _duAnRepository.GetProjectById(id);
        }

        async Task<bool> IDuAnService.UpdateProject(DuAn duAn)
        {
            return await _duAnRepository.UpdateProject(duAn);
        }

        public async Task<DuAn> GetLastProject()
        {
            return await _duAnRepository.GetLastProject();
        }

        async Task<List<DuAn>> IDuAnService.GetAllProject()
        {
            var projects = await _duAnRepository.GetAllProjects();
            Console.WriteLine($"Retrieved {projects.Count} projects in service");
            return projects;
        }
    }
}
