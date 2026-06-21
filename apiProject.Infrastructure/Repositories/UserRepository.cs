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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.User
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.User
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetWithRolesAndPermissionsAsync(string username)
        {
            return await _context.User
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.User.AnyAsync(x => x.UserName == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.User.AnyAsync(x => x.Email == email);
        }
        public async  Task AddUserAsync(User user)
        {
            await _context.User.AddAsync(user);
        }

    
        public async Task AddPhotoAsync(UserPhoto photo)
        {
            await _context.UserPhoto.AddAsync(photo);
        }

        public async Task DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User
                .Include(x => x.UserPhoto)
                 .Include(x => x.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Include(x=>x.UserUnits)
                .ThenInclude(x=>x.Unit)
                 .AsNoTracking()
                 .ToListAsync();



        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.User
               .Include(x => x.UserPhoto)
                .Include(x => x.UserRoles)
               .ThenInclude(ur => ur.Role)
                    .Include(x => x.UserUnits)
                .ThenInclude(x => x.Unit)
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();
              
               
        }

        public async Task<User?> GetWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public  Task UpdateUserAsync(User user)
        {
             _context.User.Update(user);
            return Task.CompletedTask;
        }

        public  async Task<bool> GetByUserNameOrEmailAsync(string userName, string email)
        {
            return await _context.User.AnyAsync(x => x.UserName == userName && x.Email == email);
           
        }
        public async Task DeleteUserPhotoAsync(int userId)
        {
            var photo = await _context.UserPhoto.FirstOrDefaultAsync(p => p.UserId == userId);
            if (photo != null)
            {
                _context.UserPhoto.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddUserRoleAsync(UserRole userRole) => await _context.UserRole.AddAsync(userRole);

    }
}
