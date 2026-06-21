using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
  public  class CreateDependantDto
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string RelationType { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
    }
}
