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
        Task<int> CreateAsync(CreateUnitDto dto);
        Task UpdateAsync(UpdateUnitDto dto);
        Task DeleteAsync(int id);
      Task<bool?> ChangeStatus(ChangeStatusUnitDto dto);

    }
}
