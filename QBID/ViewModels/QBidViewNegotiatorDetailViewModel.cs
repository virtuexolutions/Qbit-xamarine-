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
using QBid.Models.APIResponse;
using QBid.DependencyServices;
using Rg.Plugins.Popup.Services;
using QBid.Views.ContentViews;
using QBid.QBidResource;

namespace QBid.ViewModels
{
    public class QBidViewNegotiatorDetailViewModel : BindableObject
    {
        APIService apiServices = new APIService();
        public string contactNoMobile = string.Empty;
        public string contactNoText = string.Empty;
        #region Constructor
        public QBidViewNegotiatorDetailViewModel()
        {
            try
            {
               
                IsLoader = true;               
                Task.Run(() =>
                {                           
                   GetQBidStatus();
                });
                  
                LableText = ResourceValues.TitleVendorLabel;
                LablText = ResourceValues.TitleQbidMemberLabel;
                lblText = ResourceValues.TitleServiceItemLabel;
                 GetQBidDetails();

                
                TotalSaving = ConstantValues.CurencySymbal + ConstantValues.DefaultValue;// "$0.00";

                    MessagingCenter.Subscribe<ManageServicePrice>(this, "ServicePrice", (value) =>
                    {
                        if (QuatationDetail != null)
                        {
                            foreach (var item in QuatationDetail.ServiceItem.Where(a => a.Index == value.Index))
                            {
                                item.NegotiablePrice = Convert.ToString(value.Price);
                            }

                            TotalOriginalPrice = ConstantValues.CurencySymbal + Convert.ToDecimal(QuatationDetail.ServiceItem.Where(a => !String.IsNullOrEmpty(a.ServcieItemPrice)).Sum(a => Convert.ToDecimal(a.ServcieItemPrice)).ToString("0.00"));
                            TotalNegotiatedPrice = ConstantValues.CurencySymbal + Convert.ToDecimal((QuatationDetail.ServiceItem.Where(a => !String.IsNullOrEmpty(a.NegotiablePrice)).Sum(a => Convert.ToDecimal(a.NegotiablePrice)).ToString("0.00")));
                            TotalSaving = ConstantValues.CurencySymbal + Convert.ToDecimal(Convert.ToDecimal(QuatationDetail.ServiceItem.Where(a => !String.IsNullOrEmpty(a.ServcieItemPrice)).Sum(a => Convert.ToDecimal(a.ServcieItemPrice))) - Convert.ToDecimal((QuatationDetail.ServiceItem.Where(a => !String.IsNullOrEmpty(a.NegotiablePrice)).Sum(a => Convert.ToDecimal(a.NegotiablePrice))))).ToString(ConstantValues.DefaultValue);
                          
                        }
                    });
                
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

        public QBidViewNegotiatorDetailViewModel(decimal Price)
        {

            TotalPrice = Price;
        }

        #endregion

        #region Property            

        private string specialNote;
        /// <summary>
        /// Property for SpecialNote
        /// </summary>
        public string SpecialNote
        {
            get { return specialNote; }
            set { specialNote = value; OnPropertyChanged(nameof(SpecialNote)); }
        }

        private string specialNoteEntry;
        /// <summary>
        /// Property for Special Note Entry
        /// </summary>
        public string SpecialNoteEntry
        {
            get { return specialNoteEntry; }
            set { specialNoteEntry = value; OnPropertyChanged(nameof(SpecialNoteEntry)); }
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

        private string benefitsEntry;
        /// <summary>
        /// Property for Benefits  Entry
        /// </summary>
        public string BenefitsEntry
        {
            get { return benefitsEntry; }
            set { benefitsEntry = value; OnPropertyChanged(nameof(BenefitsEntry)); }
        }
       
        private string lableText;
        /// <summary>
        /// Property for lableText
        /// </summary>
        public string LableText
        {
            get { return lableText; }
            set { lableText = value; OnPropertyChanged(nameof(LableText)); }
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
        public ObservableCollection<ServiceItemHeaderModel> StackLayOutList { get; set; } = new ObservableCollection<ServiceItemHeaderModel>();

        private ObservableCollection<NegotiatorQBidDetails> qBidDetails;
        /// <summary>
        ///  Property for qBidDetails
        /// </summary>
        public ObservableCollection<NegotiatorQBidDetails> QBidDetails
        {
            get { return qBidDetails; }
            set { qBidDetails = value; OnPropertyChanged(nameof(QBidDetails)); }
        }

        private QuotationList quatationDetail;
        /// <summary>
        /// Property for quatationDetail
        /// </summary>
        public QuotationList QuatationDetail
        {
            get { return quatationDetail; }
            set { quatationDetail = value; OnPropertyChanged(nameof(QuatationDetail)); }
        }
        private bool isArrowShowHide;
        /// <summary>
        /// Property for show hide arrow option
        /// </summary>
        public bool IsArrowShowHide
        {
            get { return isArrowShowHide; }
            set { isArrowShowHide = value; OnPropertyChanged(nameof(IsArrowShowHide)); }
        }

        private string yes;
        /// <summary>
        /// property for yes
        /// </summary>

        public string Yes
        {
            get { return yes; }
            set { yes = value; OnPropertyChanged(nameof(Yes)); }
        }

        private string no;
        /// <summary>
        /// property for no
        /// </summary>
        public string No
        {
            get { return no; }
            set { no = value; OnPropertyChanged(nameof(No)); }
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

        private string negotiatedPrice;
        /// <summary>
        /// Property for NegotiatedPrice
        /// </summary>
        public string NegotiatedPrice
        {
            get { return negotiatedPrice; }
            set { negotiatedPrice = value; OnPropertyChanged(nameof(NegotiatedPrice)); }
        }

        private bool isEarningShowHide;
        /// <summary>
        /// Property for NegotiatedPrice
        /// </summary>
        public bool IsEarningShowHide
        {
            get { return isEarningShowHide; }
            set { isEarningShowHide = value; OnPropertyChanged(nameof(IsEarningShowHide)); }
        }

        private string negotiatedPriceErrorMessage;
        /// <summary>
        /// Property for NegotiatedPriceErrorMessage
        /// </summary>
        public string NegotiatedPriceErrorMessage
        {
            get { return negotiatedPriceErrorMessage; }
            set { negotiatedPriceErrorMessage = value; OnPropertyChanged(nameof(NegotiatedPriceErrorMessage)); }
        }


        private bool isVisibleNegotiatedPriceError;
        /// <summary>
        /// Property for IsVisibleNegotiatedPriceError
        /// </summary>
        public bool IsVisibleNegotiatedPriceError
        {
            get { return isVisibleNegotiatedPriceError; }
            set { isVisibleNegotiatedPriceError = value; OnPropertyChanged(nameof(IsVisibleNegotiatedPriceError)); }
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
        /// Property for isVisible Facility Name
        /// </summary>
        public bool IsVisibleFacilityName
        {
            get { return isVisibleFacilityName; }
            set { isVisibleFacilityName = value; OnPropertyChanged(nameof(IsVisibleFacilityName)); }
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
        /// Property for isVisible Facility Address
        /// </summary>
        public bool IsVisibleFacilityAddress
        {
            get { return isVisibleFacilityAddress; }
            set { isVisibleFacilityAddress = value; OnPropertyChanged(nameof(IsVisibleFacilityAddress)); }
        }
        private bool isVisibleFacilityContact;
        /// <summary>
        /// Property for isVisible Facility Contact
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

        private string serviceName;
        /// <summary>
        /// Property for service Name
        /// </summary>
        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; OnPropertyChanged(nameof(ServiceName)); }
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
        /// Property for negotiatorContact for mobile
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


        private bool isAcceptedByMember;
        /// <summary>
        /// Property for isAcceptedByMember
        /// </summary>
        public bool IsAcceptedByMember
        {
            get { return isAcceptedByMember; }
            set { isAcceptedByMember = value; OnPropertyChanged(nameof(IsAcceptedByMember)); }
        }

        private decimal totalPrice;
        /// <summary>
        /// Property for totalPrice
        /// </summary>
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        private bool isVisibleNegotiatedPricePopup;
        /// <summary>
        /// Property for isVisibleNegotiatedPricePopup
        /// </summary>
        public bool IsVisibleNegotiatedPricePopup
        {
            get { return isVisibleNegotiatedPricePopup; }
            set { isVisibleNegotiatedPricePopup = value; OnPropertyChanged(nameof(IsVisibleNegotiatedPricePopup)); }
        }

        //private bool isNegotiatedPriceAvailable;
        ///// <summary>
        ///// Property for isNegotiatedPriceAvailable
        ///// </summary>
        //public bool IsNegotiatedPriceAvailable
        //{
        //    get { return isNegotiatedPriceAvailable; }
        //    set { isNegotiatedPriceAvailable = value; OnPropertyChanged(nameof(IsNegotiatedPriceAvailable)); }
        //}

        private bool isPriceSubmitButtonVisible;
        /// <summary>
        /// Property for Price Submit Button Visible
        /// </summary>
        public bool IsPriceSubmitButtonVisible
        {
            get { return isPriceSubmitButtonVisible; }
            set { isPriceSubmitButtonVisible = value; OnPropertyChanged(nameof(IsPriceSubmitButtonVisible)); }
        }


        private bool isApprovedDeclineStatusVisible;
        /// <summary>
        /// Property for isApprovedDeclineStatusVisible
        /// </summary>
        public bool IsApprovedDeclineStatusVisible
        {
            get { return isApprovedDeclineStatusVisible; }
            set { isApprovedDeclineStatusVisible = value; OnPropertyChanged(nameof(IsApprovedDeclineStatusVisible)); }
        }

        private string approvedDeclineStatus;
        /// <summary>
        /// Property for approvedDeclineStatus
        /// </summary>
        public string ApprovedDeclineStatus
        {
            get { return approvedDeclineStatus; }
            set { approvedDeclineStatus = value; OnPropertyChanged(nameof(ApprovedDeclineStatus)); }
        }

        private string totalSaving;
        /// <summary>
        /// Property for totalSaving
        /// </summary>
        public string TotalSaving
        {
            get { return totalSaving; }
            set { totalSaving = value; OnPropertyChanged(nameof(TotalSaving)); }
        }

        private string totalEarning;
        /// <summary>
        /// Property for total earning by negotiator
        /// </summary>
        public string TotalEarning
        {
            get { return totalEarning; }
            set { totalEarning = value; OnPropertyChanged(nameof(TotalEarning)); }
        }

        private string totalOriginalPrice;
        /// <summary>
        /// Property for TotalOriginalPrice
        /// </summary>
        public string TotalOriginalPrice
        {
            get { return totalOriginalPrice; }
            set { totalOriginalPrice = value; OnPropertyChanged(nameof(TotalOriginalPrice)); }
        }

        private string totalNegotiatedPrice;
        /// <summary>
        /// Property for TotalNegotiatedPrice
        /// </summary>
        public string TotalNegotiatedPrice
        {
            get { return totalNegotiatedPrice; }
            set { totalNegotiatedPrice = value; OnPropertyChanged(nameof(TotalNegotiatedPrice)); }
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
        private bool isPopupForNoteandBenefits;
        /// <summary>
        /// property for Popup For Note and Benefits
        /// </summary>
        public bool IsPopupForNoteandBenefits
        {
            get { return isPopupForNoteandBenefits; }
            set { isPopupForNoteandBenefits = value; OnPropertyChanged(nameof(IsPopupForNoteandBenefits)); }
        }

        private bool isPopupForNoteandBenefitsEntryPopup;
        /// <summary>
        /// property  for show Note and Benefits Entry Popup
        /// </summary>
        public bool IsPopupForNoteandBenefitsEntryPopup
        {
            get { return isPopupForNoteandBenefitsEntryPopup; }
            set { isPopupForNoteandBenefitsEntryPopup = value;OnPropertyChanged(nameof(IsPopupForNoteandBenefitsEntryPopup)); }
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

        #region Command


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

                            //
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

        public  void SubmitPricePopUp()
        {
            var value = QBidHelper.AccceptQuotationStatus;
            switch (value)
            {
                case true:
                    SubmitPrice();
                    break;
                case false:
                    break;
            }
        }

        public  void SubmitQuotationPopUp()
        {
            var value = QBidHelper.AccceptQuotationStatus;
            switch (value)
            {
                case true:
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await GetQBidDetails().ConfigureAwait(true);
                        await GetQBidStatus().ConfigureAwait(true);
                    });

                    break;
                case false:
                    break;
            }
        }


        private Command commandOnSubmit;
        /// <summary>
        /// This command use for process CommandOnSubmit
        /// </summary>
        public Command CommandOnSubmit
        {
            get
            {
                if (commandOnSubmit == null)
                {
                    commandOnSubmit = new Command(async () =>
                    {
                        try
                        {
                            

                            if (QuatationDetail.ServiceItem.Any(a => !String.IsNullOrEmpty(a.NegotiablePrice) && (Convert.ToDecimal(a.ServcieItemPrice) >= Convert.ToDecimal(a.NegotiablePrice.ToString()))))
                            {

                                IsPopupForNoteandBenefitsEntryPopup = true;
                                //  await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(SubmitPricePopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.ConfirmSubmitNegoPriceMessage, true)).ConfigureAwait(true);

                            }
                            else
                            {
                                App.Current.MainPage.DisplayAlert("Alert", ResourceValues.VailidNegoPriceMessage, "OK");
                            }
                            //else
                            //{
                            //    Device.BeginInvokeOnMainThread(() =>
                            //    { 
                            //    DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.VailidNegoPriceMessage);

                            //    });
                            //}
                            //IsLoader = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnSubmit;
            }
        }

        private Command commandForNegotiatedPriceAccept;
        /// <summary>
        ///  Command for on commandForNegotiatedPriceAccept 
        /// </summary>
        public Command CommandForNegotiatedPriceAccept
        {
            get
            {
                if (commandForNegotiatedPriceAccept == null)
                {
                    commandForNegotiatedPriceAccept = new Command((negotiatorPrice) =>
                    {
                        try
                        {

                            var value = negotiatorPrice;
                            switch (value)
                            {
                                case "0":
                                    Yes = "Yes";
                                    SubmitPrice();
                                    IsVisibleNegotiatedPricePopup = false;
                                    break;
                                case "1":
                                    No = "No";
                                    IsVisibleNegotiatedPricePopup = false;
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandForNegotiatedPriceAccept;
            }
        }


        private Command commandOnBackForService;
        /// <summary>
        /// This command use for On Back For Service
        /// </summary>
        public Command CommandOnBackForService
        {
            get
            {
                if (commandOnBackForService == null)
                {
                    commandOnBackForService = new Command(async () =>
                    {
                        try
                        {
                            if (IsLoader == false)
                            {
                                await App.Current.MainPage.Navigation.PopAsync();
                            }

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnBackForService;
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
                                //PhoneDialer.Open(FacilityContact);
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
                                //PhoneDialer.Open(NegotiatorContact);
                            }
                            if (!string.IsNullOrEmpty(NegotiatorContactText))
                            {
                                contactNoText = NegotiatorContactText;
                                IsCallorSmsShow = true;
                                //PhoneDialer.Open(NegotiatorContact);
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
                                        App.Current.MainPage.DisplayAlert(string.Empty,  ResourceValues.UpdateCallNoMessage, ResourceValues.OkButtontext);
                                    }
                                    break;

                                case ConstantValues.Two:
                                    if (!string.IsNullOrEmpty(contactNoText))
                                    {
                                        Launcher.OpenAsync(new Uri(String.Format("sms:{0}", contactNoText)));
                                        //Launcher.OpenAsync(new Uri(String.Format("sms:{0}?body={1}", contactNo, string.Empty)));
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

        private Command commandOnCancel;

        public Command CommandOnCancel
        {
            get
            {
                if(commandOnCancel == null)
                {
                    commandOnCancel = new Command(() =>
                    {
                        IsPopupForNoteandBenefitsEntryPopup = false;
                    });

                }
                return commandOnCancel;
            }
        }

        private Command commandOnSaveData;
        /// <summary>
        ///  Command for on Close Special Notes abd Benefit 
        /// </summary>
        public Command CommandOnSaveData
        {
            get
            {
                if (commandOnSaveData == null)
                {
                    commandOnSaveData = new Command(() =>
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(SpecialNoteEntry) && !string.IsNullOrEmpty(BenefitsEntry))
                                App.Current.MainPage.DisplayAlert("Alert", "Negotiator please add any Special Notes or Coverages from the Vendor/Salesperson that may relate to the project/repairs. Such as may cost more due to unseen or expected circumstances to the project that could not be foreseen over a phone call.", "OK");                            
                            else if (!string.IsNullOrEmpty(SpecialNoteEntry) && string.IsNullOrEmpty(BenefitsEntry))
                                App.Current.MainPage.DisplayAlert("Alert", "Negotiator please add any Benefits or Coverages from the Vendor/Salesperson that may relate to the project/repairs. Such as warranties, disclaimers or any other information that benefits the consumer.", "OK");
                            else if(string.IsNullOrEmpty(SpecialNoteEntry) && string.IsNullOrEmpty(BenefitsEntry))
                                App.Current.MainPage.DisplayAlert("Alert", "Pleaes fill Special Notes and Benefits.", "OK");
                            else
                            {
                                IsPopupForNoteandBenefitsEntryPopup = false;
                                PopupNavigation.Instance.PushAsync(new PopUp(SubmitPricePopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.ConfirmSubmitNegoPriceMessage, true)).ConfigureAwait(true);
                               
                            }


                          
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnSaveData;
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
        


        #endregion

        #region Method
        /// <summary>
        /// Method for submitQuotationByNegotiatorRequest
        /// </summary>
        public void SubmitPrice()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoader = true;
                });

                    if (QuatationDetail.ServiceItem.All(a => !String.IsNullOrEmpty(a.NegotiablePrice)))
                    {

                        if (QuatationDetail.ServiceItem.All(a => Convert.ToDecimal(a.ServcieItemPrice) >= Convert.ToDecimal(Convert.ToString(a.NegotiablePrice))))
                        {

                            SubmitQuotationByNegotiatorRequest submitQuotationByNegotiatorRequest = new SubmitQuotationByNegotiatorRequest();
                            NegotiatedPrice negotiatedPrice = new NegotiatedPrice();
                            List<NegotiatedPrice> lstNegotiatedPrice = new List<NegotiatedPrice>();

                            foreach (var item in QuatationDetail.ServiceItem)
                            {
                                negotiatedPrice = new NegotiatedPrice();
                                negotiatedPrice.itemId = Convert.ToString(item.ServiceItemId);
                                negotiatedPrice.newPrice = item.NegotiablePrice;                               
                                lstNegotiatedPrice.Add(negotiatedPrice);
                            }
                            submitQuotationByNegotiatorRequest.quotationId = Convert.ToString(QuatationDetail.QuotationId);
                            submitQuotationByNegotiatorRequest.facilityId = QuatationDetail.ServiceItem.Select(a => Convert.ToString(a.FacilityId)).FirstOrDefault();
                            submitQuotationByNegotiatorRequest.negotiatedPrice = lstNegotiatedPrice;
                            submitQuotationByNegotiatorRequest.specialNote = SpecialNoteEntry;
                            submitQuotationByNegotiatorRequest.benefitNote = BenefitsEntry;
                            SubmitQuotationByNegotiator(submitQuotationByNegotiatorRequest);

                        }
                        else
                        {

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                IsLoader = false;
                                DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.VailidNegoPriceMessage);
                            });
                        }

                    }
                    else
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoader = false;
                            DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.VailidNegoPriceMessage);
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        IsLoader = false;
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                    //    IsLoader = false;
                       
                    //});
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

        public bool IsVaild()
        {
            return false;
        }

        /// <summary>
        /// method for bind Quotation list
        /// </summary>
        /// <param name="item"></param>
        public void BindObject(QuotationList item)
        {
            try
            {
               
                    IsLoader = true;                
                    IsEarningShowHide = false;
                    StackLayOutList.Clear();
                    ServiceItemHeaderModel serviceItemHeaderModel = new ServiceItemHeaderModel();
                    StackLayout stackLayout = new StackLayout();
                    if (!string.IsNullOrEmpty(item.FacilityName))
                    {
                        IsRecordsAvailable = true;
                        IsRecordNotAvail = false;
                        RecordNotFound = string.Empty;
                        FacilityName = item.FacilityName;
                        FacilityRepresentative = item.FacilityAdvisor; //QBidHelper.AdviserName;
                        FacilityEmail = item.FacilityEmail;
                        FacilityAddress = item.FacilityAddress;
                        FacilityContact = item.FacilityContact;
                        ServiceName = item.serviceName;
                        IsVisibleFacilityContact = !String.IsNullOrEmpty(item.FacilityContact);
                        IsVisibleFacilityName = !String.IsNullOrEmpty(item.FacilityName);
                        IsVisiblefacilityRepresentative = !String.IsNullOrEmpty(item.FacilityAdvisor);
                        IsVisibleFacilityEmail = !String.IsNullOrEmpty(item.FacilityEmail);
                        IsVisibleFacilityAddress = !String.IsNullOrEmpty(item.FacilityAddress);
                    if(! string.IsNullOrEmpty(item.approvedChargeToUser))
                    {
                        ShowTransferAmountMessage = (Convert.ToDecimal(item.approvedChargeToUser) > Convert.ToDecimal("0.49")) ? false : true;
                    }
                    else
                    {
                        ShowTransferAmountMessage = false;
                    }
                        
                    SpecialNote = (item.SpecialNote != null) ? item.SpecialNote : ResourceValues.TextNothingToShow;                 
                    Benefits = (item.Benefits != null) ? item.Benefits : ResourceValues.TextNothingToShow;
                 
                    var qBidMsg = item.QbidCurrentStatus.Where(res => res.currentStatus == true && res.statusId == 7 || res.statusId == 8).FirstOrDefault();
                        if (qBidMsg != null)
                        {
                            switch (qBidMsg.statusId)
                            {
                                case 7:
                                    QBidStatusMessage = ResourceValues.StatusDeclineQBid;
                                    QBidStatusColor = ConstantValues.AppRedColor;
                                    break;
                                case 8:
                                    QBidStatusMessage = ResourceValues.StatusApprovedQBId;
                                    QBidStatusColor = ConstantValues.AppColor;
                                    break;
                                default:
                                    QBidStatusMessage = string.Empty;
                                    break;
                            }
                        }
                        var chkApproved = item.QbidCurrentStatus.Any(res => res.statusId > 4);
                        NegotiatorDetails NegoDetails = new NegotiatorDetails();
                        if ((item.NegotiatorDetails != null) && (item.NegotiatorDetails.Any()))
                        {
                            IsNegotiatorAvailble = true;
                            NegoDetails = item.NegotiatorDetails.Where(res => res.ActiveNegotiator).FirstOrDefault();
                        }
                        UserDetailsInfo userDetailsInfo = new UserDetailsInfo();
                        if ((item.UserDetails != null) && (item.UserDetails.Any()))
                        {
                            IsNegotiatorAvailble = true;
                            userDetailsInfo = item.UserDetails.FirstOrDefault();
                            if (userDetailsInfo != null)
                            {
                                NegotiatorName = userDetailsInfo.firstName + userDetailsInfo.lastName;
                                NegotiatorEmail = userDetailsInfo.userEmail;
                                NegotiatorAddress = userDetailsInfo.addressLine1 + ConstantValues.OneSpace + userDetailsInfo.addressLine2;
                                NegotiatorContactMobile = userDetailsInfo.mobileCall;
                                NegotiatorContactText = userDetailsInfo.mobileText;
                        }
                            else
                            {
                                IsNegotiatorAvailble = false;
                            }
                        }
                        else
                        {
                            IsNegotiatorAvailble = false;
                        }
                        if ((item.ServiceItem != null) && (item.ServiceItem.Any()))
                        {

                            int i = 0;
                            var negotiatedPrice = string.Empty;
                            var serviceItemOEM = string.Empty;
                            var serviceItemOEMVisible = false;
                            bool itemChecked = false;
                            bool isMeamberApprovedPrice = false;

                            if (item.ServiceItem.Any(res => res.Price.Count() > 0))
                            {
                                itemChecked = isMeamberApprovedPrice = item.ServiceItem.Any(res => res.Price[0].Approved);

                            }
                            var chkOEM = item.ServiceItem.Any(res => Convert.ToBoolean(Convert.ToInt32(res.OEM)));
                            Views.ContentViews.OriginalFacilityView originalFacilityContentView = new Views.ContentViews.OriginalFacilityView("Service", "Original Price", "OEM", chkOEM, "Negotiated Price", itemChecked);
                            stackLayout.Children.Add(originalFacilityContentView);
                            stackLayout.IsClippedToBounds = true;
                            serviceItemHeaderModel.StackLayOutHeaderList.Children.Add(stackLayout);
                            foreach (var serviceItem in item.ServiceItem)
                            {
                                bool IsNegotiatorPriceSubmitted = false;
                                if (serviceItem.Price.Count > 0)
                                {
                                    itemChecked = serviceItem.Price.Any(res => (res.NegotiatorId == NegoDetails.NegotiatorId) && res.Approved);
                                    IsNegotiatorPriceSubmitted = serviceItem.Price.Any(res => (res.NegotiatorId == NegoDetails.NegotiatorId));
                                    negotiatedPrice = Convert.ToDecimal(!string.IsNullOrEmpty(serviceItem.Price[0].NegotiatedPrice.ToString())) > 0 ? serviceItem.Price[0].NegotiatedPrice.ToString() : ConstantValues.DefaultValue;
                                    serviceItem.NegotiablePrice = Convert.ToDecimal(!string.IsNullOrEmpty(serviceItem.Price[0].NegotiatedPrice.ToString())) > 0 ? serviceItem.Price[0].NegotiatedPrice.ToString() : ConstantValues.DefaultValue;
                                    if (isMeamberApprovedPrice)
                                    {
                                        TotalSaving = ConstantValues.CurencySymbal + (Convert.ToDecimal(item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.ServcieItemPrice) && a.Price[0].Approved).Sum(a => Convert.ToDecimal(a.ServcieItemPrice))) - Convert.ToDecimal((item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.NegotiablePrice) && a.Price[0].Approved).Sum(a => Convert.ToDecimal(a.NegotiablePrice))))).ToString(ConstantValues.DefaultValue);

                                    }
                                    else
                                    {
                                        TotalSaving = ConstantValues.CurencySymbal + (Convert.ToDecimal(item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.ServcieItemPrice)).Sum(a => Convert.ToDecimal(a.ServcieItemPrice))) - Convert.ToDecimal((item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.NegotiablePrice)).Sum(a => Convert.ToDecimal(a.NegotiablePrice))))).ToString(ConstantValues.DefaultValue);
                                    }                                   
                                }
                                else
                                {
                                  
                                    negotiatedPrice = ConstantValues.DefaultValue;  //"0.00"
                                }
                                serviceItemOEM = Convert.ToInt32(serviceItem.OEM) > 0 ? ResourceValues.YesButtontext : ResourceValues.NoButtontext;
                                serviceItemOEMVisible = Convert.ToBoolean(Convert.ToInt32(serviceItem.OEM)) ? true : false;
                                Views.ContentViews.OriginalFacilityNegotiatorView originalFacilityNegotiatorView = new Views.ContentViews.OriginalFacilityNegotiatorView(serviceItem.ServcieItemName, Convert.ToDecimal(serviceItem.ServcieItemPrice).ToString(ConstantValues.DefaultValue), serviceItemOEM, serviceItemOEMVisible, itemChecked, i, (serviceItem.Index != null ? true : false), Convert.ToDecimal(negotiatedPrice).ToString(ConstantValues.DefaultValue), IsNegotiatorPriceSubmitted, isMeamberApprovedPrice);
                                stackLayout.Children.Add(originalFacilityNegotiatorView);
                                serviceItem.Index = i;
                                i++;
                            }
                            if (isMeamberApprovedPrice)
                            {

                                if (!string.IsNullOrEmpty(item.negotiatorEarning))
                                {
                                    TotalEarning = ConstantValues.CurencySymbal + Convert.ToDecimal(item.negotiatorEarning).ToString(ConstantValues.DefaultValue);
                                }
                                else
                                {
                                    TotalEarning = ConstantValues.CurencySymbal + ConstantValues.DefaultValue;
                                }
                                TotalOriginalPrice = ConstantValues.CurencySymbal + Convert.ToDecimal(item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.ServcieItemPrice) && a.Price[0].Approved).Sum(a => Convert.ToDecimal(a.ServcieItemPrice)).ToString(ConstantValues.DefaultValue));
                                TotalNegotiatedPrice = ConstantValues.CurencySymbal + Convert.ToDecimal((item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.NegotiablePrice) && a.Price[0].Approved).Sum(a => Convert.ToDecimal(a.NegotiablePrice)).ToString(ConstantValues.DefaultValue)));
                                IsEarningShowHide = true;
                            }
                            else
                            {
                                TotalOriginalPrice = ConstantValues.CurencySymbal + Convert.ToDecimal(item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.ServcieItemPrice)).Sum(a => Convert.ToDecimal(a.ServcieItemPrice)).ToString(ConstantValues.DefaultValue));
                                TotalNegotiatedPrice = ConstantValues.CurencySymbal + Convert.ToDecimal(item.ServiceItem.Where(a => !String.IsNullOrEmpty(a.NegotiablePrice)).Sum(a => Convert.ToDecimal(a.NegotiablePrice)).ToString(ConstantValues.DefaultValue));
                            }
                           
                            stackLayout.IsClippedToBounds = true;
                            serviceItemHeaderModel.StackLayOutHeaderList.Children.Add(stackLayout);
                        }
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            IsLoader = false;
                        });
                    }
                    else
                    {
                        RecordNotFound = ResourceValues.NorecordsfoundMessage;
                    IsRecordsAvailable = false;
                        IsRecordNotAvail = true;
                    }

                    QuatationDetail = item;
                    StackLayOutList.Add(serviceItemHeaderModel);

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
        /// Method for submitQuotationByNegotiatorRequest
        /// </summary>
        /// <returns></returns>
        private async Task SubmitQuotationByNegotiator(SubmitQuotationByNegotiatorRequest submitQuotationByNegotiatorRequest)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    var data = await apiServices.SubmitQuotationNegotiatorDetails(submitQuotationByNegotiatorRequest);
                   if(data !=null)
                    {
                        if (data.code == 200)
                        {
                            IsLoader = false;
                            
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(SubmitQuotationPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, data.message + ConstantValues.OneSpace + ResourceValues.ContactToMember + "\n" + "\n" + ResourceValues.TitleForSubmitPriceMessage, false)).ConfigureAwait(true);
                            IsPriceSubmitButtonVisible = false;  //for submit button hide
                        }
                        else
                        {
                            IsLoader = false;
                            var err = string.Empty;
                            if (Convert.ToString(data.message).ToLower() == "failed")
                            {
                                err = Convert.ToString(data.error);
                            }
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(SubmitQuotationPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, data.message +"\n" + err , false)).ConfigureAwait(true);
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
        /// Method for get Qbid detail
        /// </summary>
        /// <returns></returns>
        private async Task GetQBidDetails()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    IsVisibleNegotiatedPricePopup = false;
                    UtilHelper utilHelper = new UtilHelper();
                    IsLoader = true;
                    var data = await apiServices.GetQBidDetails(QBidHelper.QuotationId);
                    QBidDetails = new ObservableCollection<NegotiatorQBidDetails>();
                    if(data !=null)
                    {
                        if ((data.data != null) && (data.data.Any()))
                        {
                            foreach (var item in data.data)
                            {
                                BindObject(item);
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
        /// Method for get Qbid Status
        /// </summary>
        /// <returns></returns>
        public async Task GetQBidStatus()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        IsLoader = true;
                    });
                    FirstColSpan = 2;
                    SecoundColSpan = 11;
                    SecoundColStart = 2;
                    IsPriceSubmitButtonVisible = false;
                    var qBidStatusRequest = new QBidStatusRequest();
                    qBidStatusRequest.QuotationId = Convert.ToString(QBidHelper.QuotationId);
                    var qBidStatusDetails = await apiServices.UserQuotationQbidStatus(qBidStatusRequest);
                    if (qBidStatusDetails != null )
                    {
                        if (qBidStatusDetails.data.CurrentStatus.Count > 0)
                        {
                            
                            int status = qBidStatusDetails.data.CurrentStatus.Where(sts => sts.currentStatus == true).Select(sts => sts.statusId).FirstOrDefault();
                            
                            switch (status)
                            {
                                case 1:

                                    FirstColSpan = 2;
                                    SecoundColSpan = 11;
                                    SecoundColStart = 2;

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
                                    IsPriceSubmitButtonVisible = true;
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
                        }
                        else
                        {
                            FirstColSpan = 1;
                            SecoundColSpan = 13;
                            SecoundColStart = 1;

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





        #endregion
    }
}
