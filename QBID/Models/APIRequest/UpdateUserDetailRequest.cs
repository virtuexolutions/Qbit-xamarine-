using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class UpdateUserDetailRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileText { get; set; }
        public string mobileCall { get; set; }
        public string zipCode { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public List<PreferredTypeRequest> preferredType { get; set; }
    }
    public class PreferredTypeRequest
    {
        public int typeId { get; set; }
    }


}
