using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
    public interface IDependantRepository
    {
        Task<List<Dependant>> GetByEmployeeIdAsync(int employeeId);

        Task<Dependant?> GetByIdAsync(int id);

        Task AddAsync(Dependant dependant);

        Task UpdateAsync(Dependant dependant);

        Task DeleteAsync(Dependant dependant);

        Task<int> SaveChangesAsync();
    }
}
