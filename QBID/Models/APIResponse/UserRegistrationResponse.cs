using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class UserRegistrationResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public UserDetails data { get; set; }
        public string token { get; set; }
    }

    public class UserDetails
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileText { get; set; }
        public string mobileCall { get; set; }
        public string email { get; set; }
        public string zipCode { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
    }
}
