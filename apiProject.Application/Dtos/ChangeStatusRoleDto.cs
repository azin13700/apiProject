using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
  public  class ChangeStatusRoleDto
    {
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
    }
}
