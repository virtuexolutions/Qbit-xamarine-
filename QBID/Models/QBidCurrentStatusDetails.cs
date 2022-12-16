using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models
{
    public class QBidCurrentStatusDetails
    {
        public string statusName { get; set; }
        public int statusId { get; set; }
        public string updatedTime { get; set; }
        public bool currentStatus { get; set; }
        public string statusColor { get; set; }
        public string QBidStatusBackGroundColor { get; set; }
        public string QBidStatusTextColor { get; set; }
    }
}
