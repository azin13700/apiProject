using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Dependancy
{
   public class UpdateDependantDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string RelationType { get; set; }
    }
}
