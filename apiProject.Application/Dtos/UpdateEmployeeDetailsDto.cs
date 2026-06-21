using apiProject.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
  public  class UpdateEmployeeDetailsDto
    {
        public int EmployeeId { get; set; }
        public List<DependantResponseDto> Dependants { get; set; } 
        public List<WorkExperienceResponseDto> WorkExperiences { get; set; } 
    }
}
