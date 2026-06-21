using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
    public interface IPermissionRepository
    {
        
        Task<List<Permissions>> GetAllAsync();
        Task<List<Permissions>> GetAllGroupByCategoryAsync();


        Task<Permissions?> GetByIdAsync(int id);
        Task AddAsync(Permissions  permissions);
        Task UpdateAsync(Permissions permissions);
        Task<int> SaveChangesAsync();
        Task<List<Permissions>> GetPermissionsByRoleIdAsync(int roleId);
        Task<Permissions?> GetByNameAsync(string name);
    }
}
