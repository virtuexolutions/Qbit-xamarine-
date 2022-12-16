using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class DeclinedQuotationRequest
    {
        public string quotationId { get; set; }
        public string facilityId { get; set; }
        public string negotiatorId { get; set; }
    }
}
