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
   public class UnitRepository: IUnitRepository
    {
        private readonly AppDbContext _context;

        public UnitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Unit unut)
        {
            await _context.Unit.AddAsync(unut);
        }

        public async  Task AddUserUnitAsync(UserUnit userUnit)
        {
           await _context.UserUnit.AddAsync(userUnit); 
        }

        public async Task DeleteByUserIdAsync(int userId)
        {
            var userRoles = _context.UserUnit
               .Where(ur => ur.UserId == userId)
               .ToList();

            if (userRoles.Any())
            {
                _context.UserUnit.RemoveRange(userRoles);
            }
        }

        public async Task<List<Unit>> GetAllAsync()
        {
            return await _context.Unit
                .Include(x=>x.UserUnits)
                .ThenInclude(x=>x.User)
                .ToListAsync();
        }

        public async  Task<List<Unit>> GetAllMainUnitAsync()
        {
            return await _context.Unit
               .Where(x => x.ParentId == null)
               .ToListAsync();
        }

        public async Task<Unit?> GetByIdAsync(int id)
        {
            return await _context.Unit.FindAsync(id);
        }

        public async  Task<UserUnit?> GetByUserUnitIdAsync(int id)
        {
            return await _context.UserUnit.FindAsync(id);
        }

        public async Task<List<Unit>> GetChildrenAsync(int parentId)
        {
            return await _context.Unit
                  .Where(x => x.ParentId != null).ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public  Task UpdateAsync(Unit unit)
        {
             _context.Unit.Update(unit);
            return Task.CompletedTask;
        }


    }

}
