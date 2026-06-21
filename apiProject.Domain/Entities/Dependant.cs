using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
  public  class Dependant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string relationType { get; set; }
        public bool IsDelete { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

 
    }
}
