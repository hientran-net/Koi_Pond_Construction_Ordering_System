using MainSite.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSite.Service.Interface
{
    public interface IDuAnService
    {
        Task<bool> AddProject(DuAn duAn);
        Task<List<DuAn>> GetAllProject();
        Task<DuAn> GetProjectById(string id);
        Task<bool> UpdateProject(DuAn duAn);
        Task<bool> DeleteProject(string id);
        Task<DuAn> GetLastProject();
    }
}
