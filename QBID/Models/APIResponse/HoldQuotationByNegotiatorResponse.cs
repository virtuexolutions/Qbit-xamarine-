using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
   public class HoldQuotationByNegotiatorResponse
    {
        public class Root
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public int Code { get; set; }
            public object Data { get; set; }
        }

    }
}
