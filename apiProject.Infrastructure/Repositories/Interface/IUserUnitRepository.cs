using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
 public   interface IUserUnitRepository
    {
        Task AddAsync(UserUnit userUnit);
        Task UpdateAsync(UserUnit userUnit);
        Task DeleteAsync(UserUnit userUnit);
        Task<int> SaveChangesAsync();
    }
}
