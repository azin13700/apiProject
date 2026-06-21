using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
  public  class CreateRoleDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
