using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class LogoutModel
    {
        public string deviceToken { get; set; } 
        public string roleId { get; set; }  
    }
}
