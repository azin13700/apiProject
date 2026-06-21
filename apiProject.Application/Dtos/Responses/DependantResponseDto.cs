using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
   public class DependantResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string RelationType { get; set; }

    }
}
