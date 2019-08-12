using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax.Models
{
    public class UserAPayment
    {
        public User User { get; set; }
        public PaymentDetails payment  { get; set; }
        public int CreditCompenyId { get; set; }

    }

}