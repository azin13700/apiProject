using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Request
{
  public  class CreateRequestDto
    {
        public string Description { get; set; }
        public int UnitId { get; set; }
        public int SubSubjectId { get; set; }
        public int CreatedByUserId { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
