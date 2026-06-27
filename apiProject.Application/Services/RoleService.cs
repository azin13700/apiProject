using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.Role;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
   

        public RoleService(IRoleRepository repository )
        {
            _repository = repository;
       
        }

        public async Task<int> CreateAsync(CreateRoleDto dto)
        {

            var role = new Role
            {
                Name = dto.RoleName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
            };

            await _repository.AddAsync(role);
            await _repository.SaveChangesAsync();
            return role.Id;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleResponseDto>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();
            return roles.Select(roles => new RoleResponseDto
            {
                RoleId = roles.Id,
                RoleName = roles.Name,   
                Description = roles.Description ,
                IsActive = (bool)roles.IsActive,
                Permissions = roles.RolePermissions?
            .Where(rp => rp.Permissions != null)
            .Select(rp => rp.Permissions.Name)
            .ToList() ?? new List<string>()

            }).ToList();
        }

        public async Task<RoleResponseDto> GetByIdAsync(int id)
        {
            var unit = await _repository.GetByIdRoleAsync(id);

            if (unit == null)
                return null;

            return new RoleResponseDto
            {
                RoleId = unit.Id,
                RoleName = unit.Name,
                Description = unit.Description,
                IsActive = (bool)unit.IsActive
            };
        }

        public async  Task UpdateAsync(UpdateRoleDto dto)
        {
            var entity = await _repository.GetByIdRoleAsync(dto.RoleId);

            if (entity == null)
                return;

            entity.Name = dto.RoleName;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }


        public async Task<bool?> ChangeStatus(ChangeStatusRoleDto dto)
        {
            var employee = await _repository.GetByIdRoleAsync(dto.RoleId);
            if (employee == null)
            {
                return null;
            }
            employee.IsActive = !employee.IsActive;
            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
        }

    }
}
