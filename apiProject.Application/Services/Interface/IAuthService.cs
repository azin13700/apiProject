using apiProject.Application.Dtos.Responses;
using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apiProject.Application.Dtos.User;

namespace apiProject.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<bool> ValidateTokenAsync(string token);
        Task LogoutAsync(int userId);
        Task<SelectRoleResponseDto> SelectRoleAsync(int userId, int roleId);

    }
}
