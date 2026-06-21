
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Dtos
{
   public class CreateEmployeeDto
    {
        public string Name { get; set; }

        public string Family { get; set; }

        public string NationalCode { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public string TypeOfEmployment { get; set; }

        public IFormFile? Photo { get; set; }


    }




}
