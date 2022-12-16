using QBid.APILog;
using QBid.Models;
using QBid.Models.APIResponse;
using QBid.ViewModels;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.Helpers
{
    public class QBidHelper : BindableObject
    {
        public static int LogoutInt = 0;
        public static ObservableCollection<LanguageModel> LanguageDetails;
        public static ObservableCollection<ServiceModel> SelectServiceDetails;
        public static ObservableCollection<QBidStatusDetails> ListOfSelectStatus;

        
        public static List<ServiceResponce> GetRegistrationAPIServiceDetail;
        public static List<LanguageResponce> GetRegistrationAPILanguageDetail;
        public static bool AccceptQuotationStatus { get; set; }
       
        public static List<QbidStatus> QbidStatus { get; set; }
        public static bool IsFilterChecked { get; set; }
        public static int QuotationId { get; set; }
        public static string AdviserName { get; set; }
        public static string FacilityId { get; set; }
        public static string NavigatePage { get; set; }

    
        public static string IsCheckedHind;
        public static int SelectedStatusId;
        public static decimal total;
        public static decimal negotiatedtotal;
        public static decimal totalSaving;
        public static int CardCount;

        public static int NegotiatorId;
        public static List<string> ServiceItemIdList = new List<string>();

        private decimal totalPrice;
        public static bool submitRatingStatus = false;

        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value; OnPropertyChanged(nameof(TotalPrice));
                total = total + totalPrice;

            }
        }

        static QBidHelper()
        {
            LanguageDetails = new ObservableCollection<LanguageModel>();
            SelectServiceDetails = new ObservableCollection<ServiceModel>();
        }

        /// <summary>
        /// method used for validate email.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (!Regex.Match(value, ConstantValues.EMAILREGEX).Success)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);

                return false;
            }
        }

        /// <summary>
        /// Method used for validate empty field.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public static async Task<HttpClient> GetHttpHeaderToken(HttpClient httpClient)
        //{
        //    if (httpClient != null)
        //    {
        //        httpClient.DefaultRequestHeaders.Clear();               
        //        httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
        //        return httpClient;
        //    }
        //    else
        //        return null;
        //}

        public static void AppCenterLogCreate(string responce, string eventName ,string request = null)
        {
            try
            {
                Dictionary<string, string> obj = new Dictionary<string, string>();
                obj.Add("Response", responce);
                LogManager.TraceLogAndEvents(eventName, obj);
                obj.Add("Request", request);
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
       
    }
}
