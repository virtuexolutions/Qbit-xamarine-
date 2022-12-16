using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
  public  class AddNegotiatorBankAccountRequest
    {
        public string accountHolderName { get; set; }
        public string routingNumber { get; set; }
        public string accountNumber { get; set; }
        public string day { get; set; }
        public string month { get; set; }
        public string year { get; set; }
       // public string mobileNumber { get; set; }
        //public string AddressLine1 { get; set; }
        //public string AddressLine2 { get; set; }
        //public string city { get; set; }
        //public string zipCode { get; set; }
        //public string state { get; set; }
        public string country { get; set; }
        public string ip { get; set; }
        public string SSN { get; set; }
        //public string accountId { get; set; }
        //public string bankId { get; set; }
    }

    public class UpdateNegotiatorBankAccountRequest
    {
        public string accountHolderName { get; set; }
        public string routingNumber { get; set; }
        public string accountNumber { get; set; }      
        public string ip { get; set; }
        public string SSN { get; set; }
        public string mobileNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string state { get; set; }
        public string country { get; set; }        
        public string accountId { get; set; }
        public string bankId { get; set; }        
        public string identityDocumentBack { get; set; }
        public string identityDocumentFront { get; set; }
    }

}
