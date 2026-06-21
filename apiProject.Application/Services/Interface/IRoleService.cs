using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services.Interface
{
   public interface IRoleService
    {
        Task<RoleResponseDto> GetByIdAsync(int id);
        Task<List<RoleResponseDto>> GetAllAsync();
        Task<int> CreateAsync(CreateRoleDto dto);
        Task UpdateAsync(UpdateRoleDto dto);
        Task DeleteAsync(int id);
        Task<bool?> ChangeStatus(ChangeStatusRoleDto dto);


    }
}
