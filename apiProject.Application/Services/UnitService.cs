using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.Unit;
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
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _repository;

        public UnitService(IUnitRepository repository)
        {
            _repository = repository;
        ;
        }

        public async Task<bool?> ChangeStatus(ChangeStatusUnitDto dto)
        {
            var employee = await _repository.GetByIdAsync(dto.UnitId);
            if (employee == null)
            {
                return null;
            }
            employee.IsActive = !employee.IsActive;
            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<int> CreateAsync(CreateUnitDto dto)
        {
            
            if (dto.ParentId.HasValue)
            {
                var parent = await _repository.GetByIdAsync(dto.ParentId.Value);
                if (parent == null)
                    throw new Exception("واحد والد یافت نشد");
            }

            var unit = new Unit
            {
                Name = dto.Name,
                Description = dto.Description,
                ParentId = dto.ParentId,
                IsActive = dto.IsActive ?? true,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await _repository.AddAsync(unit);
            await _repository.SaveChangesAsync();

            return unit.Id;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<List<UnitResponseDto>> GetAllAsync()
        {
            var units = await _repository.GetAllAsync();

            return units.Select(unit => new UnitResponseDto
            {
                UnitId = unit.Id,
                Name = unit.Name,
                Description = unit.Description,
                ParentId = unit.ParentId,
             
                IsActive = (bool)unit.IsActive,
                UserCount = unit.UserUnits?.Count ?? 0,


            }).ToList();
        }

        public async Task<UnitResponseDto?> GetByIdAsync(int id)
        {
            var unit = await _repository.GetByIdAsync(id);

            if (unit == null)
                return null;

            return new UnitResponseDto
            {
                UnitId = unit.Id,
                Name = unit.Name,
                Description = unit.Description,
                  IsActive = (bool)unit.IsActive,
                  ParentId = unit.ParentId
            };
        }

        public Task<List<UnitResponseDto>> GetChildrenAsync(int parentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UnitResponseDto>> GetRootUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UnitResponseDto?>> GetSubSubjectByIdAsync(int parentId)
        {
            var units = await _repository.GetChildrenAsync(parentId);

            if (units == null)
                return null;
            return units.Select(unit => new UnitResponseDto
            {
                UnitId = unit.Id,
                Name = unit.Name,
                Description = unit.Description,
                IsActive = (bool)unit.IsActive,
                ParentId = unit.ParentId

            }).ToList();
        }

        public async Task<List<SelectedSubUnitDto?>> GetSubUnit(int unitId)
        {
            var subjects = await _repository.GetChildrenAsync(unitId);
            if (subjects == null)
                return null;
            return subjects.Select(sub => new SelectedSubUnitDto
            {
                UnitId = sub.Id,
                ParentId = sub.ParentId,
                Name = sub.Name

            }).ToList();
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
            {
                return false;
            }
            employee.IsActive = !employee.IsActive;
            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async  Task UpdateAsync(UpdateUnitDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.UnitId);

            if (entity == null)
                return;

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public Task UpdateAsync(int id, UpdateUnitDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
