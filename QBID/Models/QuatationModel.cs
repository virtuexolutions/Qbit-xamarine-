using QBid.Models.APIResponse;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
    public class QuatationModel
    {
        public string Name { get; set; }
        public string FacilityName { get; set; }
        public string QbidDateTime { get; set; }
        public string SofaCleaner { get; set; }
        public string ViewQuotation { get; set; }
        public string ClickHereToHireNegociator { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
        public string ProfileImage { get; set; }
    }

    public class QuatationDetailsModel : BindableObject
    {
        private string qBidStatusNameColor;
        public bool IsCancleQbid { get; set; }
        public string QBidStatusNameColor
        {
            get { return qBidStatusNameColor; }
            set { qBidStatusNameColor = value; OnPropertyChanged(nameof(QBidStatusNameColor)); }
        }
        private string qBidStatusNameButtonColor;

        public string QBidStatusNameButtonColor
        {
            get { return qBidStatusNameButtonColor; }
            set { qBidStatusNameButtonColor = value; OnPropertyChanged(nameof(QBidStatusNameButtonColor)); }
        }
     
        private bool isQbidDateTimeVisible;
        /// <summary>
        ///  Property for User qbidDateTime visible
        /// </summary>

        public bool IsQbidDateTimeVisible
        {
            get { return isQbidDateTimeVisible; }
            set { isQbidDateTimeVisible = value; OnPropertyChanged(nameof(IsQbidDateTimeVisible)); }
        }
        private string firstColFrameColor;
        /// <summary>
        /// Property for First Column Frame Bacground Color
        /// </summary>

        public string FirstColFrameColor
        {
            get { return firstColFrameColor; }
            set { firstColFrameColor = value; OnPropertyChanged(nameof(FirstColFrameColor)); }
        }

        private bool isAcceptButtonShow;
        /// <summary>
        /// Property for Accept Button Show
        /// </summary>

        public bool IsAcceptButtonShow
        {
            get { return isAcceptButtonShow; }
            set { isAcceptButtonShow = value; OnPropertyChanged(nameof(IsAcceptButtonShow)); }
        }

        private string hireAndChangeNegotiatior;
        /// <summary>
        ///  Property for hireAndChangeNegotiatior
        /// </summary>

        public string HireAndChangeNegotiatior
        {
            get { return hireAndChangeNegotiatior; }
            set { hireAndChangeNegotiatior = value; OnPropertyChanged(nameof(HireAndChangeNegotiatior)); }
        }


        private string secoundColFrameColor;
        /// <summary>
        /// Property for secoundColFrameColor
        /// </summary>

        public string SecoundColFrameColor
        {
            get { return secoundColFrameColor; }
            set { secoundColFrameColor = value; OnPropertyChanged(nameof(SecoundColFrameColor)); }
        }

        private string thirdColFrameColor;
        /// <summary>
        /// Property for thirdColFrameColor
        /// </summary>

        public string ThirdColFrameColor
        {
            get { return thirdColFrameColor; }
            set { thirdColFrameColor = value; OnPropertyChanged(nameof(ThirdColFrameColor)); }
        }

        private string fourthColFrameColor;
        /// <summary>
        /// Property for fourthColFrameColor
        /// </summary>

        public string FourthColFrameColor
        {
            get { return fourthColFrameColor; }
            set { fourthColFrameColor = value; OnPropertyChanged(nameof(FourthColFrameColor)); }
        }

        private string fifthColFrameColor;
        /// <summary>
        /// Property for fifthColFrameColor
        /// </summary>

        public string FifthColFrameColor
        {
            get { return fifthColFrameColor; }
            set { fifthColFrameColor = value; OnPropertyChanged(nameof(FifthColFrameColor)); }
        }

        private string sixthColFrameColor;
        /// <summary>
        /// Property for sixthColFrameColor
        /// </summary>

        public string SixthColFrameColor
        {
            get { return sixthColFrameColor; }
            set { sixthColFrameColor = value; OnPropertyChanged(nameof(SixthColFrameColor)); }
        }

        private string seventhColFrameColor;
        /// <summary>
        /// Property for seventhColFrameColor
        /// </summary>

        public string SeventhColFrameColor
        {
            get { return seventhColFrameColor; }
            set { seventhColFrameColor = value; OnPropertyChanged(nameof(SeventhColFrameColor)); }
        }

        private int firstColSpan;
        /// <summary>
        /// Property for FirstColSpan
        /// </summary>

        public int FirstColSpan
        {
            get { return firstColSpan; }
            set { firstColSpan = value; OnPropertyChanged(nameof(FirstColSpan)); }
        }

        private int secoundColSpan;
        /// <summary>
        /// Property for secoundColSpan
        /// </summary>

        public int SecoundColSpan
        {
            get { return secoundColSpan; }
            set { secoundColSpan = value; OnPropertyChanged(nameof(SecoundColSpan)); }
        }

        private int secoundColStart;
        /// <summary>
        /// Property for secoundColSpan
        /// </summary>

        public int SecoundColStart
        {
            get { return secoundColStart; }
            set { secoundColStart = value; OnPropertyChanged(nameof(SecoundColStart)); }
        }

        private bool isFirstStepCompleted;
        /// <summary>
        /// Property for isFirstStepCompleted
        /// </summary>

        public bool IsFirstStepCompleted
        {
            get { return isFirstStepCompleted; }
            set { isFirstStepCompleted = value; OnPropertyChanged(nameof(IsFirstStepCompleted)); }
        }

        private bool isSecoundStepCompleted;
        /// <summary>
        /// Property for isSecoundStepCompleted
        /// </summary>

        public bool IsSecoundStepCompleted
        {
            get { return isSecoundStepCompleted; }
            set { isSecoundStepCompleted = value; OnPropertyChanged(nameof(IsSecoundStepCompleted)); }
        }

        private bool isThirdStepCompleted;
        /// <summary>
        /// Property for isThirdStepCompleted
        /// </summary>

        public bool IsThirdStepCompleted
        {
            get { return isThirdStepCompleted; }
            set { isThirdStepCompleted = value; OnPropertyChanged(nameof(IsThirdStepCompleted)); }
        }

        private bool isFourthStepCompleted;
        /// <summary>
        /// Property for isFourthStepCompleted
        /// </summary>

        public bool IsFourthStepCompleted
        {
            get { return isFourthStepCompleted; }
            set { isFourthStepCompleted = value; OnPropertyChanged(nameof(IsFourthStepCompleted)); }
        }

        private bool isFifthStepCompleted;
        /// <summary>
        /// Property for isFifthStepCompleted
        /// </summary>

        public bool IsFifthStepCompleted
        {
            get { return isFifthStepCompleted; }
            set { isFifthStepCompleted = value; OnPropertyChanged(nameof(IsFifthStepCompleted)); }
        }

        private bool isSixthStepCompleted;
        
        /// <summary>
        /// Property for isSixthStepCompleted
        /// </summary>

        public bool IsSixthStepCompleted
        {
            get { return isSixthStepCompleted; }
            set { isSixthStepCompleted = value; OnPropertyChanged(nameof(IsSixthStepCompleted)); }
        }

        private bool isSeventhStepCompleted;
        /// <summary>
        /// Property for isSeventhStepCompleted
        /// </summary>

        public bool IsSeventhStepCompleted
        {
            get { return isSeventhStepCompleted; }
            set { isSeventhStepCompleted = value; OnPropertyChanged(nameof(IsSeventhStepCompleted)); }
        }

        private bool isSeventhStepDeclined;
        /// <summary>
        /// Property for IsSeventhStepDeclined
        /// </summary>

        public bool IsSeventhStepDeclined
        {
            get { return isSeventhStepDeclined; }
            set { isSeventhStepDeclined = value; OnPropertyChanged(nameof(IsSeventhStepDeclined)); }
        }



        private bool isFirstStepInProcessing;
        /// <summary>
        /// Property for isFirstStepInProcessing
        /// </summary>

        public bool IsFirstStepInProcessing
        {
            get { return isFirstStepInProcessing; }
            set { isFirstStepInProcessing = value; OnPropertyChanged(nameof(IsFirstStepInProcessing)); }
        }

        private bool isSecoundStepInProcessing;
        /// <summary>
        /// Property for isSecoundStepInProcessing
        /// </summary>

        public bool IsSecoundStepInProcessing
        {
            get { return isSecoundStepInProcessing; }
            set { isSecoundStepInProcessing = value; OnPropertyChanged(nameof(IsSecoundStepInProcessing)); }
        }

        private bool isThirdStepInProcessing;
        /// <summary>
        /// Property for isThirdStepInProcessing
        /// </summary>

        public bool IsThirdStepInProcessing
        {
            get { return isThirdStepInProcessing; }
            set { isThirdStepInProcessing = value; OnPropertyChanged(nameof(IsThirdStepInProcessing)); }
        }

        private bool isFourthStepInProcessing;
        /// <summary>
        /// Property for isFourthStepInProcessing
        /// </summary>

        public bool IsFourthStepInProcessing
        {
            get { return isFourthStepInProcessing; }
            set { isFourthStepInProcessing = value; OnPropertyChanged(nameof(IsFourthStepInProcessing)); }
        }

        private bool isFifthStepInProcessing;
        /// <summary>
        /// Property for isFifthStepInProcessing
        /// </summary>

        public bool IsFifthStepInProcessing
        {
            get { return isFifthStepInProcessing; }
            set { isFifthStepInProcessing = value; OnPropertyChanged(nameof(IsFifthStepInProcessing)); }
        }

        private bool isSixthStepInProcessing;
        /// <summary>
        /// Property for isSixthStepInProcessing
        /// </summary>

        public bool IsSixthStepInProcessing
        {
            get { return isSixthStepInProcessing; }
            set { isSixthStepInProcessing = value; OnPropertyChanged(nameof(IsSixthStepInProcessing)); }
        }

        private bool isSeventhStepInProcessing;
        /// <summary>
        /// Property for isSeventhStepInProcessing
        /// </summary>

        public bool IsSeventhStepInProcessing
        {
            get { return isSeventhStepInProcessing; }
            set { isSeventhStepInProcessing = value; OnPropertyChanged(nameof(IsSeventhStepInProcessing)); }
        }



        private bool isFirstStepPending;
        /// <summary>
        /// Property for isFirstStepPending
        /// </summary>

        public bool IsFirstStepPending
        {
            get { return isFirstStepPending; }
            set { isFirstStepPending = value; OnPropertyChanged(nameof(IsFirstStepPending)); }
        }

        private bool isSecoundStepPending;
        /// <summary>
        /// Property for isSecoundStepPending
        /// </summary>

        public bool IsSecoundStepPending
        {
            get { return isSecoundStepPending; }
            set { isSecoundStepPending = value; OnPropertyChanged(nameof(IsSecoundStepPending)); }
        }

        private bool isThirdStepPending;
        /// <summary>
        /// Property for isThirdStepPending
        /// </summary>

        public bool IsThirdStepPending
        {
            get { return isThirdStepPending; }
            set { isThirdStepPending = value; OnPropertyChanged(nameof(IsThirdStepPending)); }
        }
        private bool isFourthStepPending;
        /// <summary>
        /// Property for isFourthStepPending
        /// </summary>

        public bool IsFourthStepPending
        {
            get { return isFourthStepPending; }
            set { isFourthStepPending = value; OnPropertyChanged(nameof(IsFourthStepPending)); }
        }
        private bool isFifthStepPending;
        /// <summary>
        /// Property for isFifthStepPending
        /// </summary>

        public bool IsFifthStepPending
        {
            get { return isFifthStepPending; }
            set { isFifthStepPending = value; OnPropertyChanged(nameof(IsFifthStepPending)); }
        }
        private bool isSixthStepPending;
        /// <summary>
        /// Property for isSixthStepPending
        /// </summary>

        public bool IsSixthStepPending
        {
            get { return isSixthStepPending; }
            set { isSixthStepPending = value; OnPropertyChanged(nameof(IsSixthStepPending)); }
        }
        private bool isSeventhStepPending;
        /// <summary>
        /// Property for isSeventhStepPending
        /// </summary>

        public bool IsSeventhStepPending
        {
            get { return isSeventhStepPending; }
            set { isSeventhStepPending = value; OnPropertyChanged(nameof(IsSeventhStepPending)); }
        }

        private string qBidStatusName;
        /// <summary>
        ///  Property for User qBidStatusName
        /// </summary>

        public string QBidStatusName
        {
            get { return qBidStatusName; }
            set { qBidStatusName = value; OnPropertyChanged(nameof(QBidStatusName)); }
        }

        private string qBidStatusBackGroundColor;
        /// <summary>
        ///  Property for User qBidStatusName
        /// </summary>

        public string QBidStatusBackGroundColor
        {
            get { return qBidStatusBackGroundColor; }
            set { qBidStatusBackGroundColor = value; OnPropertyChanged(nameof(QBidStatusBackGroundColor)); }
        }

        private string qBidStatusTextColor;
        /// <summary>
        ///  Property for User qBidStatusName
        /// </summary>

        public string QBidStatusTextColor
        {
            get { return qBidStatusTextColor; }
            set { qBidStatusTextColor = value; OnPropertyChanged(nameof(QBidStatusTextColor)); }
        }


        private string facilityEmail;
        /// <summary>
        ///  Property for User facilityEmail
        /// </summary>

        public string FacilityEmail
        {
            get { return facilityEmail; }
            set { facilityEmail = value; OnPropertyChanged(nameof(FacilityEmail)); }
        }

        private string serviceName;
        /// <summary>
        /// Property for service Name
        /// </summary>
        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; OnPropertyChanged(nameof(ServiceName)); }
        }

        private bool isVisibleButtonHireNegotiator;
        /// <summary>
        ///  Property for User isVisibleHireNegotiator
        /// </summary>

        public bool IsVisibleButtonHireNegotiator
        {
            get { return isVisibleButtonHireNegotiator; }
            set { isVisibleButtonHireNegotiator = value; OnPropertyChanged(nameof(IsVisibleButtonHireNegotiator)); }
        }

        private string buttonHireNegotiatorText;
        /// <summary>
        ///  Property for User buttonHireNegotiatorText
        /// </summary>

        public string ButtonHireNegotiatorText
        {
            get { return buttonHireNegotiatorText; }
            set { buttonHireNegotiatorText = value; OnPropertyChanged(nameof(ButtonHireNegotiatorText)); }
        }

        private bool isQBidDetailsAvailable;
        /// <summary>
        ///  Property for User isQBidDetailsAvailable
        /// </summary>

        public bool IsQBidDetailsAvailable
        {
            get { return isQBidDetailsAvailable; }
            set { isQBidDetailsAvailable = value; OnPropertyChanged(nameof(IsQBidDetailsAvailable)); }
        }
        private bool isVisibleHireNegotiator;
        /// <summary>
        ///  Property for User isVisibleHireNegotiator
        /// </summary>

        public bool IsVisibleHireNegotiator
        {
            get { return isVisibleHireNegotiator; }
            set { isVisibleHireNegotiator = value; OnPropertyChanged(nameof(IsVisibleHireNegotiator)); }
        }


        private string facilityContactNo;
        /// <summary>
        ///  Property for User facilityContactNo
        /// </summary>

        public string FacilityContactNo
        {
            get { return facilityContactNo; }
            set { facilityContactNo = value; OnPropertyChanged(nameof(FacilityContactNo)); }
        }

        private int id;
        /// <summary>
        ///  Property for User id
        /// </summary>

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        private string facilityId;
        /// <summary>
        ///  Property for facilityId
        /// </summary>

        public string FacilityId
        {
            get { return facilityId; }
            set { facilityId = value; OnPropertyChanged(nameof(FacilityId)); }
        }

        private List<string> attachmentUrl;
        /// <summary>
        ///  Property for Attachment Url
        /// </summary>

        //public string AttachmentUrl
        //{
        //    get { return attachmentUrl; }
        //    set { attachmentUrl = value; OnPropertyChanged(nameof(AttachmentUrl)); }
        //}

        public List<string> AttachmentUrl
        {
            get { return attachmentUrl; }
            set { attachmentUrl = value; OnPropertyChanged(nameof(AttachmentUrl)); }
        }

        private string name;
        /// <summary>
        ///  Property for User name
        /// </summary>

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        private bool isPriceAvailableVisible;
        /// <summary>
        ///  Property for User isPriceAvailableVisible
        /// </summary>

        public bool IsPriceAvailableVisible
        {
            get { return isPriceAvailableVisible; }
            set { isPriceAvailableVisible = value; OnPropertyChanged(nameof(IsPriceAvailableVisible)); }
        }

        private bool isVendorPriceAvailableVisible;
        /// <summary>
        ///  Property for  isVendor Price Available Visible
        /// </summary>

        public bool IsVendorPriceAvailableVisible
        {
            get { return isVendorPriceAvailableVisible; }
            set { isVendorPriceAvailableVisible = value; OnPropertyChanged(nameof(IsVendorPriceAvailableVisible)); }
        }
        private bool isFacilityEmailVisible;
        /// <summary>
        ///  Property for User isFacilityContactVisible
        /// </summary>

        public bool IsFacilityEmailVisible
        {
            get { return isFacilityEmailVisible; }
            set { isFacilityEmailVisible = value; OnPropertyChanged(nameof(IsFacilityEmailVisible)); }
        }

        private bool isDownLoadIconVisible;
        /// <summary>
        ///  Property for User DownLoad Icon Visible
        /// </summary>

        public bool IsDownLoadIconVisible
        {
            get { return isDownLoadIconVisible; }
            set { isDownLoadIconVisible = value; OnPropertyChanged(nameof(IsDownLoadIconVisible)); }
        }

        private bool isFacilityContactVisible;
        /// <summary>
        ///  Property for User isFacilityContactVisible
        /// </summary>

        public bool IsFacilityContactVisible
        {
            get { return isFacilityContactVisible; }
            set { isFacilityContactVisible = value; OnPropertyChanged(nameof(IsFacilityContactVisible)); }
        }
        private string qBidStatusDateTime;
        /// <summary>
        ///  Property for User isVisibleQBidStatus
        /// </summary>

        public string QBidStatusDateTime
        {
            get { return qBidStatusDateTime; }
            set { qBidStatusDateTime = value; OnPropertyChanged(nameof(QBidStatusDateTime)); }
        }

        private bool isVisibleQBidStatus;
        /// <summary>
        ///  Property for User isVisibleQBidStatus
        /// </summary>

        public bool IsVisibleQBidStatus
        {
            get { return isVisibleQBidStatus; }
            set { isVisibleQBidStatus = value; OnPropertyChanged(nameof(IsVisibleQBidStatus)); }
        }

        private string facilityName;
        /// <summary>
        ///  Property for User facilityName
        /// </summary>

        public string FacilityName
        {
            get { return facilityName; }
            set { facilityName = value; OnPropertyChanged(nameof(FacilityName)); }
        }

        private string advisorName;
        /// <summary>
        ///  Property for User advisorName
        /// </summary>

        public string AdvisorName
        {
            get { return advisorName; }
            set { advisorName = value; OnPropertyChanged(nameof(AdvisorName)); }
        }

        private string negotiatorProfileImage;
        /// <summary>
        ///  Property for User negotiatorProfileImage
        /// </summary>

        public string NegotiatorProfileImage
        {
            get { return negotiatorProfileImage; }
            set { negotiatorProfileImage = value; OnPropertyChanged(nameof(NegotiatorProfileImage)); }
        }

        private string qbidDateTime;
        /// <summary>
        ///  Property for User qbidDateTime
        /// </summary>

        public string QbidDateTime
        {
            get { return qbidDateTime; }
            set { qbidDateTime = value; OnPropertyChanged(nameof(QbidDateTime)); }
        }
        private string sofaCleaner;
        /// <summary>
        ///  Property for User sofaCleaner
        /// </summary>

        public string SofaCleaner
        {
            get { return sofaCleaner; }
            set { sofaCleaner = value; OnPropertyChanged(nameof(SofaCleaner)); }
        }
        private string viewQuotation;
        /// <summary>
        ///  Property for User viewQuotation
        /// </summary>
        public string ViewQuotation
        {
            get { return viewQuotation; }
            set { viewQuotation = value; OnPropertyChanged(nameof(ViewQuotation)); }
        }
        private string clickHereToHireNegociator;
        /// <summary>
        ///  Property for User clickHereToHireNegociator
        /// </summary>

        public string ClickHereToHireNegociator
        {
            get { return clickHereToHireNegociator; }
            set { clickHereToHireNegociator = value; OnPropertyChanged(nameof(ClickHereToHireNegociator)); }
        }
        private string status;
        /// <summary>
        ///  Property for User status
        /// </summary>

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }
        private string total;
        /// <summary>
        ///  Property for User total
        /// </summary>
        public string Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged(nameof(Total)); }
        }

        private string vendorTotal;
        /// <summary>
        ///  Property for Vendor total price
        /// </summary>
        public string VendorTotal
        {
            get { return vendorTotal; }
            set { vendorTotal = value; OnPropertyChanged(nameof(VendorTotal)); }
        }

        private string profileImage;
        /// <summary>
        ///  Property for User profileImage
        /// </summary>
        public string ProfileImage
        {
            get { return profileImage; }
            set { profileImage = value; OnPropertyChanged(nameof(ProfileImage)); }
        }

        private bool isNegotiatordetails;
        /// <summary>
        /// Property for user isNegotiatordetails
        /// </summary>
        public bool IsNegotiatordetails
        {
            get { return isNegotiatordetails; }
            set
            {
                isNegotiatordetails = value; OnPropertyChanged(nameof(IsNegotiatordetails));
            }
        }

        private bool isFacilitydetails;
        /// <summary>
        /// Property for user isFacilitydetails
        /// </summary>
        public bool IsFacilitydetails
        {
            get { return isFacilitydetails; }
            set
            {
                isFacilitydetails = value; OnPropertyChanged(nameof(IsFacilitydetails));
            }
        }

        private string negotiatorName;
        /// <summary>
        /// Property for user negotiatorName
        /// </summary>
        public string NegotiatorName
        {
            get { return negotiatorName; }
            set
            {
                negotiatorName = value; OnPropertyChanged(nameof(NegotiatorName));
            }
        }
        private string negotiatorEmail;
        /// <summary>
        /// Property for user negotiatorEmail
        /// </summary>
        public string NegotiatorEmail
        {
            get { return negotiatorEmail; }
            set
            {
                negotiatorEmail = value; OnPropertyChanged(nameof(NegotiatorEmail));
            }
        }
        private string negotiatorContactMobile;
        /// <summary>
        /// Property for user negotiator Contact mobile
        /// </summary>
        public string NegotiatorContactMobile
        {
            get { return negotiatorContactMobile; }
            set
            {
                negotiatorContactMobile = value; OnPropertyChanged(nameof(NegotiatorContactMobile));
            }
        }

        private string negotiatorContactText;
        /// <summary>
        /// Property for user negotiator Contact text
        /// </summary>
        public string NegotiatorContactText
        {
            get { return negotiatorContactText; }
            set
            {
                negotiatorContactText = value; OnPropertyChanged(nameof(NegotiatorContactText));
            }
        }

        private string negotiatorRating;
        /// <summary>
        /// Property for user negotiatorRating
        /// </summary>
        public string NegotiatorRating
        {
            get { return negotiatorRating; }
            set
            {
                negotiatorRating = value; OnPropertyChanged(nameof(NegotiatorRating));
            }
        }

        private string gridFirstRowHeight;
        /// <summary>
        /// property for GridFirstRowHeight
        /// </summary>

        public string GridFirstRowHeight
        {
            get { return gridFirstRowHeight; }
            set { gridFirstRowHeight = value; OnPropertyChanged(nameof(GridFirstRowHeight)); }
        }
        private string gridLastRowHeight;
        /// <summary>
        /// property for GridFirstRowHeight
        /// </summary>

        public string GridLastRowHeight
        {
            get { return gridLastRowHeight; }
            set { gridLastRowHeight = value; OnPropertyChanged(nameof(GridLastRowHeight)); }
        }

        private bool isReSendQbidLink;
        /// <summary>
        ///  Property for User isSendNewQbidLink
        /// </summary>
        public bool IsReSendQbidLink
        {
            get { return isReSendQbidLink; }
            set { isReSendQbidLink = value; OnPropertyChanged(nameof(IsReSendQbidLink)); }
        }

        private List<QbidCurrentStatus> lstQbidCurrentStatus;
        /// <summary>
        /// property for LstQbidCurrentStatus
        /// </summary>
        public List<QbidCurrentStatus> LstQbidCurrentStatus
        {
            get { return lstQbidCurrentStatus; }
            set { lstQbidCurrentStatus = value; OnPropertyChanged(nameof(LstQbidCurrentStatus)); }
        }

        private bool isAttachmentVisible;
        /// <summary>
        ///  Property for User isQBidDetailsAvailable
        /// </summary>

        public bool IsAttachmentVisible
        {
            get { return isAttachmentVisible; }
            set { isAttachmentVisible = value; OnPropertyChanged(nameof(IsAttachmentVisible)); }
        }


        private Command commandOnNegotiatorCalling;
        /// <summary>
        ///  Property for User commandOnNegotiatorCalling
        /// </summary>

        public Command CommandOnNegotiatorCalling
        {
            get { return commandOnNegotiatorCalling; }
            set { commandOnNegotiatorCalling = value; OnPropertyChanged(nameof(CommandOnNegotiatorCalling)); }
        }
        public Command DownloadCommand { get; set; }

        private Command commandOnCalling;
        /// <summary>
        ///  Property for User commandOnCalling
        /// </summary>

        public Command CommandOnCalling
        {
            get { return commandOnCalling; }
            set { commandOnCalling = value; OnPropertyChanged(nameof(CommandOnCalling)); }
        }

        private Command commandOnEmailtoVendor;
        /// <summary>
        ///  Property for Email to vendor email
        /// </summary>

        public Command CommandOnEmailtoVendor
        {
            get { return commandOnEmailtoVendor; }
            set { commandOnEmailtoVendor = value; OnPropertyChanged(nameof(CommandOnEmailtoVendor)); }
        }

        private Command commandOnEmailtoNegotiator;
        /// <summary>
        ///  Property for Email to negotiator email
        /// </summary>

        public Command CommandOnEmailtoNegotiator
        {
            get { return commandOnEmailtoNegotiator; }
            set { commandOnEmailtoNegotiator = value; OnPropertyChanged(nameof(CommandOnEmailtoNegotiator)); }
        }

        private Command commandOnEmailtoMember;
        /// <summary>
        ///  Property for Email to Member email
        /// </summary>

        public Command CommandOnEmailtoMember
        {
            get { return commandOnEmailtoMember; }
            set { commandOnEmailtoMember = value; OnPropertyChanged(nameof(CommandOnEmailtoMember)); }
        }
        

        private Command commandOnViewQBidStatus;
        /// <summary>
        ///  Command for User commandOnViewQBidStatus
        /// </summary>

        public Command CommandOnViewQBidStatus
        {
            get { return commandOnViewQBidStatus; }
            set { commandOnViewQBidStatus = value; OnPropertyChanged(nameof(CommandOnViewQBidStatus)); }
        }

        private Command commandOnViewQBid;
        /// <summary>
        ///  Command for User commandOnViewQBid
        /// </summary>

        public Command CommandOnViewQBid
        {
            get { return commandOnViewQBid; }
            set { commandOnViewQBid = value; OnPropertyChanged(nameof(CommandOnViewQBid)); }
        }
        private Command commandOnNegotiatiorDetails;
        /// <summary>
        ///  Command for User commandOnNegotiatiorDetails
        /// </summary>
        public Command CommandOnNegotiatiorDetails
        {
            get { return commandOnNegotiatiorDetails; }
            set { commandOnNegotiatiorDetails = value; OnPropertyChanged(nameof(CommandOnNegotiatiorDetails)); }
        }

        private Command commandOnHoldQbid;
        /// <summary>
        ///  Command for User CommandOnHoldQbid
        /// </summary>
        public Command CommandOnHoldQbid
        {
            get { return commandOnHoldQbid; }
            set { commandOnHoldQbid = value; OnPropertyChanged(nameof(CommandOnHoldQbid)); }
        }

        private Command acceptQuotationByNegotiator;
        /// <summary>
        ///  Command for User accept quotation by negotiator
        /// </summary>
        public Command AcceptQuotationByNegotiator
        {
            get { return acceptQuotationByNegotiator; }
            set { acceptQuotationByNegotiator = value; OnPropertyChanged(nameof(AcceptQuotationByNegotiator)); }
        }

        private Command declineQuotationByNegotiator;
        /// <summary>
        ///  Command for User decline quotation by negotiator
        /// </summary>
        public Command DeclineQuotationByNegotiator
        {
            get { return declineQuotationByNegotiator; }
            set { declineQuotationByNegotiator = value; OnPropertyChanged(nameof(DeclineQuotationByNegotiator)); }
        }
        public Command RatingOnCommand { get; set; }
        public Command CommandCancleQbid { get; set; }      
        public Command CommandReSendQbidMail { get; set; }
        
    }
}
