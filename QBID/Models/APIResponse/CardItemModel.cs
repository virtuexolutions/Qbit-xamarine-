using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models.APIResponse
{
    public class CardItemModel:BindableObject
    {
        public string id { get; set; }
        public string @object { get; set; }
        public object account { get; set; }
        public object address_city { get; set; }
        public object address_country { get; set; }
        public object address_line1 { get; set; }
        public object address_line1_check { get; set; }
        public object address_line2 { get; set; }
        public object address_state { get; set; }
        public object address_zip { get; set; }
        public object address_zip_check { get; set; }
        public object available_payout_methods { get; set; }
        public string brand { get; set; }
        public string country { get; set; }
        public object currency { get; set; }
        public string customer { get; set; }
        public string cvc_check { get; set; }
        public object default_for_currency { get; set; }
        public object description { get; set; }
        public object dynamic_last4 { get; set; }
        public int exp_month { get; set; }
        public int exp_year { get; set; }
        public string fingerprint { get; set; }
        public string funding { get; set; }
        public object tokenization_method { get; set; }
        public object iin { get; set; }
        public object issuer { get; set; }
        public string last4 { get; set; }
        public Metadata metadata { get; set; }
        public object name { get; set; }
        public object recipient { get; set; }
        public string CardExpiry { get; set; }
        public bool IsDefault { get; set; }
        public Command DeleteCommand { get; set; }
        public Command SetCardDefaultCommand { get; set; }      
        public bool IsSelected { get; set; }
    }
   

}
