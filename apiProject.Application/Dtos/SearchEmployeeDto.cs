using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
   public class SearchEmployeeDto
    {
        public string? Name { get; set; }   
        public string? Family { get; set; }
        public string? NationalCode { get; set; } 
        public string? CompanyName { get; set; }
        public int? Gender { get; set; }
        public string? TypeOfEmployment { get; set; }
        public string? FromYear { get; set; }
        public string? ToYear { get; set; }
    }
}
