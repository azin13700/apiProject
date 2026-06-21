using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
   public class RolePermissions
    {
        public int Id { get; set; }
   
        public Permissions Permissions { get; set; } = null!;
        public int PermissionsId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
