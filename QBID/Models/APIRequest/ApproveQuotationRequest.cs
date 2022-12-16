using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class ApproveQuotationRequest
    {
        public string quotationId { get; set; }
        public string facilityId { get; set; }
        public string negotiatorId { get; set; }
        public string typeId { get; set; }
        public string type { get; set; }
        public List<items> items { get; set; }
    }

    public class items
    {
        public string itemId { get; set; }
    }
}
