using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
   public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode  { get; set; }
        public string? AvatarUrl { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string TypeOfEmployment { get; set; }
        public bool IsDelete { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; } = new();
        public List<Dependant> Dependants { get; set; } = new();
        public Photo Photo { get; set; } = new();

    }
}
