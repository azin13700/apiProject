using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
  public  class WorkExperienceResponseDto
    {
        public int WorkExperienceId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string FromYear { get; set; } = string.Empty;
        public string ToYear { get; set; } = string.Empty;
        public string RelationType { get; set; } = string.Empty;

    }
}
