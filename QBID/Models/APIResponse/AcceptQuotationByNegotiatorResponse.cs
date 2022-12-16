using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class AcceptQuotationByNegotiatorResponse
    {
        public bool Success { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
