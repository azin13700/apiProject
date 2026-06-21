using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
   public class RoleResponseDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<string> Permissions { get; set; }


    }
}
