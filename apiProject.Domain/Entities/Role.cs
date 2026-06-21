using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
  public  class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
  
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public ICollection<RolePermissions> RolePermissions { get; set; } = new List<RolePermissions>();
    }
}
