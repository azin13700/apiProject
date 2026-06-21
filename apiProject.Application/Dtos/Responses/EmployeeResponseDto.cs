using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
   public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public bool IsDelete { get; set; }

        public DateTime DateOfEmployment { get; set; }
        public string TypeOfEmployment { get; set; } = string.Empty;
        public List<DependantResponseDto> Dependants { get; set; } = [];
        public List<WorkExperienceResponseDto> WorkExperiences { get; set; } = [];
        public byte[]? Photo { get; set; }

    }
}
