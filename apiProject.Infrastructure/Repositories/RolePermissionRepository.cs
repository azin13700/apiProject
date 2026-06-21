using apiProject.Domain.Entities;
using apiProject.Infrastructure.Data;
using apiProject.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly AppDbContext _context;

        public RolePermissionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RolePermissions role)
        {
            await _context.RolePermissions.AddAsync(role);
        }

        public Task AddRangeAsync(List<RolePermissions> rolePermissions)
        {
            _context.RolePermissions.AddRange(rolePermissions);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(RolePermissions role)
        {
            _context.RolePermissions.Remove(role);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(List<RolePermissions> currentPermissions)
        {
            _context.RolePermissions.RemoveRange(currentPermissions);
            return Task.CompletedTask;

        }

        public async Task<List<RolePermissions>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RolePermissions> GetByIdAsync(int id)
        {
            return  _context.RolePermissions.Where(x => x.Id == id).FirstOrDefault();
        }

        public  async Task<List<RolePermissions>> GetByRoleIdAsync(int roleId)
        {
            return await _context.RolePermissions
                 .Where(rp => rp.RoleId == roleId)
                 .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public  Task UpdateAsync(RolePermissions role)
        {
            _context.RolePermissions.Update(role);
            return Task.CompletedTask;
        }
    }
}
