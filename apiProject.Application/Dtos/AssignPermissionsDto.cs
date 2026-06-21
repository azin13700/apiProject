using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
   public class AssignPermissionsDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
