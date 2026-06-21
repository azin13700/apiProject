using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
   public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Family { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string NationalNo { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public UserPhoto? UserPhoto { get; set; } = new();
        // ارتباط یک‌به‌چند با جدول واسط UserRole

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserUnit> UserUnits { get; set; } = new List<UserUnit>();


    }
}
