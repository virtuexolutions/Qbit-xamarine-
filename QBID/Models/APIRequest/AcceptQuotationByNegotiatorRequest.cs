using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class AcceptQuotationByNegotiatorRequest
    {
        public string quotationId { get; set; }
        public string facilityId { get; set; }
        
    }
}
