
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Data;
using apiProject.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace apiProject.Infrastructure.Repositories
{
    public class WorkExperienceRepository : IWorkExperienceRepository
    {
        private readonly AppDbContext _context;

        public WorkExperienceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkExperience>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.WorkExperiences
                .Where(x => x.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<WorkExperience?> GetByIdAsync(int id)
        {
            return await _context.WorkExperiences
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(WorkExperience workExperience)
        {
            await _context.WorkExperiences.AddAsync(workExperience);
        }

        public Task UpdateAsync(WorkExperience workExperience)
        {
            _context.WorkExperiences.Update(workExperience);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(WorkExperience workExperience)
        {
            _context.WorkExperiences.Remove(workExperience);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}