using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
   public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentUnitId { get; set; }


        public int? ParentId { get; set; }
        public bool IsActive { get; set; }

        public Subject Parent { get; set; }
        public ICollection<Subject> Childern { get; set; } = new List<Subject>();
    }
}
