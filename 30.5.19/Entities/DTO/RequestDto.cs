using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class RequestDto
    {

        public int request_id { get; set; }
        public string request_name { get; set; }
        public System.DateTime date_from { get; set; }
        public System.DateTime date_to { get; set; }
        public int user_id { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<int> hour { get; set; }
        public Nullable<int> day_in_month { get; set; }
        public int frequency_id { get; set; }
        public byte[] file_stream { get; set; }
        public byte[] recording_stream { get; set; }
        public string file_name { get; set; }
        public System.Guid file_id { get; set; }
        public System.Guid recording_id { get; set; }
        public bool is_relevant { get; set; }
        public bool IsAuthorized { get; set; }
        public string ErrorMessage { get; set; }
        public string userMail { get; set; }
        // public virtual User1 User { get; set; }

    }
}
