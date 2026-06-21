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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserRole userRole)
        {
            await _context.UserRole.AddAsync(userRole);
        }

        public async Task DeleteAsync(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserRolesByUserIdAsync(int userId)
        {
            var userRoles = _context.UserRole.Where(ur => ur.UserId == userId).ToList();
            if (userRoles.Any())
            {
                _context.UserRole.RemoveRange(userRoles);
            }
        }

        public async Task DeleteByUserIdAsync(int userId)
        {
            var userRoles =  _context.UserRole
                .Where(ur => ur.UserId == userId)
                .ToList();

            if (userRoles.Any())
            {
                _context.UserRole.RemoveRange(userRoles);
            }
        }
    }
}
