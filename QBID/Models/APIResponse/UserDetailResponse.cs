using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class UserDetailResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public UserProfile data { get; set; }
    }
    public class UserProfile
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string mobileText { get; set; }
        public string mobileCall { get; set; }
        public string profilePicture { get; set; }
        public string zipCode { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string TotalSaving { get; set; }
        public List<ContactPreference> ContactPreference { get; set; }
        public QuotationCounts quotationCounts { get; set; }
    }
    public class ContactPreference
    {
        public string name { get; set; }
    }
    public class QuotationCounts
    {
        public int totalCounts { get; set; }
        public int sentToFacility { get; set; }
        public int submittedbyfacility { get; set; }
        public int searchForNegotiator { get; set; }
        public int inProgress { get; set; }
        public int submittedByNegotaitor { get; set; }
        public int WOU { get; set; }
        public int declined { get; set; }
        public int completed { get; set; }
    }


}
