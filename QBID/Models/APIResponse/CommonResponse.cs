using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class CommonResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public string data { get; set; }
        public object usersDetails { get; set; }
        public string token { get; set; }
        public string error { get; set; }
        
    }    
}
