using apiProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace apiProject.Infrastructure.Data
{
   public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
      .HasOne(u => u.UserPhoto)
      .WithOne(p => p.User)
      .HasForeignKey<UserPhoto>(p => p.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RolePermissions>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermissions>()
                .HasOne(rp => rp.Permissions)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionsId);

            modelBuilder.Entity<UserUnit>()
                .HasOne(uu => uu.User)
                .WithMany(u => u.UserUnits)
                .HasForeignKey(uu => uu.UserId);

            modelBuilder.Entity<UserUnit>()
                .HasOne(uu => uu.Unit)
                .WithMany(u => u.UserUnits)
                .HasForeignKey(uu => uu.UnitId);

            modelBuilder.Entity<Employee>()
             .HasMany(x => x.Dependants)
             .WithOne(x => x.Employee)
             .HasForeignKey(x => x.EmployeeId);

            modelBuilder.Entity<Employee>()
           .HasMany(x => x.WorkExperiences)
           .WithOne(x => x.Employee)
           .HasForeignKey(x => x.EmployeeId);

            modelBuilder.Entity<Employee>()
              .HasOne(x => x.Photo)
              .WithOne(x => x.Employee)
              .HasForeignKey<Photo>(x=>x.EmployeeId);

            modelBuilder.Entity<Subject>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Childern)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.SubSubject)
                .WithMany()
                .HasForeignKey(r => r.SubSubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Unit)
                .WithMany()
                .HasForeignKey(r => r.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.CreatedByUser)
                .WithMany()
                .HasForeignKey(r => r.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                  .HasOne(u => u.Photo)
                  .WithOne(p => p.Request)
                     .HasForeignKey<Request>(p => p.Id);

        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestPhoto> RequestPhoto { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Dependant> Dependants { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserPhoto> UserPhoto { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserUnit> UserUnit { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
//dotnet ef migrations add editSubject
//    --project apiProject.Infrastructure\apiProject.Infrastructure.csproj 
//    --startup-project apiProject.Api\apiProject.Api.csproj
