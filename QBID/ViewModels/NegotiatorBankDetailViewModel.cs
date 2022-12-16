using Plugin.Media;
using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using QBid.Views;
using Stripe;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class NegotiatorBankDetailViewModel : BaseViewModel
    {
        public static bool isFromFileUpload; 
        string accountId;
        string bankId;
        public string SSLNoError;

        public string ssnVerificationDocumentFrontId = string.Empty;
        public string ssnVerificationDocumentBackId = string.Empty;

        public string ssnVerificationDocumentFrontPath = string.Empty;
        public string ssnVerificationDocumentBackPath = string.Empty;

        #region Constructor
        public NegotiatorBankDetailViewModel()
        {
            IsDocumentUploadShow = false;
            IsEnableField = true;
            GetState().ConfigureAwait(false);
        }

        #endregion

        #region Propertys

        private bool isLoader;
        /// <summary>
        /// Property for loader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private int maxLength;
        /// <summary>
        /// Property for max length for SSN no
        /// </summary>
        public int MaxLength
        {
            get { return maxLength; }
            set { maxLength = value; OnPropertyChanged(nameof(MaxLength)); }
        }

        private string titalSSN;
        /// <summary>
        /// Property for label for ssn no
        /// </summary>
        public string TitalSSN
        {
            get { return titalSSN; }
            set { titalSSN = value; OnPropertyChanged(nameof(TitalSSN)); }
        }

        private string placeHolderSSN;
        /// <summary>
        /// Property for place holder for ssn no
        /// </summary>
        public string PlaceHolderSSN
        {
            get { return placeHolderSSN; }
            set { placeHolderSSN = value; OnPropertyChanged(nameof(PlaceHolderSSN)); }
        }

        private string bankAccountStatus;
        /// <summary>
        /// Property for Set bank active status text
        /// </summary>
        public string BankAccountStatus
        {
            get { return bankAccountStatus; }
            set { bankAccountStatus = value; OnPropertyChanged(nameof(BankAccountStatus)); }
        }

        private ImageSource bankAccountStatusImage;
        /// <summary>
        /// Property for Set bank active bank image 
        /// </summary>
        public ImageSource BankAccountStatusImage
        {
            get { return bankAccountStatusImage; }
            set { bankAccountStatusImage = value; OnPropertyChanged(nameof(BankAccountStatusImage)); }
        }

        private string bankAccountStatusColor;
        /// <summary>
        /// Property for Set  active bank label color 
        /// </summary>
        public string BankAccountStatusColor
        {
            get { return bankAccountStatusColor; }
            set { bankAccountStatusColor = value; OnPropertyChanged(nameof(BankAccountStatusColor)); }
        }
        private bool isTextReadOnly;
        /// <summary>
        /// Property for All text read only
        /// </summary>
        public bool IsTextReadOnly
        {
            get { return isTextReadOnly; }
            set { isTextReadOnly = value; OnPropertyChanged(nameof(IsTextReadOnly)); }
        }

        private bool isEnableField;
        /// <summary>
        /// Property for All text read only
        /// </summary>
        public bool IsEnableField
        {
            get { return isEnableField; }
            set { isEnableField = value; OnPropertyChanged(nameof(IsEnableField)); }
        }

        private bool isvisible;
        /// <summary>
        /// Property for field visible
        /// </summary>
        public bool Isvisible
        {
            get { return isvisible; }
            set { isvisible = value; OnPropertyChanged(nameof(Isvisible)); }
        }

        private bool isSSNvisible;
        /// <summary>
        /// Property for for ssn field visible
        /// </summary>
        public bool IsSSNvisible
        {
            get { return isSSNvisible; }
            set { isSSNvisible = value; OnPropertyChanged(nameof(IsSSNvisible)); }
        }
        private bool isDOBvisible;
        /// <summary>
        /// Property for for DOB field visible
        /// </summary>
        public bool IsDOBvisible
        {
            get { return isDOBvisible; }
            set { isDOBvisible = value; OnPropertyChanged(nameof(IsDOBvisible)); }
        }

        private bool isSubmitShow;
        /// <summary>
        /// Property for Submit button Show
        /// </summary>
        public bool IsSubmitShow
        {
            get { return isSubmitShow; }
            set { isSubmitShow = value; OnPropertyChanged(nameof(IsSubmitShow)); }
        }

        private bool isDocumentUploadShow;
        /// <summary>
        /// Property for Ducument upload Show
        /// </summary>
        public bool IsDocumentUploadShow
        {
            get { return isDocumentUploadShow; }
            set { isDocumentUploadShow = value; OnPropertyChanged(nameof(IsDocumentUploadShow)); }
        }


        private Color documentUplodedMessageTextColorBack;
        /// <summary>
        /// Property for Ducument upload message color Show
        /// </summary>
        public Color DocumentUplodedMessageTextColorBack
        {
            get { return documentUplodedMessageTextColorBack; }
            set
            {
                documentUplodedMessageTextColorBack = value; OnPropertyChanged(nameof(DocumentUplodedMessageTextColorBack));
            }
        }

        private Color documentUplodedMessageTextColorFront;
        /// <summary>
        /// Property for Ducument upload message color Show
        /// </summary>
        public Color DocumentUplodedMessageTextColorFront
        {
            get { return documentUplodedMessageTextColorFront; }
            set
            {
                documentUplodedMessageTextColorFront = value; OnPropertyChanged(nameof(DocumentUplodedMessageTextColorFront));
            }
        }

        private bool isEditShow;
        /// <summary>IsEditShow
        /// Property for Edit button Show
        /// </summary>
        public bool IsEditShow
        {
            get { return isEditShow; }
            set { isEditShow = value; OnPropertyChanged(nameof(IsEditShow)); }
        }

        private bool isCancelShow;
        /// <summary>IsEditShow
        /// Property for Cancel button Show
        /// </summary>
        public bool IsCancelShow
        {
            get { return isCancelShow; }
            set { isCancelShow = value; OnPropertyChanged(nameof(IsCancelShow)); }
        }

        private bool isUpdateShow;
        /// <summary>IsEditShow
        /// Property for Update button Show
        /// </summary>
        public bool IsUpdateShow
        {
            get { return isUpdateShow; }
            set { isUpdateShow = value; OnPropertyChanged(nameof(IsUpdateShow)); }
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

        private bool skipShowHide;
        /// <summary>
        /// Property for show hide skip option
        /// </summary>
        public bool SkipShowHide
        {
            get { return skipShowHide; }
            set { skipShowHide = value; OnPropertyChanged(nameof(SkipShowHide)); }
        }

        private bool isShowHide;
        /// <summary>
        /// Property for show hide bank status label
        /// </summary>
        public bool IsShowHide
        {
            get { return isShowHide; }
            set { isShowHide = value; OnPropertyChanged(nameof(IsShowHide)); }
        }

        private string accountHolderName;
        /// <summary>
        /// Property for AccountHolderName
        /// </summary>
        public string AccountHolderName
        {
            get { return accountHolderName; }
            set
            {
                accountHolderName = value;
                if (!string.IsNullOrEmpty(accountHolderName))
                {
                    AccountHolderErrorMessage = string.Empty;
                    IsVisibleAccountHolderNameError = false;
                }
                else
                {
                    AccountHolderErrorMessage = ResourceValues.CardHolderNameErrorMessage;
                    IsVisibleAccountHolderNameError = true;
                }
                OnPropertyChanged(nameof(AccountHolderName));
            }
        }

        private string accountHolderErrorMessage;
        /// <summary>
        /// Property for Account HolderName Error message
        /// </summary>
        public string AccountHolderErrorMessage
        {
            get { return accountHolderErrorMessage; }
            set { accountHolderErrorMessage = value; OnPropertyChanged(nameof(AccountHolderErrorMessage)); }
        }

        private bool isVisibleAccountHolderNameError;
        /// <summary>
        /// Property for AccountHolderNameError
        /// </summary>
        public bool IsVisibleAccountHolderNameError
        {
            get { return isVisibleAccountHolderNameError; }
            set { isVisibleAccountHolderNameError = value; OnPropertyChanged(nameof(IsVisibleAccountHolderNameError)); }
        }

        private DateTime? dob;
        /// <summary>
        /// Property for Date of Birth
        /// </summary>
        public DateTime? DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                if (dob != null)
                {
                    DOBerrorMessage = string.Empty;
                    IsVisibleDOBError = false;
                }
                OnPropertyChanged(nameof(DOB));
            }
        }

        private string dobErrorMessage;
        /// <summary>
        /// Property for Date of Birth Error message
        /// </summary>
        public string DOBerrorMessage
        {
            get { return dobErrorMessage; }
            set { dobErrorMessage = value; OnPropertyChanged(nameof(DOBerrorMessage)); }
        }

        private bool isVisibleDOBError;
        /// <summary>
        /// Property for Date of Birth Error
        /// </summary>
        public bool IsVisibleDOBError
        {
            get { return isVisibleDOBError; }
            set { isVisibleDOBError = value; OnPropertyChanged(nameof(IsVisibleDOBError)); }
        }

        private string sslNumber;
        /// <summary>
        /// Property for SSL Number
        /// </summary>
        public string SSLNumber
        {
            get { return sslNumber; }
            set
            {
                sslNumber = value;
                if (!string.IsNullOrEmpty(sslNumber))
                {
                    SSLNoErrorMessage = string.Empty;
                    IsVisibleSSLNOError = false;
                }
                else
                {
                    SSLNoErrorMessage = SSLNoError;
                    IsVisibleSSLNOError = true;
                }
                OnPropertyChanged(nameof(SSLNumber));
            }
        }

        private string sSLNoErrorMessage;
        /// <summary>
        /// Property for SSLNO Error message
        /// </summary>
        public string SSLNoErrorMessage
        {
            get { return sSLNoErrorMessage; }
            set { sSLNoErrorMessage = value; OnPropertyChanged(nameof(SSLNoErrorMessage)); }
        }

        private bool isVisibleSSLNOError;
        /// <summary>
        /// Property for  SSLNO Error visible
        /// </summary>
        public bool IsVisibleSSLNOError
        {
            get { return isVisibleSSLNOError; }
            set { isVisibleSSLNOError = value; OnPropertyChanged(nameof(IsVisibleSSLNOError)); }
        }

        private string routingNo;
        /// <summary>
        /// Property for Routing Number
        /// </summary>
        public string RoutingNo
        {
            get { return routingNo; }
            set
            {
                routingNo = value;
                if (!string.IsNullOrEmpty(routingNo))
                {
                    RoutingNoErrorMessage = string.Empty;
                    IsVisibleRoutingNoError = false;
                }
                else
                {
                    RoutingNoErrorMessage = ResourceValues.RoutingNoErrorMessage;
                    IsVisibleRoutingNoError = true;
                }
                OnPropertyChanged(nameof(RoutingNo));
            }
        }

        private string routingNoErrorMessage;
        /// <summary>
        /// Property for Routing Number Error message
        /// </summary>
        public string RoutingNoErrorMessage
        {
            get { return routingNoErrorMessage; }
            set { routingNoErrorMessage = value; OnPropertyChanged(nameof(RoutingNoErrorMessage)); }
        }

        private bool isVisibleRoutingNoError;
        /// <summary>
        /// Property for Routing No Error visible
        /// </summary>
        public bool IsVisibleRoutingNoError
        {
            get { return isVisibleRoutingNoError; }
            set { isVisibleRoutingNoError = value; OnPropertyChanged(nameof(IsVisibleRoutingNoError)); }
        }


        private string bankAcountNo;
        /// <summary>
        /// Property for Account Number
        /// </summary>
        public string BankAcountNo
        {
            get { return bankAcountNo; }
            set
            {
                bankAcountNo = value;
                if (!string.IsNullOrEmpty(bankAcountNo))
                {
                    BankAccountErrorMessage = string.Empty;
                    IsVisibleBankAccountNoError = false;
                }
                else
                {
                    BankAccountErrorMessage = ResourceValues.BankAccountErrorMessage;
                    IsVisibleBankAccountNoError = true;
                }
                OnPropertyChanged(nameof(BankAcountNo));
            }
        }

        private string bankAccountErrorMessage;
        /// <summary>
        /// Property for Account Number Error message
        /// </summary>
        public string BankAccountErrorMessage
        {
            get { return bankAccountErrorMessage; }
            set { bankAccountErrorMessage = value; OnPropertyChanged(nameof(BankAccountErrorMessage)); }
        }

        private bool isVisibleBankAccountNoError;
        /// <summary>
        /// Property for AccountNo Error visible
        /// </summary>
        public bool IsVisibleBankAccountNoError
        {
            get { return isVisibleBankAccountNoError; }
            set { isVisibleBankAccountNoError = value; OnPropertyChanged(nameof(IsVisibleBankAccountNoError)); }
        }


        private string mobileNumber;
        /// <summary>
        /// Property for User MobilenumberforText
        /// </summary>
        public string MobileNumber
        {
            get { return mobileNumber; }
            set
            {
                mobileNumber = value;
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    MobileNumberErrorMessage = string.Empty;
                    IsMobileNumberError = false;
                }

                OnPropertyChanged(nameof(MobileNumber));
            }
        }
        private string mobileNumberErrorMessage;
        /// <summary>
        /// Property for mobile number Error message
        /// </summary>
        public string MobileNumberErrorMessage
        {
            get { return mobileNumberErrorMessage; }
            set { mobileNumberErrorMessage = value; OnPropertyChanged(nameof(MobileNumberErrorMessage)); }
        }
        private bool isMobileNumberError;
        /// <summary>
        /// Property for isMobileNumber Error
        /// </summary>
        public bool IsMobileNumberError
        {
            get { return isMobileNumberError; }
            set { isMobileNumberError = value; OnPropertyChanged(nameof(IsMobileNumberError)); }
        }

        private string addressLine1;
        /// <summary>
        /// Property for User AddressLine1
        /// </summary>
        public string AddressLine1
        {
            get { return addressLine1; }
            set
            {
                addressLine1 = value;
                if (!string.IsNullOrEmpty(addressLine1))
                {
                    AddressLine1ErrorMessage = string.Empty;
                    IsAddressLine1Error = false;
                }
                else
                {
                    AddressLine1ErrorMessage = ResourceValues.AddressLine1ErrorMessage;
                    IsAddressLine1Error = true;
                }
                OnPropertyChanged(nameof(AddressLine1));
            }
        }

        private bool isAddressLine1Error;
        /// <summary>
        /// Property for AddressLine1 Error visible
        /// </summary>
        public bool IsAddressLine1Error
        {
            get { return isAddressLine1Error; }
            set { isAddressLine1Error = value; OnPropertyChanged(nameof(IsAddressLine1Error)); }
        }
        private string addressLine1ErrorMessage;
        /// <summary>
        /// Property for AddressLine1 Error message
        /// </summary>
        public string AddressLine1ErrorMessage
        {
            get { return addressLine1ErrorMessage; }
            set { addressLine1ErrorMessage = value; OnPropertyChanged(nameof(AddressLine1ErrorMessage)); }
        }

        private bool isVisibleAddressLine1Error;
        /// <summary>
        /// Property for isVisibleAddressLine1Error
        /// </summary>
        public bool IsVisibleAddressLine1Error
        {
            get { return isVisibleAddressLine1Error; }
            set
            {
                isVisibleAddressLine1Error = value; OnPropertyChanged(nameof(IsVisibleAddressLine1Error));
            }
        }

        private string addressLine2;
        /// <summary>
        /// Property for User Address Line 2
        /// </summary>
        public string AddressLine2
        {
            get { return addressLine2; }
            set
            {

                addressLine2 = value; OnPropertyChanged(nameof(AddressLine2));
            }
        }

        private string addressLine2ErrorMessage;
        /// <summary>
        /// Property for AddressLine2 Error message
        /// </summary>
        public string AddressLine2ErrorMessage
        {
            get { return addressLine2ErrorMessage; }
            set { addressLine2ErrorMessage = value; OnPropertyChanged(nameof(AddressLine2ErrorMessage)); }
        }
        private bool isVisibleAddressLine2Error;
        /// <summary>
        /// Property for use isVisibleAddressLine2Error
        /// </summary>
        public bool IsVisibleAddressLine2Error
        {
            get { return isVisibleAddressLine2Error; }
            set
            {
                isVisibleAddressLine2Error = value; OnPropertyChanged(nameof(IsVisibleAddressLine2Error));
            }
        }


        private string cityName;
        /// <summary>
        /// Property for city name
        /// </summary>
        public string CityName
        {
            get { return cityName; }
            set
            {
                cityName = value;
                if (!string.IsNullOrEmpty(cityName))
                {
                    CityErrorMessage = string.Empty;
                    IsVisibleCityNameError = false;
                }
                else
                {
                    CityErrorMessage = ResourceValues.CityErrorMessage;
                    IsVisibleCityNameError = true;
                }
                OnPropertyChanged(nameof(CityName));
            }
        }

        private string cityErrorMessage;
        /// <summary>
        /// Property for city name Error message
        /// </summary>
        public string CityErrorMessage
        {
            get { return cityErrorMessage; }
            set { cityErrorMessage = value; OnPropertyChanged(nameof(CityErrorMessage)); }
        }

        private bool isVisibleCityNameError;
        /// <summary>
        /// Property for City name Error visible
        /// </summary>
        public bool IsVisibleCityNameError
        {
            get { return isVisibleCityNameError; }
            set { isVisibleCityNameError = value; OnPropertyChanged(nameof(IsVisibleCityNameError)); }
        }



        public ObservableCollection<State> listOfState;
        /// <summary>
        /// Propert for ListOf State
        /// </summary>
        public ObservableCollection<State> ListOfState
        {
            get { return listOfState; }
            set { listOfState = value; OnPropertyChanged(nameof(ListOfState)); }
        }

        private State stateValue;
        /// <summary>
        /// Property for Selected State
        /// </summary>
        public State StateValue
        {
            get { return stateValue; }
            set
            {
                stateValue = value;
                OnPropertyChanged(nameof(StateValue));
                if (StateValue != null)
                {
                    StateNameErrorMessage = string.Empty;
                    IsVisibleStateNameError = false;

                }
            }
        }

        private string stateNameErrorMessage;
        /// <summary>
        /// Property for state name Error message
        /// </summary>
        public string StateNameErrorMessage
        {
            get { return stateNameErrorMessage; }
            set { stateNameErrorMessage = value; OnPropertyChanged(nameof(StateNameErrorMessage)); }
        }

        private bool isVisibleStateNameError;
        /// <summary>
        /// Property for State name Error visible
        /// </summary>
        public bool IsVisibleStateNameError
        {
            get { return isVisibleStateNameError; }
            set { isVisibleStateNameError = value; OnPropertyChanged(nameof(IsVisibleStateNameError)); }
        }

        private string countryName;
        /// <summary>
        /// Property for Country name
        /// </summary>
        public string CountryName
        {
            get { return countryName; }
            set
            {
                countryName = value;
                if (!string.IsNullOrEmpty(countryName))
                {
                    CountryNameErrorMessage = string.Empty;
                    IsVisibleCountryNameError = false;
                }
                else
                {
                    CountryNameErrorMessage = ResourceValues.CountryNameErrorMessage;
                    IsVisibleCountryNameError = true;
                }
                OnPropertyChanged(nameof(CountryName));
            }
        }

        private string countryNameErrorMessage;
        /// <summary>
        /// Property for Country name Error message
        /// </summary>
        public string CountryNameErrorMessage
        {
            get { return countryNameErrorMessage; }
            set { countryNameErrorMessage = value; OnPropertyChanged(nameof(CountryNameErrorMessage)); }
        }

        private bool isVisibleCountryNameError;
        /// <summary>
        /// Property for Country name Error visible
        /// </summary>
        public bool IsVisibleCountryNameError
        {
            get { return isVisibleCountryNameError; }
            set { isVisibleCountryNameError = value; OnPropertyChanged(nameof(IsVisibleCountryNameError)); }
        }


        private string zipCode;
        /// <summary>
        /// Property for Zipcode
        /// </summary>
        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                zipCode = value;
                if (!string.IsNullOrEmpty(zipCode))
                {
                    ZipCodeErrorMessage = string.Empty;
                    IsVisibleZipCodeError = false;
                }
                else
                {
                    ZipCodeErrorMessage = ResourceValues.ZipCodeErrorMessage;
                    IsVisibleZipCodeError = true;
                }
                OnPropertyChanged(nameof(ZipCode));
            }
        }

        private string zipCodeErrorMessage;
        /// <summary>
        /// Property for ZipCode  Error message
        /// </summary>
        public string ZipCodeErrorMessage
        {
            get { return zipCodeErrorMessage; }
            set { zipCodeErrorMessage = value; OnPropertyChanged(nameof(ZipCodeErrorMessage)); }
        }

        private bool isVisibleZipCodeError;
        /// <summary>
        /// Property for ZipCode Error visible
        /// </summary>
        public bool IsVisibleZipCodeError
        {
            get { return isVisibleZipCodeError; }
            set { isVisibleZipCodeError = value; OnPropertyChanged(nameof(IsVisibleZipCodeError)); }
        }

        private string documentUploadMessage;
        /// <summary>
        /// Property for Document upload message
        /// </summary>
        public string DocumentUploadMessage
        {
            get { return documentUploadMessage; }
            set { documentUploadMessage = value; OnPropertyChanged(nameof(DocumentUploadMessage)); }
        }

        private string documentUploadMessageFront;
        /// <summary>
        /// Property for front face Document  upload message
        /// </summary>
        public string DocumentUploadMessageFront
        {
            get { return documentUploadMessageFront; }
            set { documentUploadMessageFront = value; OnPropertyChanged(nameof(DocumentUploadMessageFront)); }
        }

        private string documentUploadMessageBack;
        /// <summary>
        /// Property for Back face Document  upload message
        /// </summary>
        public string DocumentUploadMessageBack
        {
            get { return documentUploadMessageBack; }
            set { documentUploadMessageBack = value; OnPropertyChanged(nameof(DocumentUploadMessageBack)); }
        }

        private bool isVisibleDocumentUplodedMessage;
        /// <summary>
        /// Property for Ducument upload message Show
        /// </summary>
        public bool IsVisibleDocumentUplodedMessage
        {
            get { return isVisibleDocumentUplodedMessage; }
            set { isVisibleDocumentUplodedMessage = value; OnPropertyChanged(nameof(IsVisibleDocumentUplodedMessage)); }
        }

        #endregion

        #region Commands


        private Command commandOnGetState;
        /// <summary>
        /// Command  for get state from api
        /// </summary>
        public Command CommandOnGetState
        {
            get
            {
                if (commandOnGetState == null)
                {
                    commandOnGetState = new Command(() =>
                    {
                        try
                        {
                            if (ListOfState == null)
                            {
                                GetState();
                            }

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    });
                }
                return commandOnGetState;
            }

        }

        private Command commandOnBackForService;
        /// <summary>
        /// This command use for go Back screen
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
                            isFromFileUpload = false;
                            await App.Current.MainPage.Navigation.PopAsync();
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

        private Command commandOnShowDocumentType;
        /// <summary>
        /// This command show document type info
        /// </summary>
        public Command CommandOnShowDocumentType
        {
            get
            {
                if (commandOnShowDocumentType == null)
                {
                    commandOnShowDocumentType = new Command( () =>
                    {
                        try
                        {
                            if (IsEnableField == true)
                            {
                                App.Current.MainPage.DisplayAlert("Acceptable forms of identification:", "i.    U.S.visa card" + "\n" + "ii.   Passport" + "\n" + "iii.  Passport card" + "\n" + "iv.   Driver license" + "\n" + "v.    State issued ID card" + "\n" + "vi.   Resident permit ID / U.S.Green Card" + "\n" + "vii.  Border crossing card" + "\n" + "viii. Child ID card" + "\n" + "ix.   NYC card", ResourceValues.OkButtontext);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnShowDocumentType;
            }
        }

        private Command commandOnUploadDocument;
        /// <summary>
        /// This command use for Save Account Detail
        /// </summary>
        public Command CommandOnUploadDocument
        {
            get
            {
                if (commandOnUploadDocument == null)
                {
                    commandOnUploadDocument = new Command(async (e) =>
                    {
                        try
                        {
                            if (IsEnableField == true)
                            {
                                isFromFileUpload = true;

                                if (!CrossMedia.Current.IsPickPhotoSupported)
                                {
                                    await App.Current.MainPage.DisplayAlert(ResourceValues.TitelGallery, ResourceValues.DocumentErrorMessage, ResourceValues.OkButtontext);
                                    return;
                                }

                                var filePath = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions

                                {
                                   
                                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                                    CustomPhotoSize = 10
                                });

                                if (filePath == null)
                                {
                                    return;
                                }
                                else
                                {
                                    var item = e;
                                    switch (item)
                                    {

                                        case ConstantValues.One:
                                            ssnVerificationDocumentFrontPath = filePath.Path.ToString();
                                            IsVisibleDocumentUplodedMessage = true;
                                            DocumentUploadMessageFront = ResourceValues.UploadDocMessage;
                                            DocumentUplodedMessageTextColorFront = Color.FromHex(ConstantValues.AppColor);
                                            break;
                                        case ConstantValues.Two:
                                            ssnVerificationDocumentBackPath = filePath.Path.ToString();
                                            IsVisibleDocumentUplodedMessage = true;
                                            DocumentUploadMessageBack = ResourceValues.UploadDocMessage;
                                            DocumentUplodedMessageTextColorBack = Color.FromHex(ConstantValues.AppColor);
                                            break;
                                    }
                                }

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
                    });
                }
                return commandOnUploadDocument;
            }
        }

        private Command commandOnSaveAccountDetail;
        /// <summary>
        /// This command use for Save Account Detail
        /// </summary>
        public Command CommandOnSaveAccountDetail
        {
            get
            {
                if (commandOnSaveAccountDetail == null)
                {
                    commandOnSaveAccountDetail = new Command(async () =>
                    {
                        try
                        {
                            if (ValidateCardDetails())
                            {

                                AddNegotiatorBankAccountRequest addNegotiatorBankAccountRequest = new AddNegotiatorBankAccountRequest();
                                var OS_Platform = Xamarin.Essentials.DeviceInfo.Platform.ToString();

                                string IpAddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                                if (!string.IsNullOrEmpty(IpAddress))
                                {
                                    addNegotiatorBankAccountRequest.ip = IpAddress;
                                    addNegotiatorBankAccountRequest.accountHolderName = AccountHolderName;
                                    addNegotiatorBankAccountRequest.routingNumber = RoutingNo;
                                    addNegotiatorBankAccountRequest.accountNumber = BankAcountNo;
                                    addNegotiatorBankAccountRequest.country = ResourceValues.DisplayCountryCode;
                                    addNegotiatorBankAccountRequest.SSN = SSLNumber;
                                    //addNegotiatorBankAccountRequest.accountId = accountId;
                                    //addNegotiatorBankAccountRequest.bankId = bankId;


                                    if (DOB != null)
                                    {
                                        addNegotiatorBankAccountRequest.day = DOB.Value.Day.ToString();
                                        addNegotiatorBankAccountRequest.month = DOB.Value.Month.ToString();
                                        addNegotiatorBankAccountRequest.year = DOB.Value.Year.ToString();
                                    }
                                    await SaveBankAccountDetail(addNegotiatorBankAccountRequest);
                                   
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread( () =>
                                    {
                                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.IPAddressErrorMessage);
                                    });
                                }
                            }
                            else
                            {
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnSaveAccountDetail;
            }
        }

        private Command commandOnSkip;
        /// <summary>
        /// this command use for skip and nevigate to page
        /// </summary>
        public Command CommandOnSkip
        {
            get
            {
                if (commandOnSkip == null)
                {
                    commandOnSkip = new Command(async () =>
                    {
                        try
                        {
                            var flag = await App.Current.MainPage.DisplayAlert(String.Empty, QBidResource.ResourceValues.ConfirmSetSkipBankDetailMessage, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext);
                            if (flag)
                            {
                                switch (QBidHelper.NavigatePage)
                                {
                                    case "UserLoginView":
                                        await App.Current.MainPage.Navigation.PushAsync(new UserLoginView());
                                        QBidHelper.NavigatePage = string.Empty;
                                        break;
                                    default:
                                        await App.Current.MainPage.Navigation.PopAsync();
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            LogManager.TraceErrorLog(ex);
                        }


                    });
                }
                return commandOnSkip;
            }

        }


        private Command commandOnEdit;
        /// <summary>
        /// this command use for edit
        /// </summary>
        public Command CommandOnEdit
        {
            get
            {
                if (commandOnEdit == null)
                {
                    commandOnEdit = new Command( () =>
                    {
                        try
                        {

                            IsEditShow = false;
                            IsCancelShow = true;
                            IsUpdateShow = true;
                            IsTextReadOnly = false;
                            IsEnableField = true;
                        }
                        catch (Exception ex)
                        {

                            LogManager.TraceErrorLog(ex);
                        }


                    });
                }
                return commandOnEdit;
            }

        }

        private Command commandOnCancel;
        /// <summary>
        /// this command use for Cancel
        /// </summary>
        public Command CommandOnCancel
        {
            get
            {
                if (commandOnCancel == null)
                {
                    commandOnCancel = new Command( () =>
                    {
                        try
                        {

                            IsCancelShow = false;
                            IsUpdateShow = false;
                            IsEditShow = true;
                            IsTextReadOnly = true;
                            IsEnableField = false;

                            ssnVerificationDocumentFrontPath = string.Empty;
                            ssnVerificationDocumentBackPath = string.Empty;
                            DocumentUploadMessageFront = ResourceValues.UploadDocFrontFace;
                            DocumentUploadMessageBack = ResourceValues.UploadDocBackFace;
                            DocumentUplodedMessageTextColorFront = Color.FromHex(ConstantValues.AppBlackColor);
                            DocumentUplodedMessageTextColorBack = Color.FromHex(ConstantValues.AppBlackColor);

                        }
                        catch (Exception ex)
                        {

                            LogManager.TraceErrorLog(ex);
                        }


                    });
                }
                return commandOnCancel;
            }

        }

        private Command commandOnUpdateAccountDetail;
        /// <summary>
        /// this command use for Update account detail
        /// </summary>
        public Command CommandOnUpdateAccountDetail
        {
            get
            {
                if (commandOnUpdateAccountDetail == null)
                {
                    commandOnUpdateAccountDetail = new Command(async () =>
                    {
                        try
                        {
                            if (ValidateCardDetails())
                            {
                                IsLoader = true;
                                var OS_Platform = Xamarin.Essentials.DeviceInfo.Platform.ToString();

                                string IpAddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                                if (!string.IsNullOrEmpty(IpAddress))
                                {

                                    UpdateNegotiatorBankAccountRequest updateNegotiatorBankAccountRequest = new UpdateNegotiatorBankAccountRequest();
                                    updateNegotiatorBankAccountRequest.ip = IpAddress;
                                    updateNegotiatorBankAccountRequest.accountHolderName = AccountHolderName;
                                    updateNegotiatorBankAccountRequest.routingNumber = RoutingNo;
                                    updateNegotiatorBankAccountRequest.SSN = SSLNumber;
                                    updateNegotiatorBankAccountRequest.accountNumber = BankAcountNo;
                                    updateNegotiatorBankAccountRequest.country = CountryName;
                                    updateNegotiatorBankAccountRequest.accountId = accountId;
                                    updateNegotiatorBankAccountRequest.bankId = bankId;
                                    updateNegotiatorBankAccountRequest.mobileNumber = MobileNumber;
                                    updateNegotiatorBankAccountRequest.AddressLine1 = AddressLine1;
                                    updateNegotiatorBankAccountRequest.AddressLine2 = AddressLine2;
                                    updateNegotiatorBankAccountRequest.city = CityName;
                                    updateNegotiatorBankAccountRequest.state = StateValue.code;
                                    updateNegotiatorBankAccountRequest.zipCode = ZipCode;

                                    /// ssn no file upload process==============
                                    if (IsDocumentUploadShow == true)
                                    {
                                        StripeKeyResponce result = null;
                                        using (APIService aPIService = new APIService())
                                        {
                                            result = await aPIService.GetStripeKeyAPI();
                                            if (result != null)
                                            {
                                                if (result.data != null)
                                                {
                                                    StripeConfiguration.ApiKey = result.data.stripeSecret;

                                                    if (ssnVerificationDocumentFrontPath != string.Empty)
                                                    {
                                                        using (FileStream stream = System.IO.File.Open(ssnVerificationDocumentFrontPath, FileMode.Open))
                                                        {
                                                            var options = new FileCreateOptions
                                                            {
                                                                File = stream,
                                                                Purpose = FilePurpose.IdentityDocument
                                                            };
                                                            var service = new FileService();
                                                            var file = service.Create(options);
                                                            if (file != null)
                                                            {
                                                                ssnVerificationDocumentFrontId = file.Id;
                                                                IsVisibleDocumentUplodedMessage = true;
                                                                updateNegotiatorBankAccountRequest.identityDocumentFront = ssnVerificationDocumentFrontId;
                                                            }
                                                        }
                                                    }
                                                    if (ssnVerificationDocumentBackPath != string.Empty)
                                                    {
                                                        using (FileStream stream = System.IO.File.Open(ssnVerificationDocumentBackPath, FileMode.Open))
                                                        {
                                                            var options = new FileCreateOptions
                                                            {
                                                                File = stream,
                                                                Purpose = FilePurpose.IdentityDocument
                                                            };
                                                            var service = new FileService();
                                                            var file = service.Create(options);
                                                            if (file != null)
                                                            {
                                                                ssnVerificationDocumentBackId = file.Id;
                                                                IsVisibleDocumentUplodedMessage = true;
                                                                updateNegotiatorBankAccountRequest.identityDocumentBack = ssnVerificationDocumentBackId;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    /// ========================================
                                    await UpdateBankAccountDetail(updateNegotiatorBankAccountRequest);
                                    isFromFileUpload = false;


                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread( () =>
                                    {
                                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.IPAddressErrorMessage);
                                    });
                                }
                            }
                            else
                            {
                              
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

                    });
                }
                return commandOnUpdateAccountDetail;
            }

        }

        #endregion


        #region Methods

        /// <summary>
        /// validate all fields 
        /// </summary>
        /// <returns></returns>
        private bool ValidateCardDetails()
        {
            try
            {

                if (string.IsNullOrEmpty(AccountHolderName))
                {
                    AccountHolderErrorMessage = ResourceValues.AccountHolderErrorMessage;
                    IsVisibleAccountHolderNameError = true;
                    return false;
                }
                if (IsDOBvisible == true)
                {
                    if (DOB == null)
                    {
                        DOBerrorMessage = ResourceValues.DOBerrorMessage;
                        IsVisibleDOBError = true;
                        return false;
                    }
                    if (DOB != null)
                    {
                        int year = DOB.Value.Year;
                        int currYear = DateTime.UtcNow.Year;
                        int valYear = currYear - year;
                        if (valYear < 0 || valYear == 0)
                        {
                            DOBerrorMessage = ResourceValues.DOBErrorValidateMessage;
                            IsVisibleDOBError = true;
                            return false;
                        }
                        if (valYear < 18)
                        {
                            DOBerrorMessage = ResourceValues.DateOfBirthErrorMessage;
                            IsVisibleDOBError = true;
                            return false;
                        }

                    }
                }
                if (IsSSNvisible == true)
                {
                    if (string.IsNullOrEmpty(SSLNumber))
                    {
                      
                        SSLNoErrorMessage = SSLNoError;
                        IsVisibleSSLNOError = true;
                        return false;
                    }
                    if (SSLNumber.Length < MaxLength || SSLNumber.Length > MaxLength)
                    {
                       
                        SSLNoErrorMessage = SSLNoError;
                        IsVisibleSSLNOError = true;
                        return false;
                    }
                }


                if (string.IsNullOrEmpty(RoutingNo))
                {
                    RoutingNoErrorMessage = ResourceValues.RoutingNoErrorMessage;
                    IsVisibleRoutingNoError = true;
                    return false;
                }
                else
                {
                    if (RoutingNo.Length < 9 || RoutingNo.Length > 9)
                    {
                        RoutingNoErrorMessage = ResourceValues.RoutingNoValidErrorMessage;
                        IsVisibleRoutingNoError = true;
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(BankAcountNo))
                {
                    BankAccountErrorMessage = ResourceValues.BankAccountErrorMessage;
                    IsVisibleBankAccountNoError = true;
                    return false;
                }
                else if (BankAcountNo.Length < 7 || BankAcountNo.Length > 15)
                {

                   
                    BankAccountErrorMessage = ResourceValues.AccountValidMessage;
                    IsVisibleBankAccountNoError = true;

                    return false;
                }

                else if (!string.IsNullOrEmpty(BankAcountNo))
                {
                    double numericValue;
                    bool isNumber = double.TryParse(BankAcountNo, out numericValue);
                    if (isNumber == false)
                    {
                        BankAccountErrorMessage = ResourceValues.BankAccountErrorMessage;
                        IsVisibleBankAccountNoError = true;
                        return false;
                    }

                }
                if (Isvisible == true)
                {
                    var mobileNumber = string.Empty;
                    if (!string.IsNullOrEmpty(MobileNumber))
                    {
                        mobileNumber = MobileNumber.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
                    }
                    if (string.IsNullOrEmpty(MobileNumber))
                    {
                        MobileNumberErrorMessage = ResourceValues.MobilenumberforTextErrorMessage;
                        IsMobileNumberError = true;
                        return false;
                    }
                    if (mobileNumber.Length < 10)
                    {
                        MobileNumberErrorMessage = ResourceValues.ValidMobileMessage;
                        IsMobileNumberError = true;
                        return false;
                    }

                    if (string.IsNullOrEmpty(AddressLine1))
                    {
                        AddressLine1ErrorMessage = ResourceValues.AddressLine1ErrorMessage;
                        IsVisibleAddressLine1Error = true;
                        return false;
                    }
                    if (string.IsNullOrEmpty(CityName))
                    {
                        CityErrorMessage = ResourceValues.CityErrorMessage;
                        IsVisibleCityNameError = true;
                        return false;
                    }

                    if (StateValue == null)
                    {
                        StateNameErrorMessage = ResourceValues.StateNameErrorMessage;
                        IsVisibleStateNameError = true;
                        return false;

                    }

                    if (string.IsNullOrEmpty(ZipCode))
                    {
                        ZipCodeErrorMessage = ResourceValues.ZipCodeErrorMessage;
                        IsVisibleZipCodeError = true;
                        return false;
                    }
                    if (!string.IsNullOrEmpty(ZipCode))
                    {
                        if (ZipCode.Length < 5)
                        {
                            ZipCodeErrorMessage = ResourceValues.ValidZipCodeMessage;
                            IsVisibleZipCodeError = true;
                            return false;
                        }
                    }
                }
                if (IsDocumentUploadShow == true && ssnVerificationDocumentFrontPath == string.Empty)
                {
                    DocumentUploadMessageFront =ResourceValues.UploadDocFrontFace;
                    IsVisibleDocumentUplodedMessage = true;
                    DocumentUplodedMessageTextColorFront = Color.FromHex(ConstantValues.AppRedColor);
                    return false;
                }
                if (IsDocumentUploadShow == true && ssnVerificationDocumentBackPath == string.Empty)
                {
                    DocumentUploadMessageBack = ResourceValues.UploadDocBackFace;
                    IsVisibleDocumentUplodedMessage = true;
                    DocumentUplodedMessageTextColorBack = Color.FromHex(ConstantValues.AppRedColor);
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
                return false;
            }
        }
        /// <summary>
        /// method for save bank detail 
        /// </summary>
        /// <returns></returns>
        public async Task SaveBankAccountDetail(AddNegotiatorBankAccountRequest addNegotiatorBankAccountRequest)
        {
            CommonResponse responce = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    using (APIService aPIService = new APIService())
                    {
                        responce = await aPIService.AddNegotiatorBankAccountAPI(addNegotiatorBankAccountRequest);
                    }
                    if (responce != null)
                    {
                        if (responce.code == 200)
                        {
                            Preferences.Set(ConstantValues.IsNegotiatorAccountAddedPref, 1);
                            Device.BeginInvokeOnMainThread(async () =>
                            {

                                DependencyService.Get<IToastMessage>().ShortAlert(responce.message.ToString());


                                await Task.Delay(2000);
                                switch (QBidHelper.NavigatePage)
                                {
                                    case "UserLoginView":
                                        await App.Current.MainPage.Navigation.PushAsync(new UserLoginView());
                                        QBidHelper.NavigatePage = string.Empty;
                                        break;
                                    default:
                                        await App.Current.MainPage.Navigation.PopAsync();
                                        break;
                                }
                            });
                        }
                        else
                        {
                            var err = string.Empty;
                            if (Convert.ToString(responce.message).ToLower() == "failed")
                            {
                                err = Convert.ToString(responce.error);
                            }
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                DependencyService.Get<IToastMessage>().LongAlert(responce.message +"\n"+ err);
                            });

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
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }

        /// <summary>
        /// method for update bank detail 
        /// </summary>
        /// <returns></returns>
        public async Task UpdateBankAccountDetail(UpdateNegotiatorBankAccountRequest updateNegotiatorBankAccountRequest)
        {
            CommonResponse responce = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    using (APIService aPIService = new APIService())
                    {
                        responce = await aPIService.UpdateNegotiatorBankAccountAPI(updateNegotiatorBankAccountRequest);
                    }
                    if (responce != null)
                    {
                        if (responce.code == 200)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                DependencyService.Get<IToastMessage>().ShortAlert(responce.message.ToString());
                                await Task.Delay(2000);
                                switch (QBidHelper.NavigatePage)
                                {
                                    case "UserLoginView":
                                        await App.Current.MainPage.Navigation.PushAsync(new UserLoginView());
                                        QBidHelper.NavigatePage = string.Empty;
                                        break;
                                    default:
                                        await App.Current.MainPage.Navigation.PopAsync();
                                        break;
                                }
                            });
                        }
                        else
                        {
                            var err = string.Empty;
                            if (Convert.ToString(responce.message).ToLower() == "failed")
                            {
                                err = Convert.ToString(responce.error);
                            }
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmBankValidMessage  +"\n"+ err);
                            });

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
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
        }

        /// <summary>
        /// method for get date for bank detail 
        /// </summary>
        /// <returns></returns>
        public async Task GetBankAccountDetail()
        {
            GetNegotiatorBankDetailResponse response = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        IsLoader = true;
                    });

                    accountId = string.Empty;
                    bankId = string.Empty;
                    GetState();
                    using (APIService aPIService = new APIService())
                    {
                        response = await aPIService.GetNegotiatorBankAccountAPI();
                    }
                    if (response != null)
                    {
                        if (response.code == 200 && response.data != null)
                        {

                            AccountHolderName = response.data.accountHolderName;
                            BankAcountNo = ConstantValues.AccountNoHideText + response.data.AccountNumberLast4Digit;
                            RoutingNo = response.data.routing;

                            MobileNumber = response.data.mobileNumber;
                            AddressLine1 = response.data.AddressLine1;
                            AddressLine2 = response.data.AddressLine2;
                            CityName = response.data.city;
                            if (ListOfState != null && ListOfState.Count > 0)
                            {
                                StateValue = ListOfState.Where(a => a.code.ToLower() == response.data.state.ToLower()).FirstOrDefault();

                            }
                            CountryName = response.data.country;
                            ZipCode = response.data.zipCode;

                            IsTextReadOnly = true;
                            IsEnableField = false;
                            accountId = response.data.account;
                            bankId = response.data.Id;
                            var accountStatus = response.data.accountStatus;
                            var bankTransefer = response.data.bankTransefer;
                            if (!string.IsNullOrEmpty(response.data.bankTransefer))
                            {
                                IsShowHide = true;
                                if (response.data.bankTransefer.ToLower() == ConstantValues.BankAccountActiveStatus.ToLower())
                                {
                                    BankAccountStatusImage = ConstantValues.BankAccountActiveImage;
                                    BankAccountStatus = ConstantValues.BankAccountActiveStatus;
                                    BankAccountStatusColor = ConstantValues.AppColor;
                                    Preferences.Set(ConstantValues.IsBankAccountStatusPref, response.data.bankTransefer);
                                    Preferences.Set(ConstantValues.IsBankAccountStatusImagePref, ConstantValues.BankAccountActiveImage);
                                    Preferences.Set(ConstantValues.IsBankVerifiedPref, response.data.accountStatus);
                                    BankAccountStatusColor = ConstantValues.AppColor;

                                    Isvisible = false;

                                }
                                else
                                {
                                    BankAccountStatusImage = ConstantValues.BankAccountInActiveImage;
                                    BankAccountStatus = ConstantValues.BankAccountInActiveStatus;
                                    BankAccountStatusColor = ConstantValues.AppRedColor;
                                    Preferences.Set(ConstantValues.IsBankAccountStatusPref, response.data.bankTransefer);
                                    Preferences.Set(ConstantValues.IsBankAccountStatusImagePref, ConstantValues.BankAccountInActiveImage);
                                    Preferences.Set(ConstantValues.IsBankVerifiedPref, response.data.accountStatus);
                                    BankAccountStatusColor = ConstantValues.AppRedColor;
                                    Isvisible = true;

                                    ///for check require field for not verified by bank
                                    if (IsCancelShow == false)
                                    {
                                        if (response.data.requirement != null)
                                        {
                                            var err = string.Empty;
                                            foreach (var item in response.data.requirement)
                                            {
                                                err = err + item.Replace("address.", string.Empty) + "\n";
                                            }
                                            if (err.Contains("id_number"))
                                            {
                                                MaxLength = 9;
                                                PlaceHolderSSN = ResourceValues.PlaceHolderSSN9Digit;
                                                TitalSSN = ResourceValues.TitalSSN9Digit;
                                                SSLNoError = ResourceValues.SSNNo9DigitErrorMessage;
                                                IsSSNvisible = true;
                                            }
                                            if (err.Contains("verification.document"))
                                            {
                                                ssnVerificationDocumentFrontId = string.Empty;
                                                ssnVerificationDocumentBackId = string.Empty;
                                                IsDocumentUploadShow = true;
                                                DocumentUploadMessageFront = ResourceValues.UploadDocFrontFace;
                                                DocumentUploadMessageBack = ResourceValues.UploadDocBackFace;
                                                DocumentUplodedMessageTextColorFront = Color.FromHex(ConstantValues.AppBlackColor);
                                                DocumentUplodedMessageTextColorBack = Color.FromHex(ConstantValues.AppBlackColor);
                                            }
                                            else
                                            {
                                                ssnVerificationDocumentFrontId = string.Empty;
                                                IsDocumentUploadShow = false;

                                            }


                                            await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.ConfirmBankReEnterFieldMessage + "\n" + "\n" + err, ResourceValues.OkButtontext);
                                            IsLoader = false;
                                        }
                                    }

                                }

                            }
                            else
                            {
                                IsShowHide = false;
                            }

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                DependencyService.Get<IToastMessage>().ShortAlert(response.message.ToString());
                            });
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
        /// method for get state data
        /// </summary>
        /// <returns></returns>
        public async Task GetState()
        {
            try
            {

                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    using (APIService aPIService = new APIService())
                    {
                       
                        var responce = await aPIService.GetStateAPI().ConfigureAwait(true);
                        if (responce != null)
                        {
                            if (responce.data != null && responce.data.Count > 0)
                            {
                                var y = new ObservableCollection<State>(responce.data.OrderBy(x => x.code));
                                ListOfState = y;
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
              
                LogManager.TraceErrorLog(ex);

            }
            finally
            {
              
            }
        }

        #endregion



    }
}
