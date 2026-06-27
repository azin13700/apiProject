using apiProject.Application.Dtos.Request;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.Subject;
using apiProject.Application.Dtos.User;
using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services.Interface
{
    public  interface ISubjectService
    {
        Task<List<SubjectResponse>> GetAllSubjectAsync();
        Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto dto);
        Task UpdateAsync(int id, UpdateSubjectDto dto);
        Task<bool?> ChangeStatus(ChangeStatusSubjectDto dto);

        Task ChangeStatusAsync(int id);
        Task<SubjectDto?> GetByIdAsync(int id);
        Task<List<SubjectDto?>> GetSubSubjectByIdAsync(int parentId);
        Task<List<SelectedSubSubjectDto?>> GetSubSubjectForRequest(int subjectId );
    }
}
