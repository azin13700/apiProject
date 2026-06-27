using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Dependancy
{
  public  class CreateWorkExperienceDto
    {
        public string CompanyName { get; set; } = string.Empty;

        public string FromYear { get; set; } = string.Empty;

        public string ToYear { get; set; } = string.Empty;

        public int EmployeeId { get; set; }
        public string RelationType { get; set; } = string.Empty;

    }
}
