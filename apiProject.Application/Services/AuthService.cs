using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.User;
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
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("کاربر یافت نشد");

            var role = await _roleRepository.GetByIdRoleAsync(roleId);
            if (role == null)
                throw new Exception("نقش یافت نشد");

            var permissions = await _roleRepository.GetPermissionsByRoleIdAsync(roleId);

            var userUnit = user.UserUnits.FirstOrDefault();

            return new SelectRoleResponseDto
            {
                UserId = user.Id,

                RoleId = role.Id,
                RoleName = role.Name,

                UnitId = userUnit?.UnitId ?? 0,
                UnitName = userUnit?.Unit?.Name ?? string.Empty,

                Permissions = permissions
                    .Select(p => p.Name)
                    .ToList()
            };
        }

    }
}
