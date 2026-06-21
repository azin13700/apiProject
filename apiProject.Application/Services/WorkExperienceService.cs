

using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;

namespace apiProject.Application.Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IWorkExperienceRepository _repository;

        public WorkExperienceService(IWorkExperienceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<WorkExperienceResponseDto>> GetByEmployeeIdAsync(int employeeId)
        {
            var data = await _repository.GetByEmployeeIdAsync(employeeId);

            return data.Select(x => new WorkExperienceResponseDto
            {
                WorkExperienceId = x.Id,
                CompanyName = x.CompanyName,
                FromYear = x.FromYear,
                ToYear = x.ToYear,
                RelationType = x.relationType
            }).ToList();
        }

        public async Task<WorkExperienceResponseDto?> GetByIdAsync(int id)
        {
            var x = await _repository.GetByIdAsync(id);

            if (x == null)
                return null;

            return new WorkExperienceResponseDto
            {
                WorkExperienceId = x.Id,
                CompanyName = x.CompanyName,
                FromYear = x.FromYear,
                ToYear = x.ToYear,
                RelationType =  x.relationType
            };
        }

        public async Task<int> CreateAsync(CreateWorkExperienceDto dto)
        {
            var entity = new WorkExperience
            {
                CompanyName = dto.CompanyName,
                FromYear = dto.FromYear,
                ToYear = dto.ToYear,
                EmployeeId = dto.EmployeeId,
                relationType = dto.RelationType
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(int id, UpdateWorkExperienceDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return;

            entity.CompanyName = dto.CompanyName;
            entity.FromYear = dto.FromYear;
            entity.ToYear = dto.ToYear;
            entity.relationType = dto.RelationType;

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return;

            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();
        }
    }
}