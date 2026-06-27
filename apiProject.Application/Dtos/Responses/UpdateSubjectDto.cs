using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos.Responses
{
   public class UpdateSubjectDto
    {
        [Required]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "عنوان الزامی است")]
        [MaxLength(100, ErrorMessage = "عنوان حداکثر 100 کاراکتر می‌تواند باشد")]
        public string Title { get; set; }

        public int? ParentId { get; set; }  // ✅ nullable

        public bool IsActive { get; set; } = true;

    }
}
