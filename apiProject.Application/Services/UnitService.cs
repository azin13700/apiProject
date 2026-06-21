using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
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
            var unit = new Unit
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive= dto.IsActive,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
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
                UserCount = unit.UserUnits?.Count ?? 0,
                IsActive = unit.IsActive,
               

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
                  IsActive = unit.IsActive    
            };
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

    }
}
