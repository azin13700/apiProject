using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
  public  interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();
        Task<Role> GetByIdRoleAsync(int id);
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task<int> SaveChangesAsync();

        Task<Role?> GetByNameAsync(string name);
        Task<IEnumerable<Permissions>> GetPermissionsByRoleIdAsync(int roleId);
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
