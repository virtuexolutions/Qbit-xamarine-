using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class CurrentStatus
    {
        public string statusName { get; set; }
        public int statusId { get; set; }
        public string createdTime { get; set; }
        public string updatedTime { get; set; }
        public bool? currentStatus { get; set; }
    }

    public class QuotationQbidStatusResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public Data data { get; set; }
        public string Error { get; set; }
    }
    public class Data
    {
        public List<CurrentStatus> CurrentStatus { get; set; }
    }
}
