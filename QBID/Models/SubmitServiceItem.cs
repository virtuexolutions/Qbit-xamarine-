using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models
{
    public class SubmitServiceItem
    {
        public string TotalPrice { get; set; }
        public string NegotiatedPrice { get; set; }
        public string TotalSaving { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsShowHide { get; set; }
        public List<string> ServiceitemId { get; set; }
    }
}
