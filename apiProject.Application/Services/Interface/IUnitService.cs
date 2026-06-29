using apiProject.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apiProject.Application.Dtos.Unit;

namespace apiProject.Application.Services.Interface
{
   public interface IUnitService
    {
        Task<List<UnitResponseDto>> GetAllAsync();
        Task<UnitResponseDto?> GetByIdAsync(int id);
        Task<List<UnitResponseDto>> GetChildrenAsync(int parentId);
        Task<List<UnitResponseDto>> GetRootUnitsAsync();
        Task<int> CreateAsync(CreateUnitDto dto);
        Task UpdateAsync(int id, UpdateUnitDto dto);
        Task DeleteAsync(int id);
        Task<bool> ToggleStatusAsync(int id);
        Task UpdateAsync(UpdateUnitDto dto);
    }
}
