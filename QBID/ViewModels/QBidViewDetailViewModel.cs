using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using QBid.APIServices;
using System.Threading.Tasks;
using QBid.Helpers;
using System.Collections.ObjectModel;
using QBid.Models.APIRequest;
using System.Linq;
using QBid.APILog;
using QBid.Views;
using QBid.Models;
using Xamarin.Essentials;
using System.Net;
using QBid.DependencyServices;
using QBid.Models.APIResponse;
using QBid.QBidResource;

namespace QBid.ViewModels
{
    public class QBidViewDetailViewModel : BindableObject
    { 
        public string contactNoMobile = string.Empty;
        public string contactNoText = string.Empty;

        APIService apiServices = new APIService();
        private ObservableCollection<ServiceItemHeaderModel> stackLayOutList;

        public ObservableCollection<ServiceItemHeaderModel> StackLayOutList
        {
            get { return stackLayOutList; }
            set { stackLayOutList = value; OnPropertyChanged(nameof(StackLayOutList)); }
        }

        #region Constructor

       
        public QBidViewDetailViewModel()
        {
            QBidHelper.ServiceItemIdList = new List<string>();
          
            LableText = ResourceValues.TitleVendorLabel;
            LablText = ResourceValues.TitleQbidNegoLabel;
            lblText = ResourceValues.TitleServiceItemLabel;
            lblAccountText = ResourceValues.TitlePaymentabel;

            GetQBidDetails();
            GetQBidStatus();

            QBidHelper.total = 0.00M;
            QBidHelper.negotiatedtotal = 0.00M;
            QBidHelper.totalSaving = 0.00M;

            TotalPrice = ConstantValues.CurencySymbal + ConstantValues.DefaultValue; 
            TotalNegotiatedPrice = ConstantValues.CurencySymbal + ConstantValues.DefaultValue;  
            TotalSaving = ConstantValues.CurencySymbal + ConstantValues.DefaultValue;

            MessagingCenter.Subscribe<SubmitServiceItem>(this, "TotalAmount", (value) =>
            {
                TotalPrice = value.TotalPrice;
                TotalNegotiatedPrice = value.NegotiatedPrice;
                TotalSaving = value.TotalSaving;

                ServiceItemId = value.ServiceitemId;
            });

             
        }
        #endregion

        #region Properties

        private string specialNote;
        /// <summary>
        /// Property for SpecialNote
        /// </summary>
        public string SpecialNote
        {
            get { return specialNote; }
            set { specialNote = value; OnPropertyChanged(nameof(SpecialNote)); }
        }

       

        private string benefits;
        /// <summary>
        /// Property for Benefits 
        /// </summary>
        public string Benefits
        {
            get { return benefits; }
            set { benefits = value; OnPropertyChanged(nameof(Benefits)); }
        }

       
        private bool yes;
        /// <summary>
        /// Property for yes option button
        /// </summary>
        public bool Yes
        {
            get { return yes; }
            set { yes = value; OnPropertyChanged(nameof(Yes)); }
        }

        private bool no;
        /// <summary>
        /// Property for No option button
        /// </summary>
        public bool No
        {
            get { return no; }
            set { no = value; OnPropertyChanged(nameof(No)); }
        }

        private string hireNegotiatorBtnText;
        /// <summary>
        /// Property for hireNegotiatorBtnText
        /// </summary>
        public string HireNegotiatorBtnText
        {
            get { return hireNegotiatorBtnText; }
            set { hireNegotiatorBtnText = value; OnPropertyChanged(nameof(HireNegotiatorBtnText)); }
        }

        private bool isShowHireNegotiator;
        /// <summary>
        /// Property for IsHireNegotiator
        /// </summary>
        public bool IsShowHireNegotiator
        {
            get { return isShowHireNegotiator; }
            set { isShowHireNegotiator = value; OnPropertyChanged(nameof(IsShowHireNegotiator)); }
        }

        private bool isPopupForHireNegoDisc;
        /// <summary>
        /// property for isPopupForHireNegoDisc
        /// </summary>
        public bool IsPopupForHireNegoDisc
        {
            get { return isPopupForHireNegoDisc; }
            set { isPopupForHireNegoDisc = value; OnPropertyChanged(nameof(IsPopupForHireNegoDisc)); }
        }

        private bool isPopupForNoteandBenefits;
        /// <summary>
        /// property for Popup For Note and Benefits
        /// </summary>
        public bool IsPopupForNoteandBenefits
        {
            get { return isPopupForNoteandBenefits; }
            set { isPopupForNoteandBenefits = value; OnPropertyChanged(nameof(IsPopupForNoteandBenefits)); }
        }


        private bool isPopupForChangeNego;
        /// <summary>
        /// property for isPopupForChangeNego
        /// </summary>
        public bool IsPopupForChangeNego
        {
            get { return isPopupForChangeNego; }
            set { isPopupForChangeNego = value; OnPropertyChanged(nameof(IsPopupForChangeNego)); }
        }

        private bool isPopupForBlockNego;
        /// <summary>
        /// property for IsPopupForBlockNego
        /// </summary>
        public bool IsPopupForBlockNego
        {
            get { return isPopupForBlockNego; }
            set { isPopupForBlockNego = value; OnPropertyChanged(nameof(IsPopupForBlockNego)); }
        }

        private string hireORChangeNegotiatorText;
        /// <summary>
        /// property for Hire or change negotiator
        /// </summary>
        public string HireORChangeNegotiatorText
        {
            get { return hireORChangeNegotiatorText; }
            set { hireORChangeNegotiatorText = value; OnPropertyChanged(nameof(HireORChangeNegotiatorText)); }
        }

        private List<string> serviceItemId;

        public List<string> ServiceItemId
        {
            get { return serviceItemId; }
            set { serviceItemId = value; OnPropertyChanged(nameof(ServiceItemId)); }
        }


        private string lableText;
        public string LableText
        {
            get { return lableText; }
            set { lableText = value; OnPropertyChanged(nameof(LableText)); }
        }
        private string lablText;
        /// <summary>
        /// Property for lablText
        /// </summary>

        public string LablText
        {
            get { return lablText; }
            set { lablText = value; OnPropertyChanged(nameof(LablText)); }
        }

        private string lblText;
        /// <summary>
        /// Property for lablText
        /// </summary>

        public string LblText
        {
            get { return lblText; }
            set { lblText = value; OnPropertyChanged(nameof(lblText)); }
        }
        private string lblAccountText;
        /// <summary>
        /// Property for lablAccountText
        /// </summary>

        public string LblAccountText
        {
            get { return lblAccountText; }
            set { lblAccountText = value; OnPropertyChanged(nameof(LblAccountText)); }
        }

        private bool isLoader;
        /// <summary>
        /// Property for isLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private bool isNegotiatorAvailble;
        /// <summary>
        /// Property for isNegotiatorAvailble
        /// </summary>
        public bool IsNegotiatorAvailble
        {
            get { return isNegotiatorAvailble; }
            set { isNegotiatorAvailble = value; OnPropertyChanged(nameof(IsNegotiatorAvailble)); }
        }

        private string facilityName;
        /// <summary>
        /// Property for facilityName
        /// </summary>
        public string FacilityName
        {
            get { return facilityName; }
            set { facilityName = value; OnPropertyChanged(nameof(FacilityName)); }
        }

        private string facilityRepresentative;
        /// <summary>
        /// Property for FacilityRepresentative
        /// </summary>
        public string FacilityRepresentative
        {
            get { return facilityRepresentative; }
            set { facilityRepresentative = value; OnPropertyChanged(nameof(FacilityRepresentative)); }
        }


        private string facilityEmail;
        /// <summary>
        /// Property for facilityEmail
        /// </summary>
        public string FacilityEmail
        {
            get { return facilityEmail; }
            set { facilityEmail = value; OnPropertyChanged(nameof(FacilityEmail)); }
        }
        private bool isVisibleFacilityName;
        /// <summary>
        /// Property for isVisibl eFacility Name
        /// </summary>
        public bool IsVisibleFacilityName
        {
            get { return isVisibleFacilityName; }
            set { isVisibleFacilityName = value; OnPropertyChanged(nameof(IsVisibleFacilityName)); }
        }
        private bool isVisibleFacilityEmail;
        /// <summary>
        /// Property for isVisibl eFacility Email
        /// </summary>
        public bool IsVisibleFacilityEmail
        {
            get { return isVisibleFacilityEmail; }
            set { isVisibleFacilityEmail = value; OnPropertyChanged(nameof(IsVisibleFacilityEmail)); }
        }
        private bool isVisibleFacilityAddress;
        /// <summary>
        /// Property for isVisibl eFacility Address
        /// </summary>
        public bool IsVisibleFacilityAddress
        {
            get { return isVisibleFacilityAddress; }
            set { isVisibleFacilityAddress = value; OnPropertyChanged(nameof(IsVisibleFacilityAddress)); }
        }
        private bool isVisibleFacilityContact;
        /// <summary>
        /// Property for isVisibl eFacility Contact
        /// </summary>
        public bool IsVisibleFacilityContact
        {
            get { return isVisibleFacilityContact; }
            set { isVisibleFacilityContact = value; OnPropertyChanged(nameof(IsVisibleFacilityContact)); }
        }
        private string facilityContact;
        /// <summary>
        /// Property for facilityContact
        /// </summary>
        public string FacilityContact
        {
            get { return facilityContact; }
            set { facilityContact = value; OnPropertyChanged(nameof(FacilityContact)); }
        }

        private string facilityAddress;
        /// <summary>
        /// Property for facilityAddress
        /// </summary>
        public string FacilityAddress
        {
            get { return facilityAddress; }
            set { facilityAddress = value; OnPropertyChanged(nameof(FacilityAddress)); }
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

        private string negotiatorName;
        /// <summary>
        /// Property for negotiatorName
        /// </summary>
        public string NegotiatorName
        {
            get { return negotiatorName; }
            set { negotiatorName = value; OnPropertyChanged(nameof(NegotiatorName)); }
        }

        private string negotiatorEmail;
        /// <summary>
        /// Property for negotiatorEmail
        /// </summary>
        public string NegotiatorEmail
        {
            get { return negotiatorEmail; }
            set { negotiatorEmail = value; OnPropertyChanged(nameof(NegotiatorEmail)); }
        }

        private string negotiatorContactMobile;
        /// <summary>
        /// Property for negotiator Contact for mobile
        /// </summary>
        public string NegotiatorContactMobile
        {
            get { return negotiatorContactMobile; }
            set { negotiatorContactMobile = value; OnPropertyChanged(nameof(NegotiatorContactMobile)); }
        }

        private string negotiatorContactText;
        /// <summary>
        /// Property for negotiatorContact for Text
        /// </summary>
        public string NegotiatorContactText
        {
            get { return negotiatorContactText; }
            set { negotiatorContactText = value; OnPropertyChanged(nameof(NegotiatorContactText)); }
        }


        private string negotiatorAddress;
        /// <summary>
        /// Property for negotiatorAddress
        /// </summary>
        public string NegotiatorAddress
        {
            get { return negotiatorAddress; }
            set { negotiatorAddress = value; OnPropertyChanged(nameof(NegotiatorAddress)); }
        }
        private string firstColFrameColor;
        /// <summary>
        /// Property for firstColFrameColor
        /// </summary>

        public string FirstColFrameColor
        {
            get { return firstColFrameColor; }
            set { firstColFrameColor = value; OnPropertyChanged(nameof(FirstColFrameColor)); }
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

        private bool isRecordsAvailable;
        /// <summary>
        /// Property for IsRecordsAvailable
        /// </summary>

        public bool IsRecordsAvailable
        {
            get { return isRecordsAvailable; }
            set { isRecordsAvailable = value; OnPropertyChanged(nameof(IsRecordsAvailable)); }
        }
        private bool isRecordNotAvail;
        /// <summary>
        /// Property for isRecordNotAvail
        /// </summary>

        public bool IsRecordNotAvail
        {
            get { return isRecordNotAvail; }
            set { isRecordNotAvail = value; OnPropertyChanged(nameof(IsRecordNotAvail)); }
        }

        private string recordNotFound;
        /// <summary>
        /// Property for recordNotFound
        /// </summary>

        public string RecordNotFound
        {
            get { return recordNotFound; }
            set { recordNotFound = value; OnPropertyChanged(nameof(RecordNotFound)); }
        }

        private ObservableCollection<QBidViewDetailViewModel> _qBidViewDetailViewModel;
        /// <summary>
        /// property for quatationDetailsList
        /// </summary>
        public ObservableCollection<QBidViewDetailViewModel> QuatationDetailsList
        {
            get { return _qBidViewDetailViewModel; }
            set { _qBidViewDetailViewModel = value; OnPropertyChanged(); }
        }

        private bool isAcceptedByMember;
        /// <summary>
        /// Property for isAcceptedByMember
        /// </summary>
        public bool IsAcceptedByMember
        {
            get { return isAcceptedByMember; }
            set { isAcceptedByMember = value; OnPropertyChanged(nameof(IsAcceptedByMember)); }
        }

        private string totalPrice;
        /// <summary>
        /// Property for totalPrice
        /// </summary>
        public string TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }
        private bool isShowHide;
        public bool IsShowHide
        {
            get { return isShowHide; }
            set { isShowHide = value; OnPropertyChanged(nameof(IsShowHide)); }
        }

        private string totalNegotiatedPrice;
        /// <summary>
        /// Property for totalNegotiatedPrice
        /// </summary>
        public string TotalNegotiatedPrice
        {
            get { return totalNegotiatedPrice; }
            set { totalNegotiatedPrice = value; OnPropertyChanged(nameof(TotalNegotiatedPrice)); }
        }
        private string totalSaving;
        /// <summary>
        /// Property for TotalSaving
        /// </summary>
        public string TotalSaving
        {
            get { return totalSaving; }
            set { totalSaving = value; OnPropertyChanged(nameof(TotalSaving)); }
        }

        private string totalSavingPayment;
        /// <summary>
        /// Property for TotalSavingPayment
        /// </summary>
        public string TotalSavingPayment
        {
            get { return totalSavingPayment; }
            set { totalSavingPayment = value; OnPropertyChanged(nameof(TotalSavingPayment)); }
        }

        private string qBIDChargesFee;
        /// <summary>
        /// Property for QBIDChargesFee
        /// </summary>
        public string QBIDChargesFee
        {
            get { return qBIDChargesFee; }
            set { qBIDChargesFee = value; OnPropertyChanged(nameof(QBIDChargesFee)); }
        }

        private string invoice;
        /// <summary>
        /// Property for Invoice
        /// </summary>
        public string Invoice
        {
            get { return invoice; }
            set { invoice = value; OnPropertyChanged(nameof(Invoice)); }
        }

        private string transaction;
        /// <summary>
        /// Property for Transaction
        /// </summary>
        public string Transaction
        {
            get { return transaction; }
            set { transaction = value; OnPropertyChanged(nameof(Transaction)); }
        }
        private string transactionStatus;
        /// <summary>
        /// Property for Transaction status
        /// </summary>
        public string TransactionStatus
        {
            
            get { return transactionStatus; }
            set { transactionStatus = value; OnPropertyChanged(nameof(TransactionStatus)); }
        }

        private string qBidStatusMessage;
        /// <summary>
        /// Property for QBidStatusMessage
        /// </summary>
        public string QBidStatusMessage
        {
            get { return qBidStatusMessage; }
            set { qBidStatusMessage = value; OnPropertyChanged(nameof(QBidStatusMessage)); }
        }

        private string qBidStatusColor;
        /// <summary>
        /// Property for QBidStatusMessage
        /// </summary>
        public string QBidStatusColor
        {
            get { return qBidStatusColor; }
            set { qBidStatusColor = value; OnPropertyChanged(nameof(QBidStatusColor)); }
        }
        private string approvedDeclinedFieldName;
        /// <summary>
        /// Property for ApprovedDeclinedFieldName
        /// </summary>
        public string ApprovedDeclinedFieldName
        {
            get { return approvedDeclinedFieldName; }
            set { approvedDeclinedFieldName = value; OnPropertyChanged(nameof(ApprovedDeclinedFieldName)); }
        }

        private string quotationApprovedDate;
        /// <summary>
        /// Property for quotationApprovedDate
        /// </summary>
        public string QuotationApprovedDate
        {
            get { return quotationApprovedDate; }
            set { quotationApprovedDate = value; OnPropertyChanged(nameof(QuotationApprovedDate)); }
        }

        private bool iSQuotationApproved;
        /// <summary>
        /// Property for iSQuotationApproved
        /// </summary>
        public bool ISQuotationApproved
        {
            get { return iSQuotationApproved; }
            set { iSQuotationApproved = value; OnPropertyChanged(nameof(ISQuotationApproved)); }
        }
        private bool iSQuotationDeclined;
        /// <summary>
        /// Property for iSQuotationDeclined
        /// </summary>
        public bool ISQuotationDeclined
        {
            get { return iSQuotationDeclined; }
            set { iSQuotationDeclined = value; OnPropertyChanged(nameof(ISQuotationDeclined)); }
        }
        private bool isOemVisible;
        /// <summary>
        /// Property for isOemVisible
        /// </summary>
        public bool IsOemVisible
        {
            get { return isOemVisible; }
            set { isOemVisible = value; OnPropertyChanged(nameof(IsOemVisible)); }
        }

        private bool isVisiblefacilityRepresentative;
        /// <summary>
        /// Property for isVisible facilityRepresentative
        /// </summary>
        public bool IsVisiblefacilityRepresentative
        {
            get { return isVisiblefacilityRepresentative; }
            set { isVisiblefacilityRepresentative = value; OnPropertyChanged(nameof(IsVisiblefacilityRepresentative)); }
        }

        private bool isCallorSmsShow;
        /// <summary>
        /// show IsCallorSmsShow UI
        /// </summary>
        public bool IsCallorSmsShow
        {
            get { return isCallorSmsShow; }
            set { isCallorSmsShow = value; OnPropertyChanged(nameof(IsCallorSmsShow)); }
        }

        private bool showTransferAmountMessage;
        /// <summary>
        /// property for on show transfer amount to negotiator message icon
        /// </summary>
        public bool ShowTransferAmountMessage
        {
            get { return showTransferAmountMessage; }
            set { showTransferAmountMessage = value; OnPropertyChanged(nameof(ShowTransferAmountMessage)); }
        }

        #endregion

        #region Commands

        private Command commandOnShowTransferAmountMessage;
        /// <summary>
        ///  Command for on show transfer amount to negotiator message 
        /// </summary>
        public Command CommandOnShowTransferAmountMessage
        {
            get
            {
                if (commandOnShowTransferAmountMessage == null)
                {
                    commandOnShowTransferAmountMessage = new Command( (res) =>
                    {
                        try
                        {
                            App.Current.MainPage.DisplayAlert(string.Empty, ResourceValues.ApproveMinimumChargeMessage, ResourceValues.OkButtontext);
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnShowTransferAmountMessage;
            }
        }


        private Command commandOnBackButton;
        /// <summary>
        /// This command use for On Back For QbidList screen
        /// </summary>
        public Command CommandOnBackButton
        {
            get
            {
                if (commandOnBackButton == null)
                {
                    commandOnBackButton = new Command(async () =>
                    {
                        try
                        {
                            if (IsLoader == false)
                            {
                               
                                await App.Current.MainPage.Navigation.PushAsync(new DashboardView());
                            }

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnBackButton;
            }
        }


        ApproveQuotationRequest approveQuotationRequest = null;

        private Command approvePriceByMemberCommand;
        /// <summary>
        /// command for on AcceptPriceByMember
        /// </summary>
        public Command ApprovePriceByMemberCommand
        {
            get
            {
                if (approvePriceByMemberCommand == null)
                {
                    approvePriceByMemberCommand = new Command(async (res) =>
                    {
                        try
                        {
                            approveQuotationRequest = new ApproveQuotationRequest();
                            if (Convert.ToDecimal(TotalPrice.Replace(ConstantValues.CurencySymbal, string.Empty)) > 0)
                            {
                                int Id = Convert.ToInt32(res);
                                string result = string.Empty;
                                switch (Id)
                                {
                                    case 1:
                                        result = ResourceValues.ConfirmApproveOriginalPriceMessage;
                                        approveQuotationRequest.negotiatorId = QBidHelper.NegotiatorId.ToString();
                                        approveQuotationRequest.type = "originalFacility";
                                        approveQuotationRequest.typeId = QBidHelper.FacilityId.ToString();
                                        break;
                                    case 2:
                                       
                                        result = ResourceValues.ConfirmApproveQbidMessage + "\n" + "\n" + ResourceValues.TitleForApproveDeclineMessage;
                                        approveQuotationRequest.negotiatorId = QBidHelper.NegotiatorId.ToString();
                                        approveQuotationRequest.type = "negotiator";
                                        approveQuotationRequest.typeId = QBidHelper.NegotiatorId.ToString();
                                        break;
                                }

                                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(ApprovePopUp, ResourceValues.YesButtontext, ResourceValues.NoButtontext, result.ToString(), true)).ConfigureAwait(true);
                            }
                            else
                            {
                                DependencyService.Get<IToastMessage>().ShortAlert(ResourceValues.ConfirmApproveServiceItemMessage);
                            }
                            IsLoader = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                            IsLoader = false;
                        }
                    });
                }
                return approvePriceByMemberCommand;
            }
        }

        DeclinedQuotationRequest declineQuotationRequest = null;

        private Command declinePriceByMemberCommand;
        /// <summary>
        /// command for on DeclinePriceByMember
        /// </summary>
        public Command DeclinePriceByMemberCommand
        {
            get
            {
                if (declinePriceByMemberCommand == null)
                {
                    declinePriceByMemberCommand = new Command(async (res) =>
                    {
                        try
                        {
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(DeclinedPopUp, ResourceValues.YesButtontext, ResourceValues.NoButtontext, ResourceValues.ConfirmDeclineMessage  + "\n" + "\n" + ResourceValues.TitleForApproveDeclineMessage, true)).ConfigureAwait(false);

                        }
                        catch (Exception ex)
                        {
                            IsLoader = false;
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return declinePriceByMemberCommand;
            }
        }

        private Command commandOnCalling;
        /// <summary>
        ///  Command for on Calling
        /// </summary>
        public Command CommandOnCalling
        {
            get
            {
                if (commandOnCalling == null)
                {
                    commandOnCalling = new Command( (res) =>
                    {
                        try
                        {
                            contactNoMobile = string.Empty;
                            contactNoText = string.Empty;
                            if (!string.IsNullOrEmpty(FacilityContact))
                            {
                                contactNoMobile = FacilityContact;
                                contactNoText = FacilityContact;
                                IsCallorSmsShow = true;
                              
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnCalling;
            }
        }

        private Command commandOnNegoCalling;
        /// <summary>
        ///  Command for on commandOnNegoCalling
        /// </summary>
        public Command CommandOnNegoCalling
        {
            get
            {
                if (commandOnNegoCalling == null)
                {
                    commandOnNegoCalling = new Command( (res) =>
                    {
                        try
                        {
                            contactNoMobile = string.Empty;
                            contactNoText = string.Empty;
                            if (!string.IsNullOrEmpty(NegotiatorContactMobile))
                            {
                                contactNoMobile = NegotiatorContactMobile;
                                IsCallorSmsShow = true;
                             
                            }
                            if (!string.IsNullOrEmpty(NegotiatorContactText))
                            {
                                contactNoText = NegotiatorContactText;
                                IsCallorSmsShow = true;
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnNegoCalling;
            }
        }

        private Command commandOnCallorSms;
        /// <summary>
        ///  Command for on Calling
        /// </summary>
        public Command CommandOnCallorSms
        {
            get
            {
                if (commandOnCallorSms == null)
                {
                    commandOnCallorSms = new Command( (res) =>
                    {
                        try
                        {

                            var opt = res;
                            switch (opt)
                            {
                                case ConstantValues.One:
                                    if (!string.IsNullOrEmpty(contactNoMobile))
                                    {
                                        PhoneDialer.Open(contactNoMobile);
                                        IsCallorSmsShow = false;
                                    }
                                    else
                                    {
                                        App.Current.MainPage.DisplayAlert(string.Empty, ResourceValues.UpdateCallNoMessage, ResourceValues.OkButtontext);
                                    }
                                    break;

                                case ConstantValues.Two:
                                    if (!string.IsNullOrEmpty(contactNoText))
                                    {
                                        Launcher.OpenAsync(new Uri(String.Format("sms:{0}", contactNoText)));
                                       
                                        IsCallorSmsShow = false;
                                    }
                                    else
                                    {
                                        App.Current.MainPage.DisplayAlert(string.Empty, ResourceValues.UpdateTextNoMessage, ResourceValues.OkButtontext);
                                    }

                                    break;
                             
                            }

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnCallorSms;
            }
        }

        private Command commandOnEmailtoVendor;
        /// <summary>
        ///  Command for email to vendor 
        /// </summary>
        public Command CommandOnEmailtoVendor
        {
            get
            {
                if (commandOnEmailtoVendor == null)
                {
                    commandOnEmailtoVendor = new Command( (res) =>
                    {
                        try
                        {
                            var ToReceiptMail = string.Empty;
                            if (!string.IsNullOrEmpty(FacilityEmail))
                            {
                                ToReceiptMail = FacilityEmail;
                                Launcher.OpenAsync(new Uri(String.Format("mailto:{0}", ToReceiptMail)));
                             
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnEmailtoVendor;
            }
        }

        private Command commandOnViewQBidStatus;
        /// <summary>
        ///  Command for on ViewQBidStatus 
        /// </summary>
        public Command CommandOnViewQBidStatus
        {
            get
            {
                if (commandOnViewQBidStatus == null)
                {
                    commandOnViewQBidStatus = new Command(async (res) =>
                    {
                        try
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new QBidStatusView());
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnViewQBidStatus;
            }
        }

       

        private Command hireNegotiatorCommand;
        /// <summary>
        ///  Command for on commandOnHireNegotiatior 
        /// </summary>
        public Command HireNegotiatorCommand
        {
            get
            {
                if (hireNegotiatorCommand == null)
                {
                    hireNegotiatorCommand = new Command(async () =>
                    {
                        try
                        {
                            IsLoader = true;
                            IsPopupForHireNegoDisc = false;
                            await NegotiatiorDetails().ConfigureAwait(true);
                        }
                        catch (Exception ex)
                        {
                            IsLoader = false;
                            LogManager.TraceErrorLog(ex);
                        }
                        finally
                        {
                            IsLoader = false;
                        }
                    });
                }
                return hireNegotiatorCommand;
            }
        }

        private Command changeNegotiatorCommand;
        /// <summary>
        ///  Command for on ChangeNegotiatorCommand 
        /// </summary>
        public Command ChangeNegotiatorCommand
        {
            get
            {
                if (changeNegotiatorCommand == null)
                {
                    changeNegotiatorCommand = new Command(async (res) =>
                    {
                        try
                        {
                            int Id = Convert.ToInt32(res);
                            switch (Id)
                            {
                                case 0:
                                    No = true;
                                    Yes = false;
                                    break;
                                case 1:
                                    Yes = true;
                                    No = false;
                                    break;
                            }
                            IsPopupForBlockNego = false;
                            await NegotiatiorDetails().ConfigureAwait(true);

                        }
                        catch (Exception ex)
                        {
                            IsLoader = false;
                            LogManager.TraceErrorLog(ex);
                        }
                        finally
                        {
                            IsLoader = false;
                        }
                    });
                }
                return changeNegotiatorCommand;
            }
        }

        private Command blockNegotiatorCommand;
        /// <summary>
        ///  Command for on ChangeNegotiatorCommand 
        /// </summary>
        public Command BlockNegotiatorCommand
        {
            get
            {
                if (blockNegotiatorCommand == null)
                {
                    blockNegotiatorCommand = new Command( () =>
                    {
                        try
                        {

                            IsPopupForChangeNego = false;
                            IsPopupForBlockNego = true;

                        }
                        catch (Exception ex)
                        {
                            IsLoader = false;
                            LogManager.TraceErrorLog(ex);
                        }
                        finally
                        {
                            IsLoader = false;
                        }
                    });
                }
                return blockNegotiatorCommand;
            }
        }

        private Command commandOnHireDiscussion;
        /// <summary>
        ///  Command for on CommandOnHireDiscussion 
        /// </summary>
        public Command CommandOnHireDiscussion
        {
            get
            {
                if (commandOnHireDiscussion == null)
                {
                    commandOnHireDiscussion = new Command(async () =>
                    {
                        try
                        {
                            await GetQBidStatus().ConfigureAwait(true);
                            IsPopupForHireNegoDisc = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnHireDiscussion;
            }
        }


        private Command hireNegotiatorDialogShowCommand;
        /// <summary>
        ///  Command for on Negotiatior Details
        /// </summary>
        public Command HireNegotiatorDialogShowCommand
        {
            get
            {
                if (hireNegotiatorDialogShowCommand == null)
                {
                    hireNegotiatorDialogShowCommand = new Command(async (res) =>
                    {
                        try
                        {

                            if (HireNegotiatorBtnText.Contains("Change"))
                            {

                                var result = ResourceValues.ConfirmChangeNegotiatorMessage;
                                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(ChangeNegoPopUp, ResourceValues.YesButtontext, ResourceValues.NoButtontext, result.ToString(), true)).ConfigureAwait(true);

                            }
                            else
                            {
                                var memberCardAdded = Preferences.Get(ConstantValues.IsMemberCardAddedPref, 0);
                                if (memberCardAdded == 1)
                                {
                                    IsPopupForHireNegoDisc = true;
                                    IsPopupForChangeNego = false;
                                }
                                else
                                {
                                    bool result = await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.ConfirmAddCreditCardMessage, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext);
                                    if (result)
                                    {
                                        App.Current.MainPage.Navigation.PushAsync(new PaymetCardList());
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return hireNegotiatorDialogShowCommand;
            }
        }

        private Command commandOnClosePopUp;
        /// <summary>
        ///  Command for on Close PopUp 
        /// </summary>
        public Command CommandOnClosePopUp
        {
            get
            {
                if (commandOnClosePopUp == null)
                {
                    commandOnClosePopUp = new Command(() =>
                    {
                        try
                        {
                            IsPopupForHireNegoDisc = false;
                            IsPopupForChangeNego = false;
                            IsPopupForBlockNego = false;
                            IsPopupForNoteandBenefits = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnClosePopUp;
            }
        }

        private Command commandOnNoteAndBenefit;
        /// <summary>
        ///  Command for show Note and Benefit popup message 
        /// </summary>
        public Command CommandOnNoteAndBenefit
        {
            get
            {
                if (commandOnNoteAndBenefit == null)
                {
                    commandOnNoteAndBenefit = new Command(() =>
                    {
                        try
                        {
                           
                            IsPopupForNoteandBenefits = true;

                            
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnNoteAndBenefit;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// this method for change/ hire  negotiator
        /// </summary>
        /// <returns></returns>
        private async Task NegotiatiorDetails()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    IsLoader = true;
                    CommonResponse commonResponse = new CommonResponse();
                    if (HireNegotiatorBtnText.Contains("Change"))
                    {
                    var changeNegotiatorRequestModel = new ChangeNegotiatorRequestModel();
                        changeNegotiatorRequestModel.quotationId = QBidHelper.QuotationId.ToString();
                        changeNegotiatorRequestModel.facilityId = QBidHelper.FacilityId; 

                        if (Yes == true)
                        {
                            changeNegotiatorRequestModel.blockNegotiator = "0";
                        }
                        else if (No == true)
                        {
                            changeNegotiatorRequestModel.blockNegotiator = "1";
                        }
                        else
                        {
                            changeNegotiatorRequestModel.blockNegotiator = "0";
                        }

                        commonResponse = await apiServices.ChangeNegotiatiorDetails(changeNegotiatorRequestModel);
                    }
                    else
                    {
                        var hireNegotiatorRequestModel = new HireNegotiatorRequestModel();
                        hireNegotiatorRequestModel.quotationId = QBidHelper.QuotationId.ToString();
                        hireNegotiatorRequestModel.facilityId = QBidHelper.FacilityId;   
                        commonResponse = await apiServices.HierNegotiatiorDetails(hireNegotiatorRequestModel);
                    }
                    

                    if (commonResponse != null)
                    {
                        if (commonResponse.code == (int)HttpStatusCode.OK)
                        {
                            IsLoader = false;
                            Task.Delay(2000);
                            await App.Current.MainPage.DisplayAlert(string.Empty, commonResponse.message, ResourceValues.OkButtontext);
                            IsShowHireNegotiator = false;
                        }
                        else
                        {
                            var err = string.Empty;
                            if (Convert.ToString(commonResponse.message).ToLower() == "failed")
                            {
                                err = Convert.ToString(commonResponse.error);
                            }
                            DependencyService.Get<IToastMessage>().LongAlert(commonResponse.message.ToString() + "\n"+ err);
                        }
                    }
                    await GetQBidStatus().ConfigureAwait(true);

                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                IsLoader = false;
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }


        /// <summary>
        /// /this Method for Get Qbid detail
        /// </summary>
        /// <returns></returns>
       
        private async Task GetQBidDetails()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    StackLayOutList = new ObservableCollection<ServiceItemHeaderModel>();

                    UtilHelper utilHelper = new UtilHelper();
                    IsLoader = true;

                    var isVisibleApprovedClm = true;
                    var data = await apiServices.GetQBidDetails(QBidHelper.QuotationId);
                    ServiceItemHeaderModel serviceItemHeaderModel = new ServiceItemHeaderModel();

                    StackLayout stackLayout = new StackLayout();

                    Views.ContentViews.OriginalFacilityServiceItems originalFacilityServiceItemViews = null;
                    Views.ContentViews.OriginalFacilityView originalFacilityContentView = null;
                    var originalTotal = Convert.ToDecimal(ConstantValues.DefaultValue);
                    var negotiatedTotal = Convert.ToDecimal(ConstantValues.DefaultValue);
                    var totalSaving = Convert.ToDecimal(ConstantValues.DefaultValue);

                    if (data != null)
                    {
                        if ((data.data != null) && (data.data.Any()))
                        {

                            foreach (var item in data.data)
                            {
                                if (!string.IsNullOrEmpty(item.FacilityName))
                                {
                                    bool itemEnable = true;

                                    IsRecordsAvailable = false;
                                    IsRecordNotAvail = true;
                                    RecordNotFound = string.Empty;
                                    FacilityName = item.FacilityName;
                                    FacilityEmail = item.FacilityEmail;
                                    FacilityAddress = item.FacilityAddress;
                                    ServiceName = item.serviceName;
                                    FacilityContact = item.FacilityContact;
                                    FacilityRepresentative = item.FacilityAdvisor; 
                                    IsVisibleFacilityContact = !String.IsNullOrEmpty(item.FacilityContact);
                                    IsVisibleFacilityName = !String.IsNullOrEmpty(item.FacilityName);
                                    IsVisibleFacilityEmail = !String.IsNullOrEmpty(item.FacilityEmail);
                                    IsVisibleFacilityAddress = !String.IsNullOrEmpty(item.FacilityAddress);
                                    IsVisiblefacilityRepresentative = !String.IsNullOrEmpty(item.FacilityAdvisor);
                                    SpecialNote = (item.SpecialNote != null) ? item.SpecialNote : ResourceValues.TextNothingToShow;                                 
                                    Benefits = (item.Benefits != null) ? item.Benefits : ResourceValues.TextNothingToShow;
                                    if (!string.IsNullOrEmpty(item.approvedChargeToUser))
                                    {
                                        ShowTransferAmountMessage = (Convert.ToDecimal(item.approvedChargeToUser) > Convert.ToDecimal("0.49")) ? false : true;
                                    }
                                    else
                                    {
                                        ShowTransferAmountMessage = false;
                                    }
                                    QBidHelper.FacilityId = item.facilityId.ToString();
                                    if ((item.NegotiatorDetails != null) && (item.NegotiatorDetails.Any()))
                                    {
                                        QBidHelper.NegotiatorId = item.NegotiatorDetails[0].NegotiatorId;
                                        IsNegotiatorAvailble = true;
                                        NegotiatorName = item.NegotiatorDetails[0].FirstName + item.NegotiatorDetails[0].Last_name;
                                        NegotiatorEmail = item.NegotiatorDetails[0].NegotiatorEmail;
                                        NegotiatorAddress = item.NegotiatorDetails[0].AddressLine1 + ConstantValues.OneSpace + item.NegotiatorDetails[0].AddressLine2;
                                        NegotiatorContactMobile = item.NegotiatorDetails[0].MobileCall;
                                        NegotiatorContactText = item.NegotiatorDetails[0].MobileText;
                                    }
                                    else
                                    {
                                        IsNegotiatorAvailble = false;
                                    }
                                    if (item.QbidCurrentStatus != null)
                                    {

                                        if (item.QbidCurrentStatus.Any(a => a.statusId == 8 && a.currentStatus))
                                        {
                                            ISQuotationApproved = true;
                                            QBidStatusMessage = ResourceValues.QBidApprovedMessage;
                                            ApprovedDeclinedFieldName = ResourceValues.ApprovedDate;
                                            QBidStatusColor = ConstantValues.AppColor;
                                            itemEnable = false;
                                            IsShowHide = false;
                                        }
                                        else if (item.QbidCurrentStatus.Any(a => a.statusId == 7 && a.currentStatus))
                                        {
                                            ISQuotationApproved = true;
                                            QBidStatusMessage = ResourceValues.QBidDeclineMessage;
                                            ApprovedDeclinedFieldName = ResourceValues.DeclinedDate;
                                            QBidStatusColor = ConstantValues.AppRedColor;
                                            itemEnable = false;
                                            IsShowHide = false;
                                        }
                                        else if (item.QbidCurrentStatus.Any(a => a.statusId == 10 && a.currentStatus))
                                        {
                                            ISQuotationApproved = true;
                                            QBidStatusMessage = ResourceValues.QBidUnFinishedMessage;
                                            ApprovedDeclinedFieldName = ResourceValues.UnFinishedDate;
                                            QBidStatusColor = ConstantValues.AppRedColor;
                                            itemEnable = false;
                                            IsShowHide = false;
                                        }
                                        else if (item.QbidCurrentStatus.Any(a => a.statusId == 5 && a.currentStatus))
                                        {
                                            itemEnable = true;
                                            IsShowHide = true;
                                        }
                                        else
                                        {
                                            ISQuotationApproved = false;
                                            QBidStatusMessage = string.Empty;
                                            QBidStatusColor = ConstantValues.AppBlackColor;
                                            itemEnable = false;
                                            IsShowHide = false;
                                        }
                                    }
                                    if ((item.ServiceItem != null) && (item.ServiceItem.Any()))
                                    {
                                        if (item.ServiceItem.Any(a => a.OEM == "1"))
                                        {
                                            IsOemVisible = true;
                                        }
                                        else
                                        {
                                            IsOemVisible = false;
                                        }
                                        if (item.ServiceItem.Any(res => res.Price.Count() > 0))
                                        {
                                           
                                        }
                                        originalFacilityContentView = new Views.ContentViews.OriginalFacilityView("Service", "Original Price", "OEM", IsOemVisible, "Negotiated Price", isVisibleApprovedClm);
                                        stackLayout.Children.Add(originalFacilityContentView);
                                        stackLayout.IsClippedToBounds = true;
                                        serviceItemHeaderModel.StackLayOutHeaderList.Children.Add(stackLayout);


                                        bool itemChecked = false;
                                        var negotiatedPrice = ConstantValues.DefaultValue;
                                        for (int i = 0; i < item.ServiceItem.Count; i++)
                                        {
                                            if (item.ServiceItem[i].Price.Count == 0)
                                            {
                                                negotiatedPrice = ConstantValues.DefaultValue;
                                            }
                                            else
                                            {
                                                negotiatedPrice = Convert.ToDecimal(!string.IsNullOrEmpty(item.ServiceItem[i].Price[0].NegotiatedPrice.ToString())) > 0 ? item.ServiceItem[i].Price[0].NegotiatedPrice.ToString() : ConstantValues.DefaultValue;
                                                itemChecked = Convert.ToBoolean(item.ServiceItem[i].Price[0].Approved);

                                                if (itemChecked == true)
                                                {
                                                    originalTotal += Convert.ToDecimal(!string.IsNullOrEmpty(item.ServiceItem[i].ServcieItemPrice.ToString())) > 0 ? Convert.ToDecimal(item.ServiceItem[i].ServcieItemPrice) : Convert.ToDecimal(ConstantValues.DefaultValue);
                                                    negotiatedTotal += Convert.ToDecimal(!string.IsNullOrEmpty(item.ServiceItem[i].Price[0].NegotiatedPrice.ToString())) > 0 ? Convert.ToDecimal(item.ServiceItem[i].Price[0].NegotiatedPrice) : Convert.ToDecimal(ConstantValues.DefaultValue);
                                                }
                                            }
                                            var serviceItemOEM = Convert.ToInt32(item.ServiceItem[i].OEM) > 0 ? ResourceValues.YesButtontext : ResourceValues.NoButtontext;
                                            originalFacilityServiceItemViews = new Views.ContentViews.OriginalFacilityServiceItems(item.ServiceItem[i].ServcieItemName,Convert.ToDecimal(item.ServiceItem[i].ServcieItemPrice).ToString(ConstantValues.DefaultValue), serviceItemOEM, IsOemVisible, Convert.ToDecimal(negotiatedPrice).ToString(ConstantValues.DefaultValue), item.ServiceItem[i].IsAcceptedByMember, itemChecked, itemEnable, true, item.ServiceItem[i].FacilityId.ToString(), item.ServiceItem[i].ServiceItemId.ToString());
                                            stackLayout.Children.Add(originalFacilityServiceItemViews);
                                        }
                                        totalSaving = Convert.ToDecimal(originalTotal) - Convert.ToDecimal(negotiatedTotal);
                                    }
                                    TotalPrice = ConstantValues.CurencySymbal + originalTotal;
                                    TotalNegotiatedPrice = ConstantValues.CurencySymbal + negotiatedTotal;
                                    TotalSaving = ConstantValues.CurencySymbal + totalSaving;

                                    if (item.paymentDetails != null && item.paymentDetails.Count > 0)
                                    {
                                        var charge = Convert.ToDecimal(ConstantValues.DefaultValue);
                                        var fee = Convert.ToDecimal(ConstantValues.DefaultValue);
                                        foreach (var paymentItem in item.paymentDetails)
                                        {
                                            TotalSavingPayment = (paymentItem.totalSaving != null) ? ConstantValues.CurencySymbal + Convert.ToDecimal(paymentItem.totalSaving).ToString(ConstantValues.DefaultValue) : ConstantValues.CurencySymbal+ ConstantValues.DefaultValue;
                                            Invoice = (string.IsNullOrEmpty(paymentItem.OrderId) ? string.Empty : paymentItem.OrderId); 
                                            Transaction = (string.IsNullOrEmpty(paymentItem.transactionId) ? string.Empty : paymentItem.transactionId);
                                            TransactionStatus = (string.IsNullOrEmpty(paymentItem.transactionStatus) ? string.Empty: paymentItem.transactionStatus);

                                                                        
                                            var localtime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(paymentItem.ApprovedDeclined));
                                            QuotationApprovedDate = localtime.ToString(ConstantValues.DateFormate);


                                          
                                            charge += (paymentItem.Charge != null) ? Convert.ToDecimal(paymentItem.Charge) : Convert.ToDecimal(ConstantValues.DefaultValue);
                                        }
                                        var feecharge =
                                        QBIDChargesFee = ConstantValues.CurencySymbal + Convert.ToString(charge);   
                                    }


                                }
                             
                            }

                            StackLayOutList.Add(serviceItemHeaderModel);

                        }
                        else
                        {
                            RecordNotFound = ResourceValues.NorecordsfoundMessage;
                            IsRecordsAvailable = true;
                            IsRecordNotAvail = false;
                        }
                    }
                   

                    IsLoader = false;
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }

        /// <summary>
        /// This method for get Quotation status
        /// </summary>
        /// <returns></returns>
        public async Task GetQBidStatus()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    FirstColSpan = 2;
                    SecoundColSpan = 11;
                    SecoundColStart = 2;

                var qBidStatusRequest = new QBidStatusRequest();
                    qBidStatusRequest.QuotationId = QBidHelper.QuotationId.ToString();
                var qBidStatusDetails = await apiServices.UserQuotationQbidStatus(qBidStatusRequest);
                   if(qBidStatusDetails !=null )
                    {
                        if (qBidStatusDetails.data != null)
                        {
                            if (qBidStatusDetails.data.CurrentStatus.Count > 0)
                            {
                                IsLoader = true;
                             
                                int status = qBidStatusDetails.data.CurrentStatus.Where(sts => sts.currentStatus == true).Select(sts => sts.statusId).FirstOrDefault();
                             
                                switch (status)
                                {
                                    case 1:
                                        FirstColSpan = 2;
                                        SecoundColSpan = 11;
                                        SecoundColStart = 2;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        HireORChangeNegotiatorText = string.Empty;

                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                        FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                        FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = false;
                                        IsThirdStepCompleted = false;
                                        IsFourthStepCompleted = false;
                                        IsFifthStepCompleted = false;
                                        IsSixthStepCompleted = false;
                                        IsSeventhStepCompleted = false;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = true;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = true;
                                        IsFourthStepPending = true;
                                        IsFifthStepPending = true;
                                        IsSixthStepPending = true;
                                        IsSeventhStepPending = true;
                                        break;
                                    case 2:

                                        FirstColSpan = 4;
                                        SecoundColSpan = 9;
                                        SecoundColStart = 4;
                                        IsShowHireNegotiator = true;
                                        HireNegotiatorBtnText = ResourceValues.ButtonClicktohireNegotiator; 
                                        HireORChangeNegotiatorText = ResourceValues.ConfirmHireNegotiatorMessage;

                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                        FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = false;
                                        IsFourthStepCompleted = false;
                                        IsFifthStepCompleted = false;
                                        IsSixthStepCompleted = false;
                                        IsSeventhStepCompleted = false;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = true;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = true;
                                        IsFifthStepPending = true;
                                        IsSixthStepPending = true;
                                        IsSeventhStepPending = true;
                                        break;
                                    case 3:
                                        FirstColSpan = 6;
                                        SecoundColSpan = 7;
                                        SecoundColStart = 6;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        HireORChangeNegotiatorText = string.Empty;

                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = false;
                                        IsFifthStepCompleted = false;
                                        IsSixthStepCompleted = false;
                                        IsSeventhStepCompleted = false;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = true;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = true;
                                        IsSixthStepPending = true;
                                        IsSeventhStepPending = true;
                                        break;
                                    case 4:
                                        FirstColSpan = 8;
                                        SecoundColSpan = 5;
                                        SecoundColStart = 8;

                                        IsShowHireNegotiator = true;
                                        HireNegotiatorBtnText = ResourceValues.ButtonChangeNegotiator;   
                                        HireORChangeNegotiatorText = ResourceValues.ConfirmChangeNegotiatorMessage;

                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppColor;
                                        SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = true;
                                        IsFifthStepCompleted = false;
                                        IsSixthStepCompleted = false;
                                        IsSeventhStepCompleted = false;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = true;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = false;
                                        IsSixthStepPending = true;
                                        IsSeventhStepPending = true;
                                        break;
                                    case 5:
                                        FirstColSpan = 10;
                                        SecoundColSpan = 3;
                                        SecoundColStart = 10;

                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;

                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppColor;
                                        SixthColFrameColor = ConstantValues.AppColor;
                                        SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = true;
                                        IsFifthStepCompleted = true;
                                        IsSixthStepCompleted = false;
                                        IsSeventhStepCompleted = false;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = true;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = false;
                                        IsSixthStepPending = false;
                                        IsSeventhStepPending = true;
                                        break;
                                    case 6:
                                        FirstColSpan = 12;
                                        SecoundColSpan = 1;
                                        SecoundColStart = 12;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppColor;
                                        SixthColFrameColor = ConstantValues.AppColor;
                                        SeventhColFrameColor = ConstantValues.AppColor;

                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = true;
                                        IsFifthStepCompleted = true;
                                        IsSixthStepCompleted = true;
                                        IsSeventhStepCompleted = false;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = true;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = false;
                                        IsSixthStepPending = false;
                                        IsSeventhStepPending = false;
                                        break;
                                    case 7:
                                        FirstColSpan = 12;
                                        SecoundColSpan = 1;
                                        SecoundColStart = 12;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppColor;
                                        SixthColFrameColor = ConstantValues.AppColor;
                                        SeventhColFrameColor = ConstantValues.AppRedColor;


                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = true;
                                        IsFifthStepCompleted = true;
                                        IsSixthStepCompleted = true;
                                        IsSeventhStepDeclined = true;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = false;
                                        IsSixthStepPending = false;
                                        IsSeventhStepPending = false;
                                        break;
                                    case 8:
                                        FirstColSpan = 12;
                                        SecoundColSpan = 1;
                                        SecoundColStart = 12;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppColor;
                                        SixthColFrameColor = ConstantValues.AppColor;
                                        SeventhColFrameColor = ConstantValues.AppColor;


                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = true;
                                        IsFifthStepCompleted = true;
                                        IsSixthStepCompleted = true;
                                        IsSeventhStepCompleted = true;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = false;
                                        IsSixthStepPending = false;
                                        IsSeventhStepPending = false;
                                        break;
                                    case 9:
                                        FirstColSpan = 12;
                                        SecoundColSpan = 1;
                                        SecoundColStart = 12;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppColor;
                                        SixthColFrameColor = ConstantValues.AppColor;
                                        SeventhColFrameColor = ConstantValues.AppRedColor;


                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = true;
                                        IsFifthStepCompleted = true;
                                        IsSixthStepCompleted = true;
                                        IsSeventhStepDeclined = true;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = false;
                                        IsSixthStepPending = false;
                                        IsSeventhStepPending = false;
                                        break;
                                    case 10:
                                        FirstColSpan = 12;
                                        SecoundColSpan = 1;
                                        SecoundColStart = 12;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        FirstColFrameColor = ConstantValues.AppColor;
                                        SecoundColFrameColor = ConstantValues.AppColor;
                                        ThirdColFrameColor = ConstantValues.AppColor;
                                        FourthColFrameColor = ConstantValues.AppColor;
                                        FifthColFrameColor = ConstantValues.AppColor;
                                        SixthColFrameColor = ConstantValues.AppColor;
                                        SeventhColFrameColor = ConstantValues.AppRedColor;


                                        IsFirstStepCompleted = true;
                                        IsSecoundStepCompleted = true;
                                        IsThirdStepCompleted = true;
                                        IsFourthStepCompleted = true;
                                        IsFifthStepCompleted = true;
                                        IsSixthStepCompleted = true;
                                        IsSeventhStepDeclined = true;


                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = false;
                                        IsSecoundStepPending = false;
                                        IsThirdStepPending = false;
                                        IsFourthStepPending = false;
                                        IsFifthStepPending = false;
                                        IsSixthStepPending = false;
                                        IsSeventhStepPending = false;
                                        break;
                                    default:
                                        FirstColSpan = 1;
                                        SecoundColSpan = 13;
                                        SecoundColStart = 1;
                                        IsShowHireNegotiator = false;
                                        HireNegotiatorBtnText = string.Empty;
                                        FirstColFrameColor = ConstantValues.AppLightGrayColor;
                                        SecoundColFrameColor = ConstantValues.AppLightGrayColor;
                                        ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                        FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                        FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                        SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                        IsFirstStepCompleted = false;
                                        IsSecoundStepCompleted = false;
                                        IsThirdStepCompleted = false;
                                        IsFourthStepCompleted = false;
                                        IsFifthStepCompleted = false;
                                        IsSixthStepCompleted = false;
                                        IsSeventhStepCompleted = false;

                                        IsFirstStepInProcessing = false;
                                        IsSecoundStepInProcessing = false;
                                        IsThirdStepInProcessing = false;
                                        IsFourthStepInProcessing = false;
                                        IsFifthStepInProcessing = false;
                                        IsSixthStepInProcessing = false;
                                        IsSeventhStepInProcessing = false;

                                        IsFirstStepPending = true;
                                        IsSecoundStepPending = true;
                                        IsThirdStepPending = true;
                                        IsFourthStepPending = true;
                                        IsFifthStepPending = true;
                                        IsSixthStepPending = true;
                                        IsSeventhStepPending = true;
                                        break;
                                }
                                IsLoader = false;
                            }
                            else
                            {
                                FirstColSpan = 1;
                                SecoundColSpan = 13;
                                SecoundColStart = 1;
                                IsShowHireNegotiator = false;
                                HireNegotiatorBtnText = string.Empty;
                                FirstColFrameColor = ConstantValues.AppLightGrayColor;
                                SecoundColFrameColor = ConstantValues.AppLightGrayColor;
                                ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                IsFirstStepCompleted = false;
                                IsSecoundStepCompleted = false;
                                IsThirdStepCompleted = false;
                                IsFourthStepCompleted = false;
                                IsFifthStepCompleted = false;
                                IsSixthStepCompleted = false;
                                IsSeventhStepCompleted = false;


                                IsFirstStepInProcessing = false;
                                IsSecoundStepInProcessing = false;
                                IsThirdStepInProcessing = false;
                                IsFourthStepInProcessing = false;
                                IsFifthStepInProcessing = false;
                                IsSixthStepInProcessing = false;
                                IsSeventhStepInProcessing = false;

                                IsFirstStepPending = true;
                                IsSecoundStepPending = true;
                                IsThirdStepPending = true;
                                IsFourthStepPending = true;
                                IsFifthStepPending = true;
                                IsSixthStepPending = true;
                                IsSeventhStepPending = true;
                            }
                        }
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                IsLoader = false;
                LogManager.TraceErrorLog(ex);

            }
            finally
            {
                IsLoader = false;
            }
        }

        /// <summary>
        /// This popup for Approve Qbid by Member
        /// </summary>
        public async void ApprovePopUp()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    var value = QBidHelper.AccceptQuotationStatus;

                    switch (value)
                    {

                        case true:
                            IsLoader = true;
                            approveQuotationRequest.facilityId =  QBidHelper.FacilityId;  
                            approveQuotationRequest.quotationId = Convert.ToString(QBidHelper.QuotationId);
                            approveQuotationRequest.items = ServiceItemId != null ? ServiceItemId.Select(a => new items
                            {
                                itemId = a
                            }).ToList() : null;
                            var approveQBidByMember = await apiServices.ApproveQuotationByMember(approveQuotationRequest).ConfigureAwait(false);
                            if(approveQBidByMember !=null)
                            {
                                if (approveQBidByMember.code == (int)HttpStatusCode.OK)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                       
                                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(ApprovePopUpOk, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, approveQBidByMember.message.ToString() , false)).ConfigureAwait(true);
                                    });
                                    IsShowHide = false;
                                }
                                else
                                {
                                    var err = string.Empty;
                                    if (Convert.ToString(approveQBidByMember.message).ToLower() == "failed")
                                    {
                                        err = Convert.ToString(approveQBidByMember.error);
                                    }
                                    Device.BeginInvokeOnMainThread( () =>
                                    {
                                        DependencyService.Get<IToastMessage>().LongAlert(approveQBidByMember.message.ToString() + "\n" + err);
                                    });
                                }
                            }
                            break;
                        case false:
                            break;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }


        /// <summary>
        /// This popup for approve Quotaion by member confirmation
        /// </summary>
        public async void ApprovePopUpOk()
        {
            try
            {
                
                var value = QBidHelper.AccceptQuotationStatus;
                switch (value)
                {
                    case true:                       
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(RattingPopUpOk, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.ConfirmToApprovePriceMessage, false)).ConfigureAwait(true);                      
                        break;
                    case false:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }


        /// <summary>
        /// This popup for give ratting to negotiator
        /// </summary>
       
        public async void RattingPopUpOk()
        {
            try
            {
                IsLoader = true;
                var value = QBidHelper.AccceptQuotationStatus;
                switch (value)
                {
                    case true:
                    await GetQBidDetails().ConfigureAwait(true);
                    await GetQBidStatus().ConfigureAwait(true);
                        IsShowHide = false;

                        if(ShowTransferAmountMessage==true)
                        {
                          await  App.Current.MainPage.DisplayAlert(string.Empty, ResourceValues.ApproveMinimumChargeMessage, ResourceValues.OkButtontext).ConfigureAwait(true);
                        }
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new RateNowPopupView()).ConfigureAwait(true);
                        break;
                    case false:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }



        /// <summary>
        /// This popup for Decline Qbid 
        /// </summary>
        public async void DeclinedPopUp()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    declineQuotationRequest = new DeclinedQuotationRequest();
                    var value = QBidHelper.AccceptQuotationStatus;

                    switch (value)
                    {
                        case true:

                            IsLoader = true;
                            declineQuotationRequest.facilityId = QBidHelper.FacilityId; 
                            declineQuotationRequest.negotiatorId = QBidHelper.NegotiatorId.ToString();
                            declineQuotationRequest.quotationId = QBidHelper.QuotationId.ToString();

                            var declineQBidByMember = await apiServices.DeclinedQuotationByMember(declineQuotationRequest).ConfigureAwait(false);
                            if (declineQBidByMember !=null)
                            {
                                if (declineQBidByMember.code == (int)HttpStatusCode.OK)
                                {
                                    
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(DeclinedPopUpOk, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, declineQBidByMember.message.ToString(), false)).ConfigureAwait(true);
                                    });
                                }
                                else
                                {
                                    var err = string.Empty;
                                    if (Convert.ToString(declineQBidByMember.message).ToLower() == "failed")
                                    {
                                        err = Convert.ToString(declineQBidByMember.error);
                                    }
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DependencyService.Get<IToastMessage>().LongAlert(declineQBidByMember.message.ToString() + "\n" + err);
                                    });
                                }
                            }
                           
                            break;
                        case false:
                            break;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }

        /// <summary>
        /// This popup for Decline Qbid comfirmaton
        /// </summary>
       
        public  void DeclinedPopUpOk()
        {
            try
            {
                IsLoader = true;
                var value = QBidHelper.AccceptQuotationStatus;
                switch (value)
                {
                    case true:
                        GetQBidDetails();
                        GetQBidStatus();
                        IsShowHide = false;

                        break;
                    case false:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }

        /// <summary>
        /// This popup for Change negotiator 
        /// </summary>
        public  void ChangeNegoPopUp()
        {
            try
            {
                var value = QBidHelper.AccceptQuotationStatus;
                switch (value)
                {
                    case true:
                        var result = ResourceValues.ConfirmBlockNegotiatorMessage;
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(BlockNegoPopUpOK, ResourceValues.YesButtontext, ResourceValues.NoButtontext, result.ToString(), true)).ConfigureAwait(true);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }

        }


     /// <summary>
     /// This popup for block negotiator confirmation
     /// </summary>
        public async void BlockNegoPopUpOK()
        {
            try
            {
                var value = QBidHelper.AccceptQuotationStatus;
                switch (value)
                {
                    case true:
                        No = false;
                        Yes = true;
                        await NegotiatiorDetails().ConfigureAwait(true);
                        break;
                    case false:
                        Yes = false;
                        No = true;
                        await NegotiatiorDetails().ConfigureAwait(true);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }

        }

        #endregion
    }
}
