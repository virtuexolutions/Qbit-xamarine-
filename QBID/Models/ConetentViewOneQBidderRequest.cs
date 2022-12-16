using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models
{
    public class ConetentViewOneQBidderRequest
    {
        public string ServiceItemName { get; set; }
        public string OriginalPrice { get; set; }
        public string OEM { get; set; }
        public string NegotiatorPrice { get; set; }
        public string ApprovedPrice { get; set; }
        public string FacilityPrice { get; set; }
        public string QbidderOEM { get; set; }
        public string QBidderPrice { get; set; }
        public string QBidderApprovedPrice { get; set; }
    }
}
