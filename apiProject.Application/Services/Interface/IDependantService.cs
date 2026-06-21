

using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;

namespace apiProject.Application.Services.Interface
{
    public interface IDependantService
    {

        Task<List<DependantResponseDto>> GetByEmployeeIdAsync(int employeeId);
        Task<DependantResponseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateDependantDto dto);
        Task UpdateAsync(int id, UpdateDependantDto dto);
        Task DeleteAsync(int id);
    }
}