using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
  public  class UnitResponseDto
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
        public bool IsActive { get; set; }
        public int UserCount { get; set; }
        public int ChildrenCount { get; set; }
        public string CreatedAt { get; set; }
    }
}
