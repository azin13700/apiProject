using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
   public class Unit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public ICollection<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
    }
}
