using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Interface;
using DevExpress.Data.Browsing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionOdering.Repositories.Repository
{
    public class DuAnRepository : IDuAnRepository
    {
        private readonly AdminDbConsturctionOderingSystemContext _dbContext;
        public DuAnRepository(AdminDbConsturctionOderingSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<bool> IDuAnRepository.AddProject(DuAn duAn)
        {
            try
            {
                await _dbContext.DuAns.AddAsync(duAn);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        async Task<List<DuAn>> GetAllProject()
        {
            // Thêm log để kiểm tra
            var projects = await _dbContext.DuAns
                .AsNoTracking()
                .OrderByDescending(x => x.MaDuAn)
                .ToListAsync();

            Console.WriteLine($"Found {projects.Count} projects in repository");
            return projects;
        }

        async Task<List<DuAn>> IDuAnRepository.GetAllProjects()
        {
            return await _dbContext.DuAns.ToListAsync();
        }

        async Task<DuAn> IDuAnRepository.GetProjectById(string id)
        {
            return await _dbContext.DuAns.FindAsync(id);
        }

        async Task<bool> IDuAnRepository.UpdateProject(DuAn duAn)
        {
            try
            {
                _dbContext.DuAns.Update(duAn);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<DuAn> GetLastProject()
        {
            using (var context = new AdminDbConsturctionOderingSystemContext())
            {
                return await context.DuAns 
                    .OrderByDescending(x => x.MaDuAn)
                    .FirstOrDefaultAsync();
            }
        }

        async Task<bool> IDuAnRepository.DeleteProject(string maDuAn)
        {

            var project = await _dbContext.DuAns.FindAsync(maDuAn);
            if (project != null)
            {
                _dbContext.DuAns.Remove(project);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
