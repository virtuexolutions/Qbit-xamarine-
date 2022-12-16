using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class HoldQuotationByNegotiatorRequest
    {
        public string quotationId { get; set; }
        public string facilityId { get; set; }
        public string message { get; set; }
        public string reason { get; set; }
    }
}
