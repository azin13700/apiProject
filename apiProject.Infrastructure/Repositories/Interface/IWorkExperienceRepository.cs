
using apiProject.Domain.Entities;

namespace apiProject.Infrastructure.Repositories.Interface
{
    public interface IWorkExperienceRepository
    {
        Task<List<WorkExperience>> GetByEmployeeIdAsync(int employeeId);

        Task<WorkExperience?> GetByIdAsync(int id);

        Task AddAsync(WorkExperience workExperience);

        Task UpdateAsync(WorkExperience workExperience);

        Task DeleteAsync(WorkExperience workExperience);

        Task<int> SaveChangesAsync();
    }
}