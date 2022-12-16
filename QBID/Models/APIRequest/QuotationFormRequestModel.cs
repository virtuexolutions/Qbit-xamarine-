using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
   public class QuotationFormRequestModel
    {
        public string representativeName { get; set; } = "jack";
        public long contact { get; set; } = 9876543256;
        public string email { get; set; } = "jack@yopmail.com";
        public string vendorName { get; set; }= "qbid";
        public string zipCode { get; set; } = "12365";
        public string workOrder { get; set; } = "dff";
        public string mileage { get; set; }
        public string VIN { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string serviceType { get; set; }
        public string repairItemName { get; set; }
        public string price { get; set; } = "150";
        public string OEM { get; set; }
        public string[] attachment{ get; set; }
    }
    public class Attachment
    {
        public string attachments { get; set; }
    }
}
