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
        public List<string> Permissions { get; set; }
        public string Token { get; set; }
    }
}
