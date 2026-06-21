
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Data;
using apiProject.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories
{
    public class DependantRepository : IDependantRepository
    {
        private readonly AppDbContext _context;

        public DependantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dependant>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Dependants
                .Where(x => x.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<Dependant?> GetByIdAsync(int id)
        {
            return await _context.Dependants
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Dependant dependant)
        {
            await _context.Dependants.AddAsync(dependant);
        }

        public Task UpdateAsync(Dependant dependant)
        {
            _context.Dependants.Update(dependant);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Dependant dependant)
        {
            _context.Dependants.Remove(dependant);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
