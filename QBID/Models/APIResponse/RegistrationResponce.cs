using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
       public class RegistrationResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public StripeResponse data { get; set; }
        public string error { get; set; }

    }

    public class StripeResponse
    {
        public string customerId { get; set; }
        public string token { get; set; }
    }
}
