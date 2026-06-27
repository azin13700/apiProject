using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.User
{
  public  class UpdateUserDto
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Family { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

       // public string Password { get; set; }
        public string Status { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string NationalNo { get; set; }
        public string PhoneNumber { get; set; }
        public int UserPhotoId { get; set; }
        public int[] RoleId { get; set; }
        public int[] UnitId { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
