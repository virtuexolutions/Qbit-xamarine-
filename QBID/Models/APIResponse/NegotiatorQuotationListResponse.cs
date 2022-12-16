using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class NegotiatorQuotationListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public List<QuotationDetails> Data { get; set; }
        public Pagination paginations { get; set; }
    }

    public class Links
    {
        public string url { get; set; }
        public string label { get; set; }
        public bool active { get; set; }
    }
    public class Pagination
    {
        public int currentPage { get; set; }
        public string firstPageUrl { get; set; }
        public int from { get; set; }
        public string priviousPageUrl { get; set; }
        public string nextPageUrl { get; set; }
        public int to { get; set; }
        public int total { get; set; }
        public List<Links> links { get; set; }
    }

    public class ServiceItemDetails
    {
        public int QuotationId { get; set; }
        public int FacilityId { get; set; }
        public int ServiceItemId { get; set; }
        public string ServcieItemName { get; set; }
        public string ServcieItemPrice { get; set; }
        public string OEM { get; set; }
        public int IsOtherFacility { get; set; }
        public List<object> Price { get; set; }
    }

    public class TotalPrice
    {
        public double TotalNegotiatedPrice { get; set; }
        public double ApprovedPrice { get; set; }
        public double TotalQbidPrice { get; set; }
    }

    public class UserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string MobileText { get; set; }
        public string MobileCall { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }

    public class QbidCurrentStatusDetails
    {
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int CurrentStatus { get; set; }
    }

    public class QbidStatu
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class QuotationDetails
    {
        public int QuotationId { get; set; }
        public string FacilityEmailQuotationLink { get; set; }
        public int UserId { get; set; }
        public int facilityId { get; set; }
        public int SubmitStatus { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAdvisor { get; set; }
        public string FacilityContact { get; set; }
        public string ZipCode { get; set; }
        public string FacilityAddress { get; set; }
        public string serviceName { get; set; }
        public string WorkOrder { get; set; }
        public string VIN { get; set; }
        public int? Mileage { get; set; }
        public object SpecialNote { get; set; }
        public object OtherNote { get; set; }
        public object ReadNote { get; set; }
        public object Benefits { get; set; }
        public List<Attachments> attachmentUrl { get; set; }
        public string FacilityEmail { get; set; }
        public List<ServiceItemDetails> ServiceItem { get; set; }
        public List<TotalPrice> TotalPrice { get; set; }
        public List<UserDetail> UserDetails { get; set; }
        public List<QbidCurrentStatusDetails> QbidCurrentStatus { get; set; }
        public List<QbidStatu> QbidStatus { get; set; }
    }
    public class Attachments
    {
        public string attachments { get; set; }
    }
}
