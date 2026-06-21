using apiProject.Domain.Entities;
using apiProject.Infrastructure.Data;
using apiProject.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories
{
    public class UserUnitRepository : IUserUnitRepository
    {
        private readonly AppDbContext _context;

        public UserUnitRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserUnit userUnit)
        {
            await _context.UserUnit.AddAsync(userUnit);
        }

        public Task DeleteAsync(UserUnit userUnit)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();

        }

        public Task UpdateAsync(UserUnit userUnit)
        {
            _context.UserUnit.Update(userUnit);
            return Task.CompletedTask;
        }
    }
}
