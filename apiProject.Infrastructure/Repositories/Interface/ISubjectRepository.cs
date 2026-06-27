using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
 public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllMainSubjectAsync();
        Task<Subject?> GetSubjectByIdAsync(int id);
        Task<Subject?> GetWithDetailsAsync(int id);
        Task AddSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject);
        Task<int> SaveChangesAsync();
        Task<List<Subject>> GetChildrenAsync(int parentId);
    }
}
