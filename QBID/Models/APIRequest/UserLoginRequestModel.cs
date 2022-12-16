using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class UserLoginRequestModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("deviceType")]
        public string DeviceType { get; set; }
        [JsonProperty("deviceToken")]
        public string DeviceToken { get; set; }

    }
}
