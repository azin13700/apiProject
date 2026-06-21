using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRoleRepository _roleRepository;
        public PermissionService( IPermissionRepository permissionRepository, IRolePermissionRepository rolePermissionRepository, IRoleRepository roleRepository    )
        {
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
        }
        public async Task<List<PermissionResponseDto>> GetAllAsync()
        {
            var permissions = await _permissionRepository.GetAllAsync();

            return permissions.Select(p => new PermissionResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                IsActive = p.IsActive,
             //   CreatedAt = p.CreatedAt,
                RoleCount = p.RolePermissions?.Count ?? 0
            }).ToList();
        }


        public async Task<PermissionResponseDto?> GetByIdAsync(int id)
        {
            var permission = await _permissionRepository.GetByIdAsync(id);
         

            return new PermissionResponseDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Category = permission.Category,
                Description = permission.Description,
                IsActive = permission.IsActive,
               // CreatedAt = permission.CreatedAt,
                RoleCount = permission.RolePermissions?.Count ?? 0
            };
        }

        public async Task<int> CreateAsync(CreatePermissionDto dto)
        {
          
            var existing = await _permissionRepository.GetByNameAsync(dto.Name);
            if (existing != null)
                throw new Exception("دسترسی با این نام قبلاً ثبت شده است");

            var permission = new Permissions
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                IsActive = dto.IsActive,
             //   CreatedAt = DateTime.UtcNow
            };

            await _permissionRepository.AddAsync(permission);
            await _permissionRepository.SaveChangesAsync();

            return permission.Id;
        }

        public async Task UpdateAsync(UpdatePermissionDto dto)
        {
            var permission = await _permissionRepository.GetByIdAsync(dto.Id);

            if (permission == null)
                throw new Exception("دسترسی یافت نشد");

            var existing = await _permissionRepository.GetByNameAsync(dto.Name);
            if (existing != null && existing.Id != dto.Id)
                throw new Exception("دسترسی با این نام قبلاً ثبت شده است");

            permission.Name = dto.Name;
            permission.Category = dto.Category;
            permission.Description = dto.Description;
            permission.IsActive = dto.IsActive;

            await _permissionRepository.UpdateAsync(permission);
            await _permissionRepository.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PermissionsGroupDto>> GetPermissionsByRole(int roleId )
        {
            var allPermissions = await _permissionRepository.GetAllGroupByCategoryAsync();
            var rolePermissions = await _rolePermissionRepository.GetByRoleIdAsync(roleId);
            var assignedIds = rolePermissions.Select(rp => rp.PermissionsId).ToHashSet();
            var grouped = allPermissions
                  .GroupBy(x => x.Category)
                  .Select(g => new PermissionsGroupDto
                  {
                      Category = g.Key,
                      Permissions = g.Select(p => new PermissionResponseDto
                      {
                          Id = p.Id,
                          Name = p.Name,
                          Category = p.Category,
                          Description = p.Description,
                          IsActive = assignedIds.Contains(p.Id),
                          RoleCount = p.RolePermissions?.Count ?? 0
                      }).ToList()
                  })
                  .ToList();

            return grouped;
        }

        public async Task AssignPermissionsToRoleAsync( int roleId, List<int> permissionIds)
        {
            var role = await _roleRepository.GetByIdRoleAsync(roleId);

            if (role == null)
                throw new Exception("نقش یافت نشد.");

            var distinctPermissionIds = permissionIds
                .Distinct()
                .ToList();

            var currentPermissions = await _rolePermissionRepository.GetByRoleIdAsync(roleId);

            await _rolePermissionRepository.DeleteAsync(currentPermissions);

            var rolePermissions = distinctPermissionIds
                .Select(permissionId => new RolePermissions
                {
                    RoleId = roleId,
                    PermissionsId = permissionId
                })
                .ToList();

            await _rolePermissionRepository.AddRangeAsync(rolePermissions);
            await _rolePermissionRepository.SaveChangesAsync();
        }

        public Task<List<PermissionResponseDto>> GetByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }
    }
}
