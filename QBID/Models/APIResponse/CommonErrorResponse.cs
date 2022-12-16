using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class CommonErrorResponse
    {
        public bool success { get; set; }
        public int code { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }

    public class RegistrationError
    {
        public List<string> errors { get; set; }
    }
}
