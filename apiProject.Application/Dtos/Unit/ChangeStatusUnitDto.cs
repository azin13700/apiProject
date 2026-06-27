using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Unit
{
   public class ChangeStatusUnitDto
    {
        public bool IsActive { get; set; }
        public int UnitId { get; set; }
    }
}
