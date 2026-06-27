using apiProject.Application.Dtos.Dependancy;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;

namespace apiProject.Application.Services
{
    public class DependantService : IDependantService
    {
        private readonly IDependantRepository _repository;

        public DependantService(IDependantRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DependantResponseDto>> GetByEmployeeIdAsync(int employeeId)
        {
            var data = await _repository.GetByEmployeeIdAsync(employeeId);

            return data.Select(x => new DependantResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Family = x.Family,
                DateOfBirth = x.DateOfBirth,
                RelationType = x.relationType
            }).ToList();
        }

        public async Task<DependantResponseDto?> GetByIdAsync(int id)
        {
            var x = await _repository.GetByIdAsync(id);

            if (x == null) return null;

            return new DependantResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Family = x.Family,
                DateOfBirth = x.DateOfBirth,
                RelationType = x.relationType
            };
        }

        public async Task<int> CreateAsync(CreateDependantDto dto)
        {
            var dependant = new Dependant
            {
                Name = dto.Name,
                Family = dto.Family,
                DateOfBirth = dto.DateOfBirth,
                relationType = dto.RelationType,
                EmployeeId = dto.EmployeeId
            };

            await _repository.AddAsync(dependant);
            await _repository.SaveChangesAsync();

            return dependant.Id;
        }

        public async Task UpdateAsync(int id, UpdateDependantDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return;

            entity.Name = dto.Name;
            entity.Family = dto.Family;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.relationType = dto.RelationType;

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
                await _repository.SaveChangesAsync();
            }
        }
    }
}