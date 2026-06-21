using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
   public class Permissions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; set; }
        public ICollection<RolePermissions> RolePermissions { get; set; } = new List<RolePermissions>();
    }
}
