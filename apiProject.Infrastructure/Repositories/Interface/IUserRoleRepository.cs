using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
  public  interface IUserRoleRepository
    {
        Task AddAsync(UserRole userRole);
        Task UpdateAsync(UserRole userRole);
        Task DeleteAsync(UserRole userRole);
        Task<int> SaveChangesAsync();
        Task DeleteByUserIdAsync(int userId);

    }
}
