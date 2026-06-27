using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Subject
{
   public class ChangeStatusSubjectDto
    {
        public bool IsActive { get; set; }
        public int SubjectId { get; set; }
    }
}
