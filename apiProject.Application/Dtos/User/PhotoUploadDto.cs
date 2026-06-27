using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.User
{
   public class PhotoUploadDto
    {
        public int EmployeeId { get; set; }

      [Required]
     public IFormFile File { get; set; } = null!;
    }
}
