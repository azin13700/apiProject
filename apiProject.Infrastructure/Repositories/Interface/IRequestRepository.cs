using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories.Interface
{
  public  interface IRequestRepository
    {
        Task AddRequestAsync(Request  request);
        Task<int> SaveChangesAsync();
        Task<Request?> GetByIdAsync(int id);
        Task UpdateSubjectAsync(Request request);
        Task<List<Request>> GetRequestsByUserIdAsync(int userId);
        Task<List<Request>> GetAllAsync();
        Task AddPhotoAsync(RequestPhoto dto);

    }
}
