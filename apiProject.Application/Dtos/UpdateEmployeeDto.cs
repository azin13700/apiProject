using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace apiProject.Application.Dtos
{
    public class UpdateEmployeeDto
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Family { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;

        [Required]
        public IFormFile File { get; set; } = null!;
        public DateTime DateOfEmployment { get; set; }

        public string TypeOfEmployment { get; set; } = string.Empty;
    }
}