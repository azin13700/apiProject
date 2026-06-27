using apiProject.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apiProject.Application.Dtos.Permission;

namespace apiProject.Application.Services.Interface
{
   public interface IPermissionService
    {
        Task<List<PermissionResponseDto>> GetAllAsync();
        Task<PermissionResponseDto?> GetByIdAsync(int roleId);
        Task<int> CreateAsync(CreatePermissionDto dto);
        Task UpdateAsync(UpdatePermissionDto dto);
        Task DeleteAsync(int id);
        Task<List<PermissionsGroupDto>> GetPermissionsByRole(int roleId);
        //Task<List<PermissionResponse>> GetPermissionsByRoleIdAsync(int roleId);
        Task AssignPermissionsToRoleAsync(int roleId, List<int> permissionIds);
        Task<List<PermissionResponseDto>> GetByCategoryAsync(string category);
     
    }
}
