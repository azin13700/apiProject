using apiProject.Application.Dtos.Dependancy;
using apiProject.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services.Interface
{
    public interface IWorkExperienceService
    {
        Task<List<WorkExperienceResponseDto>> GetByEmployeeIdAsync(int employeeId);
        Task<WorkExperienceResponseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateWorkExperienceDto dto);
        Task UpdateAsync(int id, UpdateWorkExperienceDto dto);
        Task DeleteAsync(int id);
    }
}
