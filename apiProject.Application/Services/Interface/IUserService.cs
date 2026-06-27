using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services.Interface
{
   public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllUserAsync();
        Task<int> CreateUserAsync(CreateUserDto dto);
        Task UpdateAsync(UpdateUserDto dto);
        Task<GetUserResponseDto?> GetByIdAsync(int id);
        Task<List<UserResponseDto>> SearchAsync(SearchUserDto dto);
        Task<bool?> ChangeStatus(ChangeStatusUserDto dto);

    }
}
