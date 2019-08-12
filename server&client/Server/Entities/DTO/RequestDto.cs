using System;
using System.Diagnostics;

namespace Entities.DTO
{
    public class RequestDto
    {
         
        public EventLog eventLog { get; set; }
        public string content { get; set; }
        public int request_id { get; set; }
        public string request_name { get; set; }
        public System.DateTime date_from { get; set; }
        public System.DateTime date_to { get; set; }
        public int user_id { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<int> hour { get; set; }
        public int frequency_id { get; set; }
        public byte[] file_stream { get; set; }
        public byte[] recording_stream { get; set; }
        public System.Guid file_id { get; set; }
        public System.Guid recording_id { get; set; }
        public bool is_relevant { get; set; }
        public string userMail { get; set; }

    }
}
