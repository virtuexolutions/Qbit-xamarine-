using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
   public class SubmitQuotationByNegotiatorRequest
    {
        public string quotationId { get; set; }
        public string facilityId { get; set; }
        public string benefitNote { get; set; }
        public string specialNote { get; set; }
        public List<NegotiatedPrice> negotiatedPrice { get; set; }
    }
    public class NegotiatedPrice
    {
        public string itemId { get; set; }
        public string newPrice { get; set; }
        
    }
}
