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
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Role
                .Include(x=>x.RolePermissions)
                .ThenInclude(x=>x.Permissions)      
             .ToListAsync();
        }

   

      public async  Task AddAsync(Role role)
        {
            await _context.Role.AddAsync(role);

        }



        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

       public  Task UpdateAsync(Role role)
        {
            _context.Role.Update(role);
            return Task.CompletedTask;
        }

        public async Task<Role?> GetByIdRoleAsync(int id)
        {
            return await _context.Role
                .Include(r => r.UserRoles)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // ====== دریافت نقش با نام ======
        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Role
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        // ====== دریافت دسترسی‌های یک نقش ======
        public async Task<IEnumerable<Permissions>> GetPermissionsByRoleIdAsync(int roleId)
        {
            var permissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permissions)
                .Select(rp => rp.Permissions)
                .ToListAsync();

            return permissions ?? new List<Permissions>();
        }

        // ====== دریافت همه نقش‌ها ======
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permissions)
                .ToListAsync();
        }
    }
    }

