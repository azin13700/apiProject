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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddSubjectAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
        }

        public async Task<List<Subject>> GetAllMainSubjectAsync()
        {
            return await _context.Subjects
                      .Where(x=>x.ParentId == null)
                      .ToListAsync();
        }

        public  async  Task<List<Subject>> GetChildrenAsync(int parentId)
        {
            return await _context.Subjects
              .Where(x => x.ParentId == parentId)
              .ToListAsync();
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _context.Subjects.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<Subject?> GetWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public  Task UpdateSubjectAsync(Subject subject)
        {
             _context.Subjects.Update(subject);
            return Task.CompletedTask;
        }
    }
}
