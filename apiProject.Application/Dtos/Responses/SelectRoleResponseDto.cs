using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
    public class SelectRoleResponseDto
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int UnitId { get; set; }

        public string UnitName { get; set; } = string.Empty;

        public List<string> Permissions { get; set; } = new();

    }
}
