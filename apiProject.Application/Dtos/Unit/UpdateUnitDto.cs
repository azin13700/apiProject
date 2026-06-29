using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Unit
{
   public class UpdateUnitDto
    {
        [Required]
        public int UnitId { get; set; }

        [Required(ErrorMessage = "نام واحد الزامی است")]
        [MaxLength(100)]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
