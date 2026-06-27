using apiProject.Domain.Entities;
using apiProject.Infrastructure.Data;
using apiProject.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Infrastructure.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPhotoAsync(RequestPhoto dto)
        {
            await _context.RequestPhoto.AddAsync(dto);
        }

        public async Task AddRequestAsync(Request request)
        {
            await _context.Requests.AddAsync(request);

        }

        public Task<List<Request>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Request?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async  Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task UpdateSubjectAsync(Request request)
        {
            _context.Requests.Update(request);
            return Task.CompletedTask;
        }

        public async Task<List<Request>> GetAllDashboardAsync()
        {
            return await _context.Requests
                .Include(x => x.SubSubject)
                .Include(x => x.Unit)
                .Include(x => x.CreatedByUser)
                .Include(x => x.Photo)
                .ToListAsync();
        }

        public async Task<List<Request>> GetRequestsByUserIdAsync(int userId)
        {
           var result =  _context.Requests
                .Include(x => x.Unit)
                    .ThenInclude(x => x.UserUnits)

                .Include(x => x.SubSubject)
                    .ThenInclude(x => x.Parent)

                .Where(x => x.Unit.UserUnits.Any(u => u.UserId == userId))
                .ToListAsync();

            return await result;
        }
    }
}
