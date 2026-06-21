using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
  public  class ChangeStatusDto
    {
        public int EmployeeId { get; set; }
        public bool IsDelete { get; set; }
    }
}
