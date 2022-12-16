using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class GetNegotiatorBankDetailResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public DataResponse data { get; set; }
    }

    public class DataResponse
    {
        public int negotiatorId { get; set; }
        public string Id { get; set; }
        public string account { get; set; }
        public string AccountNumberLast4Digit { get; set; }
        public string routing { get; set; }
        public string accountHolderName { get; set; }
        public string bankName { get; set; }       
        public string currency { get; set; }
        public string mobileNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
        public string accountStatus { get; set; }
        public string bankTransefer { get; set; }
        public bool SSNProvided { get; set; }
        public List<string> requirement { get; set; }

    }

   

}
