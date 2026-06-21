using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
   public class Photo
    {
        public int Id { get; set; }

        public byte[]? ImageData { get; set; }     

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

    }
}
