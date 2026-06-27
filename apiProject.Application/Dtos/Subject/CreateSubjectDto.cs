using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Subject
{
   public class CreateSubjectDto
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
    }
}
