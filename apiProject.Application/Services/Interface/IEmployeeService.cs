
using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services.Interface
{
   public interface IEmployeeService
    {
        Task<List<EmployeeResponseDto>> GetAllAsync();
        Task<EmployeeResponseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateEmployeeDto dto);
        Task<int> CreateDetailsAsync(CreateEmployeeDetailsDto dto);
        Task UpdateAsync(UpdateEmployeeDto dto);
        Task DeleteAsync(int id);
        Task<List<EmployeeResponseDto>> SearchAsync(SearchEmployeeDto searchDto);
        Task<int> UpdateDetailsAsync(UpdateEmployeeDetailsDto dto);
        Task<bool?> ChangeStatus(ChangeStatusDto  dto);
        Task<bool> UploadPhotoAsync(PhotoUploadDto dto);

    }
}
