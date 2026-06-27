using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.Subject;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;
        public SubjectService(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public Task ChangeStatusAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<SubjectDto?> GetByIdAsync(int id)
        {
            var subject = await _repository.GetSubjectByIdAsync(id);

            if (subject == null)
                return null;
            return new SubjectDto
            {
                SubjectId = subject.Id,
                Title = subject.Title,
                ParentId = subject.ParentId,
                IsActive = subject.IsActive

            };

        }

        public async  Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto dto)
        {
            var subject = new Subject
            {
                Title = dto.Title,
                ParentId = dto.ParentId,
                IsActive = true
            };

            await _repository.AddSubjectAsync(subject);
            await _repository.SaveChangesAsync();

            return new SubjectDto
            {
                SubjectId = subject.Id,
                Title = subject.Title,
                ParentId = subject.ParentId,
                IsActive = subject.IsActive
            };

        }

        public async Task<List<SubjectResponse>> GetAllSubjectAsync()
        {
            var result = await _repository.GetAllMainSubjectAsync();
            return result.Select(sub => new SubjectResponse
            {
                SubjectId = sub.Id,
               IsActive = sub.IsActive ,
               ParentId = sub.ParentId,
               Title = sub.Title    
   
            }).ToList();
        }

        public async Task UpdateAsync( int id,UpdateSubjectDto dto)
        {
            var user = await _repository.GetSubjectByIdAsync(id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد");
            }


            user.Title = dto.Title;
        
            await _repository.UpdateSubjectAsync(user);

            await _repository.SaveChangesAsync();

        }

        public async Task<List<SubjectDto?>> GetSubSubjectByIdAsync(int parentId)
        {
            var subjects = await _repository.GetChildrenAsync(parentId);

            if (subjects == null)
                return null;
            return subjects.Select(sub => new SubjectDto
            {
                SubjectId = sub.Id,
                Title = sub.Title,
                ParentId = sub.ParentId,
                IsActive = sub.IsActive

            }).ToList();

         
        }

        public async  Task<List<SelectedSubSubjectDto?>> GetSubSubjectForRequest(int subjectId)
        {
            var subjects = await _repository.GetChildrenAsync(subjectId);
            if (subjects == null)
                return null;
            return subjects.Select(sub => new SelectedSubSubjectDto
            {
                SubjectId = sub.Id,
                Title = sub.Title,
                ParentId = sub.ParentId  

            }).ToList();

        }

        public async Task<bool?> ChangeStatus(ChangeStatusSubjectDto dto)
        {
            var subject = await _repository.GetSubjectByIdAsync(dto.SubjectId);

            if (subject == null)
                return null;

            subject.IsActive = !subject.IsActive;
            await _repository.UpdateSubjectAsync(subject);
            await _repository.SaveChangesAsync();
            return true;

        }
    }
}
