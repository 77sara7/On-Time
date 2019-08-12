using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UserDto
    {
        public bool IsAuthorized { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public string name { get; set; }
       public int user_id { get; set; }
        public string ErrorMessage { get; set; }
    }
}
