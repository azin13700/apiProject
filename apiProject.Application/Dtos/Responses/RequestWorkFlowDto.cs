using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
    public class RequestWorkFlowDto
    {
        public int Id { get; set; }

        public string SubjectTitle { get; set; }

        public string SubSubjectTitle { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UnitName { get; set; }
        public string RequestCode { get; set; }

    }
}
