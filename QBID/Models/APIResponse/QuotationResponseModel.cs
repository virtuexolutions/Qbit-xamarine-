using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class QuotationResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public LData data { get; set; }
        public object usersDetails { get; set; }
        public string token { get; set; }
        public string error { get; set; }
    }
    public class LData
    {
        public int quptationId { get; set; }
        public string facilityId { get; set; }
    }
}
