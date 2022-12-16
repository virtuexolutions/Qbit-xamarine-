using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class UserRegistrationRequestModel
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("mobileText")]
        public string MobileText { get; set; }
        [JsonProperty("mobileCall")]
        public string MobileCall { get; set; }
        [JsonProperty("roleId")]
        public string RoleId { get; set; }
        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        public List<PreferredType> preferredType { get; set; }
        
    }
    public class PreferredType {
        public int typeId { get; set; }
    }
}
