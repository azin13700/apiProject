using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Request
{
  public  class SearchRequestDto
    {
        public List<int>? UnitId { get; set; }
        public List<int>? SubjectId { get; set; }
        public List<int>?  SubSubjectId{ get; set; }
        public string? RequestCode { get; set; }

        public int UserId { get; set; }
    }
}
