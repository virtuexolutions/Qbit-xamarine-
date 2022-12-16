using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class StripeKeyResponce
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public StripeKey data { get; set; }
    }

    public class StripeKey
    {
        public string stripeKey { get; set; }
        public string stripeSecret { get; set; }
    }

  
}
