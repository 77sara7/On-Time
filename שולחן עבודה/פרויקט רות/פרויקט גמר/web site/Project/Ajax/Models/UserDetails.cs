using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax.Models
{
    public class UserDetails
    {
        public string userId { get; set; }
        public string firstName { get; set; }
        public string password { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public int CreditCompenyId { get; set; }
        public string ownerName { get; set; }
        public string numCredit { get; set; }
        public Nullable<System.DateTime> validDate { get; set; }
        public string cvc { get; set; }
       
    }
}