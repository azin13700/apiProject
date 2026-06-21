using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
   public interface IRolePermissionRepository
    {
        Task<List<RolePermissions>> GetAllAsync();
        Task<RolePermissions> GetByIdAsync(int id);
        Task<List<RolePermissions>> GetByRoleIdAsync(int roleId);
        Task AddAsync(RolePermissions role);
        Task UpdateAsync(RolePermissions role);
        Task<int> SaveChangesAsync();
        Task DeleteAsync(List<RolePermissions> currentPermissions);
        Task AddRangeAsync(List<RolePermissions> rolePermissions);
    }
}
