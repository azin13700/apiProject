using apiProject.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Permission
{
   public class PermissionsGroupDto
    {
        public string Category { get; set; }
        public List<PermissionResponseDto> Permissions { get; set; }
    }
}
