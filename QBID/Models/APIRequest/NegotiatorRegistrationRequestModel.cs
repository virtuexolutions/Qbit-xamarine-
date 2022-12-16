using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class NegotiatorRegistrationRequestModel
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }
        [JsonProperty("retired")]
        public int Retired { get; set; }
        [JsonProperty("selfEmployed")]
        public int SelfEmployed { get; set; }
        [JsonProperty("nickName")]
        public string NickName { get; set; }
        [JsonProperty("displayNickName")]
        public int displayStatus { get; set; }
        [JsonProperty("mobileText")]
        public string MobileText { get; set; }
        [JsonProperty("mobileCall")]
        public string MobileCall { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("service")]
        public List<Service> Service { get; set; }
        [JsonProperty("language")]
        public List<Language> Language { get; set; }

    }

    public class Service
    {
        public int serviceId { get; set; }
        public string experienceTime { get; set; }
    }

    public class Language
    {
        public int languageId { get; set; }
    }
}