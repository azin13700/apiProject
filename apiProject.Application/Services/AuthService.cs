using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories;
using apiProject.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly IRoleRepository _roleRepository;

        public AuthService(  IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;

        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var login = await _userRepository.GetByLoginAsync(request.Username , request.Password);

            if (login == null)
            {
                throw new Exception("نام کاربری یا رمز عبور اشتباه است");
            }

            var user = await _userRepository.GetWithRolesAndPermissionsAsync(request.Username);


            var roles = user.UserRoles
                .Select(ur => ur.Role.Name)
                .ToList();

            var permissions = user.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions ?? new List<RolePermissions>())
                .Select(rp => rp.Permissions?.Name)
                .Where(p => p != null)
                .Distinct()
                .ToList();

            return new LoginResponseDto
            {
                UserId = user.Id,
                Username = user.UserName,
                FullName = $"{user.Name} {user.Family}",
                Email = user.Email,
                Roles = roles,
                Permissions = permissions
            };
        }

        public Task LogoutAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
        public async Task<SelectRoleResponseDto> SelectRoleAsync(int userId, int roleId)
        {
            // 1. بررسی وجود کاربر
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("کاربر یافت نشد");

            // 2. بررسی وجود نقش
            var role = await _roleRepository.GetByIdRoleAsync(roleId);
            if (role == null)
                throw new Exception("نقش یافت نشد");

         
            //var userRoles = await _userRepository.GetUserByIdAsync(userId);

            //    throw new Exception("شما دسترسی به این نقش را ندارید");

            // 4. دریافت دسترسی‌های نقش
            var permissions = await _roleRepository.GetPermissionsByRoleIdAsync(roleId);

            // 5. ساخت پاسخ
            return new SelectRoleResponseDto
            {
                UserId = userId,
                RoleId = roleId,
                RoleName = role.Name,
             Permissions = permissions.Select(p => p.Name).ToList(),
             //   Token = GenerateToken(userId, user.UserName, roleId) // اگر JWT دارید
            };
        }

    }
}
