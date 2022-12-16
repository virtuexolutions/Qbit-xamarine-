using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{     
    public class UpdateNegotiatorDetailRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string nickName { get; set; }
        public int displayStatus { get; set; }
        public string companyName { get; set; }
        public int retired { get; set; }
        public int selfEmployee { get; set; }
        public string mobileText { get; set; }
        public string mobileCall { get; set; }
        public string email { get; set; }
        public string zipCode { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public List<ServiceRequest> service { get; set; }
        public List<LanguageRequest> language { get; set; }
    }
    public class ServiceRequest
    {
        public int serviceId { get; set; }
        public string experienceTime { get; set; }
    }

    public class LanguageRequest
    {
        public int languageId { get; set; }
    }

}
