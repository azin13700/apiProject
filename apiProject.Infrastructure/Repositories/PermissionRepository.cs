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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Permissions permissions)
        {
            await _context.Permissions.AddAsync(permissions);
        }

   

        public async Task<List<Permissions>> GetAllAsync()
        {
            return await _context.Permissions
                 .Include(x => x.RolePermissions)
                 .ThenInclude(x=>x.Role)
                 .ToListAsync();
        }



        public async Task<Permissions?> GetByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        //public async Task<Permissions?> GetByNameAsync(string name)
        //{
        //    return await _context.Permissions.Where(x => x.Name == name).FirstOrDefaultAsync();
        //}

        public async  Task<List<Permissions>> GetPermissionsByRoleIdAsync(int roleId)
        {
            return await _context.Permissions
                   .Include(x => x.RolePermissions)
                   .ThenInclude(x => x.RoleId == roleId)
                   .ToListAsync();
        }
  
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public  Task UpdateAsync(Permissions permissions)
        {
            _context.Permissions.Update(permissions);
            return Task.CompletedTask;
        }

        public async Task<Permissions?> GetByNameAsync(string name)
        {
            return await _context.Permissions.FindAsync( name);
        }

        public  async   Task<List<Permissions>> GetAllGroupByCategoryAsync()
        {
         return await _context.Permissions
          .Include(x => x.RolePermissions)
              .ThenInclude(x => x.Role)
          .OrderBy(x => x.Category)
          .ThenBy(x => x.Name)
          .ToListAsync();

     
        }
    }
}
