using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
  public  class Request
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public RequestPhoto? Photo  { get; set; } = new();

        public Subject? SubSubject { get; set; } 
        public Unit? Unit { get; set; } 
        public int UnitId { get; set; }
        public int SubSubjectId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } 


    }
}
