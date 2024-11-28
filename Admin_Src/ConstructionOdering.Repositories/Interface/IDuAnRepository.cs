using ConstructionOdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOdering.Repositories.Interface
{
    public interface IDuAnRepository
    {
        Task<bool> AddProject(DuAn duAn);
        Task<List<DuAn>> GetAllProjects();
        Task<DuAn> GetProjectById(string id);
        Task<bool> UpdateProject(DuAn duAn);
        Task<bool> DeleteProject(string maDuAn);
        Task<DuAn> GetLastProject();

    }
}
