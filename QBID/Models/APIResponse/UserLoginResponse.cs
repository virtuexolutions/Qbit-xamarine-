using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class UserLoginResponse
    {

        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public UsersDetails data { get; set; }
        public string token { get; set; }
    }
    public class UsersDetails
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string nickName { get; set; }
        public string companyName { get; set; }
        public string retired { get; set; }
        public object selfEmployee { get; set; }
        public string mobileText { get; set; }
        public string mobileCall { get; set; }
        public string zipCode { get; set; }
        public string addressLine1 { get; set; }
        public string CustomerStripeId { get; set; }
        public string addressLine2 { get; set; }
        public string customerStripeId { get; set; }
        public int cardAdded { get; set; }
        public int accountAdded { get; set; }
        public int roleId { get; set; }

        public string bankTransefer { get; set; }
        public string accountStatus { get; set; }

        public bool isRightsAndProvisionAccepted { get; set; }
    }
}
