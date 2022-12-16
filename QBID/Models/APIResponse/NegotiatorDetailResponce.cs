using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class NegotiatorDetailResponce
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public NegotiatorProfile data { get; set; }
    }
    public class LanguageResponce
    {
     
        public int id { get; set; }
        public string languageName { get; set; }
    }

    public class ServiceResponce
    {
        public int id { get; set; }
        public string serviceName { get; set; }
        public string experianceTime { get; set; }
    }

    public class NegotiatorProfile
    {
        public int negotiatorId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string mobileText { get; set; }
        public string mobileCall { get; set; }
        public string nickName { get; set; }
        public string zipCode { get; set; }
        public int retired { get; set; }
        public int selfEmployed { get; set; }
        public string companyName { get; set; }
        public List<LanguageResponce> languages { get; set; }
        public List<ServiceResponce> services { get; set; }
        public string profilePicture { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public int displayStatus { get; set; }
        public string negotiatorRating { get; set; }
        public TotalQbid totalQbid { get; set; }
        public CommisionDetails commisionDetails { get; set; }
    }
    public class TotalQbid
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

    public class CommisionDetails
    {
        public double totalBonus { get; set; }
        public double todayCommision { get; set; }
        public double totalEarning { get; set; }
      

    }

}
