using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
  public  class UserResponseDto
    {
        public int UserId { get; set; } 
        public string FullName { get; set; } 
        public byte[]? Photo { get; set; }
        public List<string> Role { get; set; } 
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Status { get; set; }
        public DateOnly createdAt { get; set; }
        public bool IsActive { get; set; }
        public List<string> Unit { get; set; }

    }
}
