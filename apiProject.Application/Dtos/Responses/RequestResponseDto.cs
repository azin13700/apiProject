using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
   public class RequestResponseDto
    {
        public int RequestId { get; set; }
        public string RequestCode { get; set; }
        public string Massage { get; set; }
    }
}
