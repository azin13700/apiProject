
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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(x=>x.Dependants)
                .Include(x=>x.WorkExperiences)
                .Include(x=>x.Photo)
             
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee?> GetWithDetailsAsync(int id)
        {
            return await _context.Employees
                .Include(x => x.Dependants)
                .Include(x => x.WorkExperiences)
                     .Include(x => x.Photo)
                .FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public  Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return Task.CompletedTask;
        }

        public  Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task AddPhotoAsync(Photo photo)
        {
            await _context.Photo.AddAsync(photo);
        }
    }
}
