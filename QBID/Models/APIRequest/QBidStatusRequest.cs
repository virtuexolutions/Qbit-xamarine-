using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class QBidStatusRequest
    {
        [JsonProperty("quotationId")]
        public string QuotationId { get; set; }
    }
}
