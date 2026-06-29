using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
  public  interface IUnitRepository
    {
        Task<List<Unit>> GetAllAsync();
        Task<Unit?> GetByIdAsync(int id);
        Task<UserUnit?> GetByUserUnitIdAsync(int id);
        Task AddUserUnitAsync(UserUnit userUnit);
        Task AddAsync(Unit unut);
        Task UpdateAsync(Unit unit);
        Task<int> SaveChangesAsync();

        Task DeleteByUserIdAsync(int userId);
        Task<List<Unit>> GetChildrenAsync(int parentId);
        Task<List<Unit>> GetAllMainUnitAsync();
    }

}
