using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
    public class PermissionResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleCount { get; set; } // تعداد نقش‌هایی که این دسترسی را دارند
    }
}
