using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models.APIResponse
{
    public class QuatationListResponse:BindableObject
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public List<QuotationList> data { get; set; }
        public Paginations paginations { get; set; }
        
    }

    public class Link
    {
        public string url { get; set; }
        public string label { get; set; }
        public bool active { get; set; }
    }
    public class Paginations
    {
        public int currentPage { get; set; }
        public string firstPageUrl { get; set; }
        public int from { get; set; }
        public object priviousPageUrl { get; set; }
        public string nextPageUrl { get; set; }
        public int to { get; set; }
        public int total { get; set; }
        public List<Link> links { get; set; }
    }

    public class QbidCurrentStatus
    {
        public string statusName { get; set; }
        public int statusId { get; set; }
        public string createdTime { get; set; }
        public DateTime updatedTime { get; set; }
        public bool currentStatus { get; set; }
    }

    public class QbidStatus
    {
        public int statusId { get; set; }
        public string statusName { get; set; }
    }

    public class Price
    {
        public int serviceItemId { get; set; }
        public int facilityId { get; set; }
        public string facilityPrice { get; set; }
        public int negotiatorId { get; set; }
        //public int negotiatedPrice { get; set; }
        public string  negotiatedPrice { get; set; }
        public int activeNegotiator { get; set; }
        public int approved { get; set; }
    }

    public class ServiceItem:BindableObject
    {
        public int QuotationId { get; set; }
        public int FacilityId { get; set; }
        public int ServiceItemId { get; set; }
        public string ServcieItemName { get; set; }
        public string ServcieItemPrice { get; set; }
        public string NegotiablePrice { get; set; }
        public string OEM { get; set; }
        public int IsOtherFacility { get; set; }
        public List<Prices> Price { get; set; }
        public bool OtherFacility { get; set; }
        public Command UserAccptedCommmand { get; set; }
     
        private bool isAcceptedByMember;

        public bool IsAcceptedByMember
        {
            get { return isAcceptedByMember; }
            set { isAcceptedByMember = value; OnPropertyChanged(nameof(IsAcceptedByMember)); }
        }
        public int? Index { get; set; }

    }

    public class Prices
    {
        public int ServiceItemId { get; set; }
        public int FacilityId { get; set; }
        public string FacilityPrice { get; set; }
        public int NegotiatorId { get; set; }
        
        public string  NegotiatedPrice { get; set; }
        public int ActiveNegotiator { get; set; }
        public bool Approved { get; set; }
    }

    public class NegotiatorDetails
    {
        public int NegotiatorId { get; set; }
        public string FirstName { get; set; }
        public string Last_name { get; set; }
        public string NegotiatorEmail { get; set; }
        public string Nick_name { get; set; }
        public string MobileText { get; set; }
        public string MobileCall { get; set; }
        public string SelfEmployee { get; set; }
        public string NegotiatorRating { get; set; }
        public string Retired { get; set; }
        public object Company_name { get; set; }
        public string NicknameDisplayStatus { get; set; }
        public string ProfilePicture { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public bool ActiveNegotiator { get; set; }

    }

   
    public class PaymentDetail
    {
        public string totalSaving { get; set; }
        public string transactionStatus { get; set; }
        public string Charge { get; set; }
        public string Fee { get; set; }
        public string OrderId { get; set; }
        public string transactionId { get; set; }
        [JsonProperty("Approved/Declined")]
        public string ApprovedDeclined { get; set; }
        public string dateApproved { get; set; }
    }
    public class QuotationList
    {
        public int QuotationId { get; set; }       
        public string FacilityEmailQuotationLink { get; set; }
        public string QuotationEmail { get; set; }
        public int UserId { get; set; }
        public int facilityId { get; set; }
        public int SubmitStatus { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAdvisor { get; set; }
        public string FacilityContact { get; set; }
        public string ZipCode { get; set; }
        public string FacilityAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string serviceName { get; set; }
        public string WorkOrder { get; set; }
        public string VIN { get; set; }
        public int? Mileage { get; set; }
        public string SpecialNote { get; set; }
        public string OtherNote { get; set; }
        public string ReadNote { get; set; }
        public string Benefits { get; set; }
        public string FacilityEmail { get; set; }
       
        public List<Attachment> attachmentUrl { get; set; }
        public string negotiatorEarning { get; set; }
        public string approvedChargeToUser { get; set; }
        public List<ServiceItem> ServiceItem { get; set; }
        public List<NegotiatorDetails> NegotiatorDetails { get; set; }
        public List<QbidCurrentStatus> QbidCurrentStatus { get; set; }
        public List<QbidStatus> QbidStatus { get; set; }
        public List<PaymentDetail> paymentDetails { get; set; }
        public List<UserDetailsInfo> UserDetails { get; set; }

    }

    public class Attachment
    {
        public string attachments { get; set; }
    }

    public class UserDetailsInfo

    {

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string userEmail { get; set; }

        public string mobileText { get; set; }

        public string mobileCall { get; set; }

        public string zipCode { get; set; }

        public string addressLine1 { get; set; }

        public string addressLine2 { get; set; }

        public string profilePicture { get; set; }

    }
}
