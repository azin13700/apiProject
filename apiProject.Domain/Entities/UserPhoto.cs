using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Domain.Entities
{
  public  class UserPhoto
    {
        public int Id { get; set; }

        public byte[]? ImageData { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
