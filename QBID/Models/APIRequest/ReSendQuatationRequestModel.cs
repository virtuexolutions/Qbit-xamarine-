using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
  public class ReSendQuatationRequestModel
    {
            public string quotationId { get; set; }
            public string facilityId { get; set; }
            public string oldEmail { get; set; }
            public string newEmail { get; set; }
        
    }
}
