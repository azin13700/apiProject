using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }

        public string Title { get; set; }

        public int? ParentId { get; set; }

        public bool IsActive { get; set; }
    }
}
