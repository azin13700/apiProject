using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Request
{
 public   class ChangeStatusRequestDto
    {
        public bool IsActive { get; set; }
        public int RequestId { get; set; }
    }
}
