using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
  public  interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetWithDetailsAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<int> SaveChangesAsync();
        Task AddPhotoAsync(UserPhoto dto);
        Task AddUserRoleAsync(UserRole userRole);
        Task<bool> GetByUserNameOrEmailAsync(string userName, string email);
        Task DeleteUserPhotoAsync(int userId);

        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByLoginAsync(string username, string password);
        Task<User> GetWithRolesAndPermissionsAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}
