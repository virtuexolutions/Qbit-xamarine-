using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.QBidResource;
using QBid.Views;
using QBid.Models;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using QBid.Models.APIRequest;
using QBid.APIServices;
using System.Net;
using System.Threading.Tasks;
using QBid.APILog;
using QBid.Models.APIResponse;
using System.Linq;

namespace QBid.ViewModels
{
    public class RegistrationViewModel : BindableObject
    {
       
        private ObservableCollection<ServiceModel> SelectServiceList;
        //private ObservableCollection<LanguageModel> languageList;
        #region Constructor
        /// <summary>
        /// Constructor implementation
        /// </summary>
        public RegistrationViewModel()
        {
            try
            {
                apiServices = new APIService();
                IsNegotiator = true;
                ClearFields();
                ListOfUserType = new ObservableCollection<UserTypeModel>();

                IsChkAccepted = false;
                IsAccepted = false;
                PasswordImage = ConstantValues.ShowPassword;
                IsPasswordVisible = true;
               
                IsPDF = false;


                bool profileEdit = Preferences.Get(ConstantValues.EditProfilePref, false);
                if (profileEdit == true)
                {
                    TitelRegistrationPage = ConstantValues.TitelRegistrationPageEditProfile; // "Update Profile";
                    IsEditShow = true;
                    IsSubmitShow = false;
                    IsShowHide = false;
                    IsCancelShow = false;
                    IsUpdateShow = false;
                    IsTextReadOnly = true;
                    IsEnableField = false;

                    UserTypeData(true);
                }
                else
                {
                    UserTypeData(false);
                    GetState().ConfigureAwait(false);
                    TitelRegistrationPage = ConstantValues.TitelRegistrationPageSignUP; // "Sign Up";
                    IsSubmitShow = true;
                    IsShowHide = true;
                    IsEditShow = false;
                    IsCancelShow = false;
                    IsUpdateShow = false;
                    IsTextReadOnly = false;
                    IsEnableField = true;
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        #endregion

        #region Property

        APIService apiServices = null;

        private string titelRegistrationPage;
        /// <summary>
        /// Property for user titel Registration Page
        /// </summary>
        public string TitelRegistrationPage
        {
            get { return titelRegistrationPage; }
            set
            {
                titelRegistrationPage = value; OnPropertyChanged(nameof(TitelRegistrationPage));
            }
        }
        private bool isMember;
        /// <summary>
        /// Property for user isMember
        /// </summary>
        public bool IsMember
        {
            get { return isMember; }
            set
            {
                isMember = value; OnPropertyChanged(nameof(IsMember));
            }
        }

        private bool isShowHide;
        /// <summary>
        /// Property for user isShowHide 
        /// </summary>
        public bool IsShowHide
        {
            get { return isShowHide; }
            set
            {
                isShowHide = value; OnPropertyChanged(nameof(IsShowHide));
            }
        }
        private bool isSubmitShow;
        /// <summary>
        /// Property for  Submit Show 
        /// </summary>
        public bool IsSubmitShow
        {
            get { return isSubmitShow; }
            set
            {
                isSubmitShow = value; OnPropertyChanged(nameof(IsSubmitShow));
            }
        }



        private bool isEditShow;
        /// <summary>
        /// Property for Edit Show 
        /// </summary>
        public bool IsEditShow
        {
            get { return isEditShow; }
            set
            {
                isEditShow = value; OnPropertyChanged(nameof(IsEditShow));
            }
        }
        private bool isCancelShow;
        /// <summary>
        /// Property for Cancel Show 
        /// </summary>
        public bool IsCancelShow
        {
            get { return isCancelShow; }
            set
            {
                isCancelShow = value; OnPropertyChanged(nameof(IsCancelShow));
            }
        }
        private bool isUpdateShow;
        /// <summary>
        /// Property for Update Show 
        /// </summary>
        public bool IsUpdateShow
        {
            get { return isUpdateShow; }
            set
            {
                isUpdateShow = value; OnPropertyChanged(nameof(IsUpdateShow));
            }
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
        /// Property for field desable
        /// </summary>
        public bool IsEnableField
        {
            get { return isEnableField; }
            set { isEnableField = value; OnPropertyChanged(nameof(IsEnableField)); }
        }

        private bool isPrefferdErrorMessage;
        /// <summary>
        /// Property for user isPrefferdErrorMessage
        /// </summary>
        public bool IsPrefferdErrorMessage
        {
            get { return isPrefferdErrorMessage; }
            set
            {
                isPrefferdErrorMessage = value; OnPropertyChanged(nameof(IsPrefferdErrorMessage));
            }
        }


        private string prefferdErrorMessage;
        /// <summary>
        /// Property for user prefferdErrorMessage
        /// </summary>
        public string PrefferdErrorMessage
        {
            get { return prefferdErrorMessage; }
            set
            {
                prefferdErrorMessage = value; OnPropertyChanged(nameof(PrefferdErrorMessage));
            }
        }

        private bool isEmail;
        /// <summary>
        /// Property for user isEmail
        /// </summary>
        public bool IsEmail
        {
            get { return isEmail; }
            set
            {
                isEmail = value; OnPropertyChanged(nameof(IsEmail));
                if (isEmail)
                {
                    PrefferdErrorMessage = string.Empty;
                    IsPrefferdErrorMessage = false;
                }
            }
        }

        private bool isCall;
        /// <summary>
        /// Property for user isCall
        /// </summary>
        public bool IsCall
        {
            get { return isCall; }
            set
            {
                isCall = value; OnPropertyChanged(nameof(IsCall));
                if (isCall)
                {
                    PrefferdErrorMessage = string.Empty;
                    IsPrefferdErrorMessage = false;
                }
            }
        }

        private bool isText;
        /// <summary>
        /// Property for user isText
        /// </summary>
        public bool IsText
        {
            get { return isText; }
            set
            {
                isText = value; OnPropertyChanged(nameof(IsText));
                if (isText)
                {
                    PrefferdErrorMessage = string.Empty;
                    IsPrefferdErrorMessage = false;
                }
            }
        }

        private bool isTermsofServicesVisible;
        /// <summary>
        /// Property for user isTermsofServicesVisible
        /// </summary>
        public bool IsTermsofServicesVisible
        {
            get { return isTermsofServicesVisible; }
            set
            {
                isTermsofServicesVisible = value; OnPropertyChanged(nameof(IsTermsofServicesVisible));
            }
        }

        private bool isUserTypeSelected;
        /// <summary>
        /// Property for user isUserTypeSelected
        /// </summary>
        public bool IsUserTypeSelected
        {
            get { return isUserTypeSelected; }
            set
            {
                isUserTypeSelected = value; OnPropertyChanged(nameof(IsUserTypeSelected));
            }
        }

        private bool isChkAccepted;
        /// <summary>
        /// Property for submit button according to check terms and services accepted or not
        /// </summary>
        public bool IsChkAccepted
        {
            get { return isChkAccepted; }
            set
            {
                isChkAccepted = value; OnPropertyChanged(nameof(IsChkAccepted));
            }
        }
      
        private bool isAccepted;
        /// <summary>
        ///  Property for check terms and services accepted or not
        /// </summary>
        public bool IsAccepted
        {
            get { return isAccepted; }
            set
            {
                isAccepted = value;
                OnPropertyChanged(nameof(IsAccepted));

                if (isAccepted)
                {
                    IsChkAccepted = true;
                }
                else
                {
                    IsChkAccepted = false;
                }
            }
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


        public ObservableCollection<UserTypeModel> listOfUserType;
        /// <summary>
        /// Propert for ListOfUserType
        /// </summary>
        public ObservableCollection<UserTypeModel> ListOfUserType
        {
            get { return listOfUserType; }
            set { listOfUserType = value; OnPropertyChanged(nameof(ListOfUserType)); }
        }

        private UserTypeModel userTypeValue;
        /// <summary>
        /// Property for userTypeValue
        /// </summary>
        public UserTypeModel UserTypeValue
        {
            get { return userTypeValue; }
            set
            {
                userTypeValue = value;
                OnPropertyChanged(nameof(UserTypeValue));
                if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Member)
                {
                    ClearFields();
                    IsUserTypeSelected = true;
                    IsNegotiator = false;
                    IsMember = true;
                    IsVisibleuserTypeErrorMessage = false;

                }
                else if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                {
                    ClearFields();
                    IsNegotiator = true;
                    IsUserTypeSelected = true;
                    IsMember = false;
                    IsVisibleuserTypeErrorMessage = false;

                }
            }
        }

        private string userTypeErrorMessage;
        /// <summary>
        /// Property for user Type Error message
        /// </summary>
        public string UserTypeErrorMessage
        {
            get { return userTypeErrorMessage; }
            set { userTypeErrorMessage = value; OnPropertyChanged(nameof(UserTypeErrorMessage)); }
        }

        private bool isVisibleuserTypeErrorMessage;
        /// <summary>
        /// Property for use isVisibleuserTypeErrorMessage
        /// </summary>
        public bool IsVisibleuserTypeErrorMessage
        {
            get { return isVisibleuserTypeErrorMessage; }
            set { isVisibleuserTypeErrorMessage = value; OnPropertyChanged(nameof(IsVisibleuserTypeErrorMessage)); }
        }


        private bool isNegotiator;
        /// <summary>
        /// Property for Use isNegotiator
        /// </summary>
        public bool IsNegotiator
        {
            get { return isNegotiator; }
            set
            {
                isNegotiator = value;
                OnPropertyChanged(nameof(IsNegotiator));
            }
        }


        private string service;
        /// <summary>
        /// Property for show selected services
        /// </summary>
        public string Service
        {
            get
            {
                return service;
            }
            set
            {
                service = value; OnPropertyChanged(nameof(Service));
            }
        }

        private string serviceErrorMessage;
        /// <summary>
        /// Property for show services error message
        /// </summary>
        public string ServiceErrorMessage
        {
            get
            {
                return serviceErrorMessage;
            }
            set
            {
                serviceErrorMessage = value; OnPropertyChanged(nameof(ServiceErrorMessage));
            }
        }
        private bool isVisibleserviceError;
        /// <summary>
        /// Property for show isVisibleserviceError
        /// </summary>
        public bool IsVisibleServiceError
        {
            get
            {
                return isVisibleserviceError;
            }
            set
            {
                isVisibleserviceError = value; OnPropertyChanged(nameof(IsVisibleServiceError));
            }
        }
        private string selectService;
        /// <summary>
        /// property for show selectService
        /// </summary>
        public string SelectService
        {
            get
            {
                return selectService;
            }
            set
            {
                selectService = value; OnPropertyChanged(nameof(SelectService));
                if (!string.IsNullOrEmpty(selectService))
                {
                    IsVisibleSelectServiceError = false;
                }
            }
        }
        private string selectServiceErrorMessage;
        /// <summary>
        /// property for show selectServiceErrorMessage
        /// </summary>
        public string SelectServiceErrorMessage
        {
            get
            {
                return selectServiceErrorMessage;
            }
            set
            {
                selectServiceErrorMessage = value; OnPropertyChanged(nameof(SelectServiceErrorMessage));
            }
        }
        private bool isVisibleSelectServiceError;
        /// <summary>
        /// property for show isVisibleSelectServiceError
        /// </summary>
        public bool IsVisibleSelectServiceError
        {
            get
            {
                return isVisibleSelectServiceError;
            }
            set
            {
                isVisibleSelectServiceError = value; OnPropertyChanged(nameof(IsVisibleSelectServiceError));
            }
        }

        private string languageErrorMessage;
        /// <summary>
        /// property for show languageErrorMessage
        /// </summary>
        public string LanguageErrorMessage
        {
            get
            {
                return languageErrorMessage;
            }
            set
            {
                languageErrorMessage = value; OnPropertyChanged(nameof(LanguageErrorMessage));
            }
        }
        private bool isVisibleLanguageError;
        /// <summary>
        /// property use for isVisibleLanguageError
        /// </summary>
        public bool IsVisibleLanguageError
        {
            get
            {
                return isVisibleLanguageError;
            }
            set
            {
                isVisibleLanguageError = value; OnPropertyChanged(nameof(IsVisibleLanguageError));
            }
        }

        private string language;
        /// <summary>
        /// property use for language
        /// </summary>

        public string Language
        {
            get { return language; }
            set
            {
                language = value; OnPropertyChanged(nameof(Language));
                if (!string.IsNullOrEmpty(Language))
                {
                    IsVisibleLanguageError = false;
                }
            }
        }

        private string termsAndConditionErrorMessage;
        /// <summary>
        /// property use for termsAndConditionErrorMessage
        /// </summary>
        public string TermsAndConditionErrorMessage
        {
            get
            {
                return termsAndConditionErrorMessage;
            }
            set
            {
                termsAndConditionErrorMessage = value; OnPropertyChanged(nameof(TermsAndConditionErrorMessage));
            }
        }
        private bool isVisibleTermsAndConditionError;
        /// <summary>
        /// property use for isVisibleTermsAndConditionError
        /// </summary>
        public bool IsVisibleTermsAndConditionError
        {
            get
            {
                return isVisibleTermsAndConditionError;
            }
            set
            {
                isVisibleTermsAndConditionError = value; OnPropertyChanged(nameof(IsVisibleTermsAndConditionError));
            }
        }

        private string firstName;
        /// <summary>
        /// Property for User First Name
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                if (!string.IsNullOrEmpty(firstName))
                {
                    FirstNameErrorMessage = string.Empty;
                    IsVisibleFirstNameError = false;
                }
                //else
                //{
                //    FirstNameErrorMessage = ResourceValues.FirstNameErrorMessage;
                //    IsVisibleFirstNameError = true;
                //}
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string lastName;
        /// <summary>
        /// Property for User Last Name;
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private string companyName;
        /// <summary>
        /// Property for User CompanyName
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }
        private int isRetired;
        /// <summary>
        /// Property for User isRetired
        /// </summary>
        public int IsRetired
        {
            get { return isRetired; }
            set
            {
                isRetired = value;
                OnPropertyChanged(nameof(IsRetired));
            }
        }

        private int isSelfEmployed;
        /// <summary>
        /// Property for User isSelfEmployed
        /// </summary>
        public int IsSelfEmployed
        {
            get { return isSelfEmployed; }
            set
            {
                isSelfEmployed = value;
                OnPropertyChanged(nameof(IsSelfEmployed));
            }
        }
        private string nickName;
        /// <summary>
        /// Property for User NickName
        /// </summary>
        public string NickName
        {
            get { return nickName; }
            set
            {
                nickName = value;
                if (!string.IsNullOrEmpty(nickName))
                {
                    NickNameErrorMessage = string.Empty;
                    IsVisibleNickNameError = false;
                }
                OnPropertyChanged(nameof(NickName));
            }
        }
        private int isDisplayNickName;
        /// <summary>
        /// Property for User isDisplayNickName
        /// </summary>
        public int IsDisplayNickName
        {
            get { return isDisplayNickName; }
            set
            {
                isDisplayNickName = value;

                OnPropertyChanged(nameof(IsDisplayNickName));
            }
        }

        private string email;
        /// <summary>
        /// Property for User Email;
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                if (string.IsNullOrEmpty(email))
                {
                    EmailErrorMessage = ResourceValues.EmailErrorMessage;
                    IsVisibleEmailError = true;
                }
                else
                {
                    bool emailCheck = QBidHelper.IsValidEmail(email);
                    if (!emailCheck)
                    {
                        EmailErrorMessage = ResourceValues.CheckEmailErrorMessage;
                        IsVisibleEmailError = true;
                    }
                    else
                    {
                        EmailErrorMessage = string.Empty;
                        IsVisibleEmailError = false;
                    }
                }
                OnPropertyChanged(nameof(Email));
            }
        }

        private string mobileNumberforText;
        /// <summary>
        /// Property for User MobilenumberforText
        /// </summary>
        public string MobileNumberforText
        {
            get { return mobileNumberforText; }
            set
            {
                mobileNumberforText = value;
                if (!string.IsNullOrEmpty(mobileNumberforText))
                {
                    MobileNumberforTextErrorMessage = string.Empty;
                    IsMobileNumberforTextError = false;
                }

                OnPropertyChanged(nameof(MobileNumberforText));
            }
        }
        private string mobileNumberforLiveCalls;
        /// <summary>
        /// Property for User MobileNumberforLIVECALLS
        /// </summary>
        public string MobileNumberforLiveCalls
        {
            get { return mobileNumberforLiveCalls; }
            set
            {
                mobileNumberforLiveCalls = value;

                OnPropertyChanged(nameof(MobileNumberforLiveCalls));
            }
        }
        private string password;
        /// <summary>
        /// Property for User Password
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                if (!string.IsNullOrEmpty(password))
                {
                    PasswordErrorMessage = string.Empty;
                    IsPasswordError = false;
                }
                else
                {
                    PasswordErrorMessage = ResourceValues.PasswordErrorMessage;
                    IsPasswordError = true;
                }
                OnPropertyChanged(nameof(Password));
            }
        }
        private string confirmPassword;
        /// <summary>
        /// Property for User ConfirmPassword
        /// </summary>
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                if (!string.IsNullOrEmpty(confirmPassword))
                {
                    if (confirmPassword != Password)
                    {
                        ConfirmPasswordErrorMessage = ResourceValues.ConfirmPasswordNotMatchErrorMessage;
                        IsConfirmPasswordError = true;
                    }
                    else
                    {
                        ConfirmPasswordErrorMessage = string.Empty;
                        IsConfirmPasswordError = false;
                    }
                }
                else
                {
                    ConfirmPasswordErrorMessage = ResourceValues.ConfirmPasswordErrorMessage;
                    IsConfirmPasswordError = true;
                }
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private string zipCode;
        /// <summary>
        /// Property for User ZipCode
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
                    IsZipCodeError = false;
                }
                else
                {
                    ZipCodeErrorMessage = ResourceValues.ZipCodeErrorMessage;
                    IsZipCodeError = true;
                }
                OnPropertyChanged(nameof(ZipCode));
            }
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

        private string nickNameErrorMessage;
        /// <summary>
        /// Property for first name Error message
        /// </summary>
        public string NickNameErrorMessage
        {
            get { return nickNameErrorMessage; }
            set { nickNameErrorMessage = value; OnPropertyChanged(nameof(NickNameErrorMessage)); }
        }
        private string firstNameErrorMessage;
        /// <summary>
        /// Property for first name Error message
        /// </summary>
        public string FirstNameErrorMessage
        {
            get { return firstNameErrorMessage; }
            set { firstNameErrorMessage = value; OnPropertyChanged(nameof(FirstNameErrorMessage)); }
        }

        private string emailErrorMessage;
        /// <summary>
        /// Property for email Error message
        /// </summary>
        public string EmailErrorMessage
        {
            get { return emailErrorMessage; }
            set { emailErrorMessage = value; OnPropertyChanged(nameof(EmailErrorMessage)); }
        }
        private string mobileNumberforTextErrorMessage;
        /// <summary>
        /// Property for mobile number for text Error message
        /// </summary>
        public string MobileNumberforTextErrorMessage
        {
            get { return mobileNumberforTextErrorMessage; }
            set { mobileNumberforTextErrorMessage = value; OnPropertyChanged(nameof(MobileNumberforTextErrorMessage)); }
        }

        private string mobileNumberforLIVECALLSErrorMessage;
        /// <summary>
        /// Property for mobile number for live calls Error message
        /// </summary>
        public string MobileNumberforLIVECALLSErrorMessage
        {
            get { return mobileNumberforLIVECALLSErrorMessage; }
            set { mobileNumberforLIVECALLSErrorMessage = value; OnPropertyChanged(nameof(MobileNumberforLIVECALLSErrorMessage)); }
        }

        private string passwordErrorMessage;
        /// <summary>
        /// Property for password Error message
        /// </summary>
        public string PasswordErrorMessage
        {
            get { return passwordErrorMessage; }
            set { passwordErrorMessage = value; OnPropertyChanged(nameof(PasswordErrorMessage)); }
        }
        private string confirmPasswordErrorMessage;
        /// <summary>
        /// Property for confirm password Error message
        /// </summary>
        public string ConfirmPasswordErrorMessage
        {
            get { return confirmPasswordErrorMessage; }
            set { confirmPasswordErrorMessage = value; OnPropertyChanged(nameof(ConfirmPasswordErrorMessage)); }
        }
        private string zipCodeErrorMessage;
        /// <summary>
        /// Property for zip code Error message
        /// </summary>
        public string ZipCodeErrorMessage
        {
            get { return zipCodeErrorMessage; }
            set { zipCodeErrorMessage = value; OnPropertyChanged(nameof(ZipCodeErrorMessage)); }
        }


        private bool isVisibleFirstNameError;
        /// <summary>
        /// Property for isVisibleFirstNameError
        /// </summary>
        public bool IsVisibleFirstNameError
        {
            get { return isVisibleFirstNameError; }
            set { isVisibleFirstNameError = value; OnPropertyChanged(nameof(IsVisibleFirstNameError)); }
        }

        private bool isVisibleNickNameError;
        /// <summary>
        /// Property for isVisibleNickNameError
        /// </summary>
        public bool IsVisibleNickNameError
        {
            get { return isVisibleNickNameError; }
            set { isVisibleNickNameError = value; OnPropertyChanged(nameof(IsVisibleNickNameError)); }
        }


        private bool isVisibleEmailError;
        /// <summary>
        /// Property for isVisibleEmailError
        /// </summary>
        public bool IsVisibleEmailError
        {
            get { return isVisibleEmailError; }
            set { isVisibleEmailError = value; OnPropertyChanged(nameof(IsVisibleEmailError)); }
        }
        private bool isMobileNumberforTextError;
        /// <summary>
        /// Property for isMobileNumberforTextError
        /// </summary>
        public bool IsMobileNumberforTextError
        {
            get { return isMobileNumberforTextError; }
            set { isMobileNumberforTextError = value; OnPropertyChanged(nameof(IsMobileNumberforTextError)); }
        }
        private bool isMobileNumberforLIVECALLSError;
        /// <summary>
        /// Property for mobile for live calls Error visible
        /// </summary>
        public bool IsMobileNumberforLIVECALLSError
        {
            get { return isMobileNumberforLIVECALLSError; }
            set { isMobileNumberforLIVECALLSError = value; OnPropertyChanged(nameof(IsMobileNumberforLIVECALLSError)); }
        }
        private bool isPasswordError;
        /// <summary>
        /// Property for use isPasswordError 
        /// </summary>
        public bool IsPasswordError
        {
            get { return isPasswordError; }
            set { isPasswordError = value; OnPropertyChanged(nameof(IsPasswordError)); }
        }

        private bool isPasswordVisible;
        /// <summary>
        /// Property forisPasswordVisible
        /// </summary>
        public bool IsPasswordVisible
        {
            get { return isPasswordVisible; }
            set
            {
                isPasswordVisible = value; OnPropertyChanged(nameof(IsPasswordVisible));
            }
        }

        private bool isVisiblePasswordError;
        /// <summary>
        /// Property for use isVisiblePasswordError
        /// </summary>
        public bool IsVisiblePasswordError
        {
            get { return isVisiblePasswordError; }
            set
            {
                isVisiblePasswordError = value; OnPropertyChanged(nameof(IsVisiblePasswordError));
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



        private bool isVisibleZipCodeError;
        /// <summary>
        /// Property for use isVisibleZipCodeError 
        /// </summary>
        public bool IsVisibleZipCodeError
        {
            get { return isVisibleZipCodeError; }
            set
            {
                isVisibleZipCodeError = value; OnPropertyChanged(nameof(IsVisibleZipCodeError));
            }
        }

        private string passwordImage;
        /// <summary>
        /// Property for use password Image
        /// </summary>
        public string PasswordImage
        {
            get { return passwordImage; }
            set
            {
                passwordImage = value; OnPropertyChanged(nameof(PasswordImage));
            }
        }

        private bool isConfirmPasswordError;
        /// <summary>
        /// Property for Confirm password Error 
        /// </summary>
        /// 
        public bool IsConfirmPasswordError
        {
            get { return isConfirmPasswordError; }
            set { isConfirmPasswordError = value; OnPropertyChanged(nameof(IsConfirmPasswordError)); }
        }
        private bool isZipCodeError;
        /// <summary>
        /// Property for Zip Code Error 
        /// </summary>
        public bool IsZipCodeError
        {
            get { return isZipCodeError; }
            set { isZipCodeError = value; OnPropertyChanged(nameof(IsZipCodeError)); }
        }
        private bool isLoader;
        /// <summary>
        /// property use for isLoader 
        /// </summary>

        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private string emial_Address;
        /// <summary>
        /// Property for use emial_Address 
        /// </summary>
        public string Emial_Address
        {
            get { return emial_Address; }
            set
            {
                emial_Address = value; OnPropertyChanged(nameof(Emial_Address));
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
                OnPropertyChanged(nameof(CityName));
                if (!string.IsNullOrEmpty(cityName))
                {
                    CityErrorMessage = string.Empty;
                    IsVisibleCityNameError = false;
                }
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




        private string lastNameErrorMessage;
        /// <summary>
        /// Property for last name Error message
        /// </summary>
        public string LastNameErrorMessage
        {
            get { return lastNameErrorMessage; }
            set { lastNameErrorMessage = value; OnPropertyChanged(nameof(LastNameErrorMessage)); }
        }

        private bool isVisibleLastNameError;
        /// <summary>
        /// Property for use isVisibleLastNameError 
        /// </summary>
        public bool IsVisibleLastNameError
        {
            get { return isVisibleLastNameError; }
            set { isVisibleLastNameError = value; OnPropertyChanged(nameof(IsVisibleLastNameError)); }
        }


        private Uri pDF;
        /// <summary>
        /// Property for use Uri pDF 
        /// </summary>
        public Uri PDF
        {
            get { return pDF; }
            set { pDF = value; OnPropertyChanged(nameof(PDF)); }
        }
        private bool isPDF;

        public bool IsPDF
        {
            get { return isPDF; }
            set { isPDF = value; OnPropertyChanged(nameof(IsPDF)); }
        }

        #endregion

        #region Commands 

        private Command commandOpenPdf;
        /// <summary>
        /// This command use for show terms and condition pdf
        /// </summary>
        public Command CommandOpenPdf
        {
            get
            {
                if (commandOpenPdf == null)
                {
                    commandOpenPdf = new Command(async () =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {

                                if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Member)
                                {

                                    PDF = new Uri("http://docs.google.com/gview?embedded=true&url=" + APIHttpHelper.DocBaseURL + ConstantValues.UserPrivacyRightsPDF);

                                }
                                else if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                                {
                                    PDF = new Uri("http://docs.google.com/gview?embedded=true&url=" + APIHttpHelper.DocBaseURL + ConstantValues.NegotiatorTermConditionsPDF);
                                }
                                else { IsPDF = false; }

                                await Browser.OpenAsync(PDF, new BrowserLaunchOptions
                                {
                                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                                    TitleMode = BrowserTitleMode.Show,
                                    PreferredToolbarColor = Color.FromHex("2CD49C"),
                                    PreferredControlColor = Color.FromHex("ffffff")
                                }).ConfigureAwait(false);

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
                    });
                }
                return commandOpenPdf;
            }
        }

        private Command commandClosePDF;
        /// <summary>
        /// This command use for Close PDF
        /// </summary>
        public Command CommandClosePDF
        {
            get
            {
                if (commandClosePDF == null)
                {
                    commandClosePDF = new Command( () =>
                    {
                        try
                        {
                            IsPDF = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandClosePDF;
            }
        }

        private Command commandClose;
        /// <summary>
        /// This command use for commandClose
        /// </summary>
        public Command CommandClose
        {
            get
            {
                if (commandClose == null)
                {
                    commandClose = new Command(() =>
                    {
                        try
                        {
                            IsTermsofServicesVisible = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandClose;
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
                            bool profileEdit = Preferences.Get(ConstantValues.EditProfilePref, false);
                            if (profileEdit == true)
                            {
                                ClearFields();
                                Preferences.Set(ConstantValues.EditProfilePref, false);
                                await App.Current.MainPage.Navigation.PopAsync();

                            }
                            else
                            {
                                var result = App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.ConfirmExitMessage, ResourceValues.YesButtontext, ResourceValues.NoButtontext);
                                if (await result)
                                {
                                    ClearFields();
                                    await App.Current.MainPage.Navigation.PopAsync();
                                }
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

        private Command showPasswordCommand;
        /// <summary>
        /// This command use for process showPasswordCommand
        /// </summary>
        public Command ShowPasswordCommand
        {
            get
            {
                if (showPasswordCommand == null)
                {
                    showPasswordCommand = new Command( () =>
                    {
                        try
                        {
                            if (IsPasswordVisible)
                            {
                                PasswordImage = ConstantValues.HideImageName;
                                IsPasswordVisible = false;
                            }
                            else
                            {
                                PasswordImage = ConstantValues.ShowImageName;
                                IsPasswordVisible = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return showPasswordCommand;
            }
        }

        private Command commandOnSignUp;
        /// <summary>
        /// This command use for process commandOnSignUp details
        /// </summary>
        public Command CommandOnSignUp
        {
            get
            {
                if (commandOnSignUp == null)
                {
                    commandOnSignUp = new Command(async () =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                IsLoader = true;
                                if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Member)
                                {
                                    Preferences.Set(ConstantValues.IsRightsAndProvisionAcceptedPref, false);
                                    await UserRegistration();
                                }
                                else if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                                {
                                    Preferences.Set(ConstantValues.IsRightsAndProvisionAcceptedPref, false);
                                    NegotiatorRegistration();
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
                    });
                }
                return commandOnSignUp;
            }
        }


        private Command commandOnEdit;
        /// <summary>
        /// This command use for process edit button
        /// </summary>
        public Command CommandOnEdit
        {
            get
            {
                if (commandOnEdit == null)
                {
                    commandOnEdit = new Command(() =>
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
        /// This command use for process Cancel button
        /// </summary>
        public Command CommandOnCancel
        {
            get
            {
                if (commandOnCancel == null)
                {
                    commandOnCancel = new Command(() =>
                    {
                        try
                        {
                            IsCancelShow = false;
                            IsUpdateShow = false;
                            IsEditShow = true;
                            IsEnableField = false;
                            IsTextReadOnly = true;

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

        private Command commandOnUpdate;
        /// <summary>
        /// This command use for process Update button
        /// </summary>
        public Command CommandOnUpdate
        {
            get
            {
                if (commandOnUpdate == null)
                {
                    commandOnUpdate = new Command(async () =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Member)
                                {

                                    var result = await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.ConfirmUpdateProfileMessage, ResourceValues.OkButtontext, ResourceValues.CancelButtontext);
                                    if (result == true)
                                    {
                                        await UpdateUserRegistration();

                                    }

                                }
                                else if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                                {
                                    var result = await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.ConfirmUpdateProfileMessage, ResourceValues.OkButtontext, ResourceValues.CancelButtontext);
                                    if (result == true)
                                    {
                                        await UpdateNegotiatorRegistration();

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

                    });
                }
                return commandOnUpdate;
            }
        }
        private Command commandForBack;
        /// <summary>
        /// This command use for process commandForBack details
        /// </summary>
        public Command CommandForBack
        {
            get
            {
                if (commandForBack == null)
                {
                    commandForBack = new Command(async () =>
                    {
                        try
                        {
                            await App.Current.MainPage.Navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandForBack;
            }
        }

        private Command cmdChooseLanguage;
        /// <summary>
        /// This command use for choose the language
        /// </summary>
        public Command CommandOnChooseLanguage
        {
            get
            {
                if (cmdChooseLanguage == null)
                {
                    cmdChooseLanguage = new Command(async () =>
                    {
                        try
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new LanguageView());
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return cmdChooseLanguage;
            }
        }

        private Command cmdChooseSelectService;
        /// <summary>
        /// This command use for choose the select service
        /// </summary>
        public Command CommandOnSelectService
        {
            get
            {
                if (cmdChooseSelectService == null)
                {
                    cmdChooseSelectService = new Command(async () =>
                    {
                        try
                        {

                            await App.Current.MainPage.Navigation.PushAsync(new SelectServiceView());
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return cmdChooseSelectService;
            }
        }

        private Command commandOnGetState;
        /// <summary>
        /// command for Get State 
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

        private Command commandOnGetUser;
        /// <summary>
        /// command for Get User 
        /// </summary>
        public Command CommandOnGetUser
        {
            get
            {
                if (commandOnGetUser == null)
                {
                    commandOnGetUser = new Command(() =>
                    {
                        try
                        {
                            if (ListOfUserType == null)
                            {
                                UserTypeData(false);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }

                    });
                }
                return commandOnGetUser;
            }

        }
        #endregion

        #region Methods


        /// <summary>
        /// validat control for User
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserControls()
        {
            var mobileNumberText = string.Empty;
            var mobileNumberCall = string.Empty;
            if (!string.IsNullOrEmpty(MobileNumberforText))
            {
                mobileNumberText = MobileNumberforText.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
            }
            if (!string.IsNullOrEmpty(MobileNumberforLiveCalls))
            {
                mobileNumberCall = MobileNumberforLiveCalls.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
            }
            if (UserTypeValue == null)
            {
                UserTypeErrorMessage = ResourceValues.SelectUserTypeErrorMessage;
                IsVisibleuserTypeErrorMessage = true;
                return false;
            }
            else if (UserTypeValue.UserTypeId == 0)
            {
                UserTypeErrorMessage = ResourceValues.SelectUserTypeErrorMessage;
                IsVisibleuserTypeErrorMessage = true;
                return false;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                FirstNameErrorMessage = ResourceValues.FirstNameErrorMessage;
                IsVisibleFirstNameError = true;
                return false;
            }
            if (!IsCall && !IsText && !IsEmail)
            {
                PrefferdErrorMessage = ResourceValues.PrefferedContactMessage;
                IsPrefferdErrorMessage = true;
                return false;
            }
            if (string.IsNullOrEmpty(MobileNumberforText))
            {
                MobileNumberforTextErrorMessage = ResourceValues.MobilenumberforTextErrorMessage;
                IsMobileNumberforTextError = true;
                return false;
            }
            if (mobileNumberText.Length < 10)
            {
                MobileNumberforTextErrorMessage = ResourceValues.ValidMobileMessage;
                IsMobileNumberforTextError = true;
                return false;
            }

            if (!String.IsNullOrEmpty(MobileNumberforLiveCalls))
            {
                if (mobileNumberCall.Length < 10)
                {
                    MobileNumberforLIVECALLSErrorMessage = ResourceValues.ValidMobileMessage;
                    IsMobileNumberforLIVECALLSError = true;
                    return false;
                }
                else
                {
                    MobileNumberforLIVECALLSErrorMessage = string.Empty;
                    IsMobileNumberforLIVECALLSError = false;
                }
            }


            if (IsCall)
            {
                if (!String.IsNullOrEmpty(MobileNumberforLiveCalls))
                {
                    if (mobileNumberCall.Length < 10)
                    {
                        MobileNumberforLIVECALLSErrorMessage = ResourceValues.ValidMobileMessage;
                        IsMobileNumberforLIVECALLSError = true;
                        return false;
                    }
                    else
                    {
                        MobileNumberforLIVECALLSErrorMessage = string.Empty;
                        IsMobileNumberforLIVECALLSError = false;
                    }
                }
                else
                {

                    MobileNumberforLIVECALLSErrorMessage = ResourceValues.ValidMobileMessage;
                    IsMobileNumberforLIVECALLSError = true;
                    return false;
                }
            }
            else
            {
                MobileNumberforLIVECALLSErrorMessage = string.Empty;
                IsMobileNumberforLIVECALLSError = false;
            }


            if (string.IsNullOrEmpty(Email))
            {
                EmailErrorMessage = ResourceValues.EmailErrorMessage;
                IsVisibleEmailError = true;
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
            bool profileEdit = Preferences.Get(ConstantValues.EditProfilePref, false);
            if (profileEdit == false)
            {
                if (string.IsNullOrEmpty(Password))
                {
                    PasswordErrorMessage = ResourceValues.PasswordErrorMessage;
                    IsVisiblePasswordError = true;
                    return false;
                }
                if (Password.Length < 6)
                {
                    PasswordErrorMessage = ResourceValues.PasswordErrorMessage;
                    IsVisiblePasswordError = true;
                    return false;
                }
                if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    ConfirmPasswordErrorMessage = ResourceValues.ConfirmPasswordErrorMessage;
                    IsConfirmPasswordError = true;
                    return false;
                }
                if (ConfirmPassword != Password)
                {
                    ConfirmPasswordErrorMessage = ResourceValues.ConfirmPasswordNotMatchErrorMessage;
                    IsConfirmPasswordError = true;
                    return false;
                }
                if (!IsAccepted)
                {
                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(ResourceValues.TermsAndConditionErrorMessage));

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// validat control for negotiator
        /// </summary>
        /// <returns></returns>
        private bool ValidateNegotiatorControls()
        {
            try
            {
                var mobileNumberText = string.Empty;
                var mobileNumberCall = string.Empty;
                if (!string.IsNullOrEmpty(MobileNumberforText))
                {
                    mobileNumberText = MobileNumberforText.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
                }
                if (!string.IsNullOrEmpty(MobileNumberforLiveCalls))
                {
                    mobileNumberCall = MobileNumberforLiveCalls.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
                }

                if (UserTypeValue == null)
                {
                    UserTypeErrorMessage = ResourceValues.SelectUserTypeErrorMessage;
                    IsVisibleuserTypeErrorMessage = true;
                    return false;
                }
                else if (UserTypeValue.UserTypeId == 0)
                {
                    UserTypeErrorMessage = ResourceValues.SelectUserTypeErrorMessage;
                    IsVisibleuserTypeErrorMessage = true;
                    return false;
                }
                if (string.IsNullOrEmpty(FirstName))
                {
                    FirstNameErrorMessage = ResourceValues.FirstNameErrorMessage;
                    IsVisibleFirstNameError = true;
                    return false;
                }
                if (IsDisplayNickName == 1)
                {
                    if (string.IsNullOrEmpty(NickName))
                    {
                        NickNameErrorMessage = ResourceValues.NickNameErrerMessage;
                        isVisibleNickNameError = true;
                        return false;
                    }
                    else
                    {
                        NickNameErrorMessage = string.Empty;
                        IsVisibleNickNameError = false;
                    }
                }
                else
                {
                    NickNameErrorMessage = string.Empty;
                    IsVisibleNickNameError = false;
                }
                if (string.IsNullOrEmpty(MobileNumberforText))
                {
                    MobileNumberforTextErrorMessage = ResourceValues.MobilenumberforTextErrorMessage;
                    IsMobileNumberforTextError = true;
                    return false;
                }
                if (mobileNumberText.Length < 10)
                {
                    MobileNumberforTextErrorMessage = ResourceValues.ValidMobileMessage;
                    IsMobileNumberforTextError = true;
                    return false;
                }
                if (!String.IsNullOrEmpty(MobileNumberforLiveCalls))
                {
                    if (mobileNumberCall.Length < 10)
                    {
                        MobileNumberforLIVECALLSErrorMessage = ResourceValues.ValidMobileMessage;
                        IsMobileNumberforLIVECALLSError = true;
                        return false;
                    }
                    else
                    {
                        MobileNumberforLIVECALLSErrorMessage = string.Empty;
                        IsMobileNumberforLIVECALLSError = false;
                    }
                }


                if (string.IsNullOrEmpty(Email))
                {
                    EmailErrorMessage = ResourceValues.EmailErrorMessage;
                    IsVisibleEmailError = true;
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
                bool profileEdit = Preferences.Get(ConstantValues.EditProfilePref, false);
                if (profileEdit == false)
                {
                    if (string.IsNullOrEmpty(Password))
                    {
                        PasswordErrorMessage = ResourceValues.PasswordErrorMessage;
                        IsVisiblePasswordError = true;
                        return false;
                    }
                    if (string.IsNullOrEmpty(ConfirmPassword))
                    {
                        ConfirmPasswordErrorMessage = ResourceValues.ConfirmPasswordErrorMessage;
                        IsConfirmPasswordError = true;
                        return false;
                    }
                    if (Password.Length < 6)
                    {
                        PasswordErrorMessage = ResourceValues.PasswordErrorMessage;
                        IsVisiblePasswordError = true;
                        return false;
                    }
                    if (ConfirmPassword != Password)
                    {
                        ConfirmPasswordErrorMessage = ResourceValues.ConfirmPasswordNotMatchErrorMessage;
                        IsConfirmPasswordError = true;
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(SelectService))
                {
                    SelectServiceErrorMessage = ResourceValues.SelectServiceMessage;
                    IsVisibleSelectServiceError = true;
                    return false;
                }


                if (string.IsNullOrEmpty(Language))
                {
                    LanguageErrorMessage = ResourceValues.SelectLanguageMessage;
                    IsVisibleLanguageError = true;
                    return false;
                }
                profileEdit = Preferences.Get(ConstantValues.EditProfilePref, false);
                if (profileEdit == false)
                {
                    if (!IsAccepted)
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(ResourceValues.TermsAndConditionErrorMessage));

                        return false;
                    }
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
        /// Method for User registration
        /// </summary>
        /// <returns></returns>
        private async Task UserRegistration()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    if (ValidateUserControls())
                    {
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            IsLoader = true;
                        });


                        PreferredType preferredType = new PreferredType();
                        List<PreferredType> lstPreferredType = new List<PreferredType>();
                        var userRegistrationRequestModel = new UserRegistrationRequestModel();
                        userRegistrationRequestModel.FirstName = FirstName;
                        userRegistrationRequestModel.LastName = LastName;
                        userRegistrationRequestModel.Email = Email;
                        userRegistrationRequestModel.MobileText = MobileNumberforText.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
                        userRegistrationRequestModel.MobileCall = MobileNumberforLiveCalls != null ? MobileNumberforLiveCalls.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString() : string.Empty;
                        userRegistrationRequestModel.AddressLine1 = AddressLine1;
                        userRegistrationRequestModel.AddressLine2 = AddressLine2;
                        userRegistrationRequestModel.city = CityName;
                        userRegistrationRequestModel.state = StateValue.code;
                        userRegistrationRequestModel.ZipCode = ZipCode;
                        userRegistrationRequestModel.Password = password;
                        if (IsCall)
                        {
                            preferredType = new PreferredType();
                            preferredType.typeId = 2;
                            lstPreferredType.Add(preferredType);
                        }
                        if (IsText)
                        {
                            preferredType = new PreferredType();
                            preferredType.typeId = 1;
                            lstPreferredType.Add(preferredType);
                        }
                        if (IsEmail)
                        {
                            preferredType = new PreferredType();
                            preferredType.typeId = 3;
                            lstPreferredType.Add(preferredType);
                        }
                        userRegistrationRequestModel.preferredType = lstPreferredType;
                        var userRegistrationResponse = await apiServices.UserRegistrationDetails(userRegistrationRequestModel);
                        if (userRegistrationResponse != null)
                        {
                            if (userRegistrationResponse.code == 200)
                            {
                                ClearFields();
                                Device.BeginInvokeOnMainThread( () =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(userRegistrationResponse.message));
                                });

                                QBidHelper.NavigatePage = "UserLoginView";
                                Preferences.Set(ConstantValues.CustomerStripeIdPref, userRegistrationResponse.data.customerId);
                                Preferences.Set(ConstantValues.TokenKeyText, userRegistrationResponse.data.token);
                                await Task.Delay(1000);
                                await Application.Current.MainPage.Navigation.PushAsync(new CardDetailView());

                            }
                            else
                            {
                                var err = string.Empty;
                                if (Convert.ToString(userRegistrationResponse.message).ToLower() == "failed")
                                {
                                    err = Convert.ToString(userRegistrationResponse.error);
                                }
                                DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(userRegistrationResponse.message) + "\n"+ err );
                            }
                        }
                        IsLoader = false;
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
        /// Method for Negotiator registration
        /// </summary>
        /// <returns></returns>
        private async Task NegotiatorRegistration()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    if (ValidateNegotiatorControls())
                    {
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            IsLoader = true;
                        });
                        var negotiatorRegistrationRequestModel = new NegotiatorRegistrationRequestModel();
                        negotiatorRegistrationRequestModel.FirstName = FirstName;
                        negotiatorRegistrationRequestModel.LastName = LastName;
                        negotiatorRegistrationRequestModel.CompanyName = CompanyName;
                        negotiatorRegistrationRequestModel.Retired = IsRetired;
                        negotiatorRegistrationRequestModel.SelfEmployed = IsSelfEmployed;
                        negotiatorRegistrationRequestModel.NickName = NickName;
                        negotiatorRegistrationRequestModel.displayStatus = IsDisplayNickName;
                        negotiatorRegistrationRequestModel.MobileText = MobileNumberforText.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
                        negotiatorRegistrationRequestModel.MobileCall = MobileNumberforLiveCalls != null ? MobileNumberforLiveCalls.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString() : string.Empty;
                        negotiatorRegistrationRequestModel.Email = Email;
                        negotiatorRegistrationRequestModel.AddressLine1 = AddressLine1;
                        negotiatorRegistrationRequestModel.AddressLine2 = AddressLine2;
                        negotiatorRegistrationRequestModel.city = CityName;
                        negotiatorRegistrationRequestModel.state = StateValue.code;
                        negotiatorRegistrationRequestModel.ZipCode = ZipCode;
                        negotiatorRegistrationRequestModel.Password = password;
                        List<Service> Service_Obj = new List<Service>();
                        if (QBidHelper.SelectServiceDetails != null)
                        {
                            foreach (var item in QBidHelper.SelectServiceDetails)
                            {
                                if (item != null)
                                {
                                    if (item.IsInYearVisible)
                                    {
                                        Service Add = new Service();
                                        Add.serviceId = item.Id;
                                        Add.experienceTime = (!String.IsNullOrEmpty(item.InYearText) ? item.InYearText.ToString() : (0).ToString());
                                        Service_Obj.Add(Add);
                                    }
                                }
                            }
                        }
                        List<Language> Languages_Obj = new List<Language>();
                        string[] languages = Language.Split(',');
                        if (languages != null)
                        {
                            foreach (var item in QBidHelper.LanguageDetails)
                            {
                                if (item.IsCheckedLang)
                                {
                                    Language Add = new Language();
                                    Add.languageId = (Convert.ToInt32(item.Id) > 0 ? Convert.ToInt32(item.Id) : 0);
                                    Languages_Obj.Add(Add);
                                }
                            }
                        }

                        negotiatorRegistrationRequestModel.Service = Service_Obj;
                        negotiatorRegistrationRequestModel.Language = Languages_Obj;


                        var userRegistrationResponse = await apiServices.NegoTiatorRegistrationDetails(negotiatorRegistrationRequestModel);
                        if (userRegistrationResponse != null)
                        {
                            if (userRegistrationResponse.code == 200)
                            {
                                ClearFields();
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(userRegistrationResponse.message));

                                    Preferences.Set(ConstantValues.TokenKeyText, userRegistrationResponse.data.token);
                                    QBidHelper.NavigatePage = "UserLoginView";
                                    await Task.Delay(1000);
                                    Preferences.Set(ConstantValues.IsNegotiatorAccountAddedPref, 0);
                                    await Application.Current.MainPage.Navigation.PushAsync(new NegotiatorBankDetailView());
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread( () =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(userRegistrationResponse.message) + "\n" + userRegistrationResponse.error);
                                });
                            }
                        }

                        IsLoader = false;
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
        /// Method for User Type
        /// </summary>
        /// <returns></returns>       
        public async Task UserTypeData(bool isEditingProfile)
        {
            try
            {

                var data = await UtilHelper.GetUserTypeFromAPI().ConfigureAwait(true);


                if (data != null && data.Count > 0)
                {
                    ListOfUserType = new ObservableCollection<UserTypeModel>(data.Select(a => new UserTypeModel()
                    {
                        UserTypeId = a.id,
                        UserType = a.name
                    }).ToList());

                    if (isEditingProfile)
                    {
                        var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
                        if (userTypeId == (int)UtilHelper.UserRoleType.Member)   //1=member
                        {
                            UserTypeValue = ListOfUserType[0];
                            IsNegotiator = false;
                            IsMember = true;
                            IsVisibleuserTypeErrorMessage = false;
                            Task.Run(async () =>
                            {

                                await GetUserProfile();
                            });

                        }
                        else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)   //2=negotiator
                        {
                            UserTypeValue = ListOfUserType[1];
                            IsNegotiator = true;
                            IsMember = false;
                            Task.Run(async () =>
                            {

                                await GetUserProfile();
                            });
                            // Get
                        }
                    }
                 
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        /// <summary>
        /// Method for clear fields
        /// </summary>
        /// <returns></returns>
        private void ClearFields()
        {
            try
            {
                FirstName = string.Empty;
                LastName = string.Empty;
                CompanyName = string.Empty;
                IsRetired = 0;
                IsSelfEmployed = 0;
                NickName = string.Empty;
                IsDisplayNickName = 0;
                MobileNumberforText = string.Empty;
                MobileNumberforLiveCalls = string.Empty;
                MobileNumberforTextErrorMessage = string.Empty;
                MobileNumberforLIVECALLSErrorMessage = string.Empty;
                Email = string.Empty;
                AddressLine1 = string.Empty;
                AddressLine2 = string.Empty;
                CityName = string.Empty;
                StateValue = null;
                ZipCode = string.Empty;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
                Service = string.Empty;
                Language = string.Empty;
                IsAccepted = false;
                UserTypeErrorMessage = string.Empty;
                IsVisibleuserTypeErrorMessage = false;
                IsVisibleFirstNameError = false;
                IsVisibleLastNameError = false;
                IsMobileNumberforTextError = false;
                IsMobileNumberforLIVECALLSError = false;
                IsVisibleEmailError = false;
                IsVisibleAddressLine1Error = false;
                IsVisibleAddressLine2Error = false;
                IsVisibleZipCodeError = false;
                IsVisiblePasswordError = false;
                IsConfirmPasswordError = false;
                IsVisibleSelectServiceError = false;
                IsVisibleLanguageError = false;
                IsVisibleTermsAndConditionError = false;
                QBidHelper.SelectServiceDetails = null;
                QBidHelper.LanguageDetails = null;
                QBidHelper.GetRegistrationAPILanguageDetail = null;
                QBidHelper.GetRegistrationAPIServiceDetail = null;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        /// <summary>
        /// Method for Get State data
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
                        IsLoader = true;
                        var responce = await aPIService.GetStateAPI().ConfigureAwait(true);
                        if (responce.data != null && responce.data.Count > 0)
                        {
                            var y = new ObservableCollection<State>(responce.data.OrderBy(x => x.code));
                            ListOfState = y;
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
        /// Method for Get User / negotiator Profile
        /// </summary>
        /// <returns></returns>
        public async Task GetUserProfile()
        {
            await GetState();

            SelectServiceList = new ObservableCollection<ServiceModel>();
            APIService aPIServices = new APIService();
            var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
            if (userTypeId == (int)UtilHelper.UserRoleType.Member)
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
                        UserDetailResponse response = new UserDetailResponse();
                        response = await aPIServices.GetUserDetail();
                        if(response !=null && response.success)
                        {
                            if (response.data != null)
                            {
                                FirstName = (response.data.firstName != null ? response.data.firstName : string.Empty);
                                LastName = (response.data.lastName != null ? response.data.lastName : string.Empty);
                                Email = (response.data.email != null ? response.data.email : string.Empty);
                                ZipCode = response.data.zipCode;
                                MobileNumberforText = (response.data.mobileText != null ? response.data.mobileText : string.Empty);
                                MobileNumberforLiveCalls = (response.data.mobileCall != null ? response.data.mobileCall : string.Empty);
                                AddressLine1 = (response.data.addressLine1 != null ? response.data.addressLine1 : string.Empty);
                                AddressLine2 = (response.data.addressLine2 != null ? response.data.addressLine2 : string.Empty);
                                CityName = (response.data.city != null ? response.data.city : string.Empty);

                                if (response.data.ContactPreference.Count > 0)
                                {
                                    IsText = false;
                                    IsCall = false;
                                    IsEmail = false;
                                    foreach (var item in response.data.ContactPreference)
                                    {
                                        switch (item.name)
                                        {
                                            case "Text":
                                                IsText = true;
                                                break;
                                            case "Call":
                                                IsCall = true;
                                                break;
                                            default:
                                                IsEmail = true;
                                                break;

                                        }
                                    }

                                }
                                if (ListOfState != null && ListOfState.Count > 0)
                                {
                                    StateValue = ListOfState.Where(a => a.code.ToLower() == response.data.state.ToLower()).FirstOrDefault();

                                }
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread( () =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(response.message) );
                                });
                            }
                        }
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            IsLoader = false;
                        });
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
            else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)
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
                        ServiceModel select = new ServiceModel();
                        NegotiatorDetailResponce response = new NegotiatorDetailResponce();
                        response = await aPIServices.GetNegotiatorDetail();
                        if(response !=null )
                        {
                            if (response.data != null)
                            {
                                FirstName = (response.data.firstName != null ? response.data.firstName : string.Empty);
                                LastName = (response.data.lastName != null ? response.data.lastName : string.Empty);
                                Email = (response.data.email != null ? response.data.email : string.Empty);
                                ZipCode = response.data.zipCode;
                                CompanyName = (response.data.companyName != null ? response.data.companyName : string.Empty);
                                NickName = (response.data.nickName != null ? response.data.nickName : string.Empty);
                                IsDisplayNickName = response.data.displayStatus;
                                MobileNumberforText = (response.data.mobileText != null ? response.data.mobileText : string.Empty);
                                MobileNumberforLiveCalls = (response.data.mobileCall != null ? response.data.mobileCall : string.Empty);
                                AddressLine1 = (response.data.addressLine1 != null ? response.data.addressLine1 : string.Empty);
                                AddressLine2 = (response.data.addressLine2 != null ? response.data.addressLine2 : string.Empty);
                                IsRetired = response.data.retired;
                                IsSelfEmployed = response.data.selfEmployed;

                                CityName = (response.data.city != null ? response.data.city : string.Empty);

                                if (response.data.services != null)
                                {
                                    QBidHelper.GetRegistrationAPIServiceDetail = response.data.services;
                                    string getService = string.Empty;
                                    response.data.services.ForEach(item =>
                                    {
                                        getService = getService + "," + item.serviceName;
                                    });
                                    SelectService = getService.Substring(1, getService.Length - 1);
                                }



                                if (response.data.languages != null)
                                {
                                    QBidHelper.GetRegistrationAPILanguageDetail = response.data.languages;
                                    string getLanguage = string.Empty;
                                    response.data.languages.ForEach(item =>
                                    {
                                        getLanguage = getLanguage + "," + item.languageName;
                                    });
                                    Language = getLanguage.Substring(1, getLanguage.Length - 1);

                                }

                                if (ListOfState != null && ListOfState.Count > 0)
                                {
                                    StateValue = ListOfState.Where(a => a.code.ToLower() == response.data.state.ToLower()).FirstOrDefault();

                                }

                            }
                            else
                            {
                                DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(response.message));
                            }

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ApiErrorMessage);
                            });

                        }
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            IsLoader = false;
                        });
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

        }

        /// <summary>
        /// Method for Update User registration
        /// </summary>
        /// <returns></returns>
        private async Task UpdateUserRegistration()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    if (ValidateUserControls())
                    {
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            IsLoader = true;
                        });
                        PreferredTypeRequest preferredType = new PreferredTypeRequest();
                        List<PreferredTypeRequest> lstPreferredType = new List<PreferredTypeRequest>();
                        var updateUserDetailRequest = new UpdateUserDetailRequest();
                        updateUserDetailRequest.firstName = FirstName;
                        updateUserDetailRequest.lastName = LastName;

                        updateUserDetailRequest.mobileText = MobileNumberforText.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
                        updateUserDetailRequest.mobileCall = MobileNumberforLiveCalls != null ? MobileNumberforLiveCalls.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString() : string.Empty;
                        updateUserDetailRequest.addressLine1 = AddressLine1;
                        updateUserDetailRequest.addressLine2 = AddressLine2;
                        updateUserDetailRequest.city = CityName;
                        updateUserDetailRequest.state = StateValue.code;
                        updateUserDetailRequest.zipCode = ZipCode;

                        if (IsCall)
                        {
                            preferredType = new PreferredTypeRequest();
                            preferredType.typeId = 2;
                            lstPreferredType.Add(preferredType);
                        }
                        if (IsText)
                        {
                            preferredType = new PreferredTypeRequest();
                            preferredType.typeId = 1;
                            lstPreferredType.Add(preferredType);
                        }
                        if (IsEmail)
                        {
                            preferredType = new PreferredTypeRequest();
                            preferredType.typeId = 3;
                            lstPreferredType.Add(preferredType);
                        }
                        updateUserDetailRequest.preferredType = lstPreferredType;
                        var userRegistrationResponse = await apiServices.UpdateUserRegistrationDetails(updateUserDetailRequest);
                        if (userRegistrationResponse != null)
                        {
                            if (userRegistrationResponse.code == 200)
                            {
                                IsLoader = false;
                                ClearFields();
                                Device.BeginInvokeOnMainThread( () =>
                                {
                                    DependencyService.Get<IToastMessage>().ShortAlert(Convert.ToString(userRegistrationResponse.message));
                                });
                                await Task.Delay(3000);
                                App.Current.MainPage.Navigation.PopAsync();
                            }
                            else
                            {
                                DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(userRegistrationResponse.message) + "\n"+ userRegistrationResponse.error );
                            }
                        }

                        IsLoader = false;
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
        /// Method for Update negotiator registration
        /// </summary>
        /// <returns></returns>
        private async Task UpdateNegotiatorRegistration()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    if (ValidateNegotiatorControls())
                    {
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            IsLoader = true;
                        });
                        var updateNegotiatorDetailRequest = new UpdateNegotiatorDetailRequest();
                        updateNegotiatorDetailRequest.firstName = FirstName;
                        updateNegotiatorDetailRequest.lastName = LastName;
                        updateNegotiatorDetailRequest.companyName = CompanyName;
                        updateNegotiatorDetailRequest.retired = IsRetired;
                        updateNegotiatorDetailRequest.selfEmployee = IsSelfEmployed;
                        updateNegotiatorDetailRequest.nickName = NickName;
                        updateNegotiatorDetailRequest.displayStatus = IsDisplayNickName;
                        updateNegotiatorDetailRequest.mobileText = MobileNumberforText.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString();
                        updateNegotiatorDetailRequest.mobileCall = MobileNumberforLiveCalls != null ? MobileNumberforLiveCalls.Replace(ConstantValues.BracketRoundFore, string.Empty).Replace(ConstantValues.BracketRoundBack, string.Empty).Replace(ConstantValues.Hyphen, string.Empty).Replace(ConstantValues.OneSpace, string.Empty).ToString() : string.Empty;
                        updateNegotiatorDetailRequest.email = Email;
                        updateNegotiatorDetailRequest.addressLine1 = AddressLine1;
                        updateNegotiatorDetailRequest.addressLine2 = AddressLine2;
                        updateNegotiatorDetailRequest.city = CityName;
                        updateNegotiatorDetailRequest.state = StateValue.code;
                        updateNegotiatorDetailRequest.zipCode = ZipCode;

                        List<ServiceRequest> Service_Obj = new List<ServiceRequest>();
                        if (QBidHelper.SelectServiceDetails != null)
                        {
                            foreach (var item in QBidHelper.SelectServiceDetails)
                            {
                                if (item != null)
                                {
                                    if (item.IsInYearVisible)
                                    {
                                        ServiceRequest Add = new ServiceRequest();
                                        Add.serviceId = item.Id;

                                        Add.experienceTime = (!String.IsNullOrEmpty(item.InYearText) ? item.InYearText : (0).ToString());
                                        Service_Obj.Add(Add);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in QBidHelper.GetRegistrationAPIServiceDetail)
                            {
                                if (item != null)
                                {
                                    ServiceRequest Add = new ServiceRequest();
                                    Add.serviceId = item.id;

                                    Add.experienceTime = (!String.IsNullOrEmpty(item.experianceTime) ? item.experianceTime : (0).ToString());
                                    Service_Obj.Add(Add);
                                }
                            }
                        }
                        List<LanguageRequest> Languages_Obj = new List<LanguageRequest>();

                        if (QBidHelper.LanguageDetails != null)
                        {
                            string[] languages = Language.Split(',');
                            if (languages != null)
                            {
                                foreach (var item in QBidHelper.LanguageDetails)
                                {
                                    if (item.IsCheckedLang)
                                    {
                                        LanguageRequest Add = new LanguageRequest();
                                        Add.languageId = (Convert.ToInt32(item.Id) > 0 ? Convert.ToInt32(item.Id) : 0);
                                        Languages_Obj.Add(Add);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in QBidHelper.GetRegistrationAPILanguageDetail)
                            {
                                if (item != null)
                                {
                                    LanguageRequest Add = new LanguageRequest();
                                    Add.languageId = (Convert.ToInt32(item.id) > 0 ? Convert.ToInt32(item.id) : 0);
                                    Languages_Obj.Add(Add);
                                }
                            }
                        }




                        updateNegotiatorDetailRequest.service = Service_Obj;
                        updateNegotiatorDetailRequest.language = Languages_Obj;


                        var userRegistrationResponse = await apiServices.UpdateNegotiatorRegistrationDetails(updateNegotiatorDetailRequest);
                        if (userRegistrationResponse != null)
                        {
                            if (userRegistrationResponse.code == 200)
                            {
                                IsLoader = false;
                                ClearFields();
                                Device.BeginInvokeOnMainThread( () =>
                                {
                                    DependencyService.Get<IToastMessage>().ShortAlert(Convert.ToString(userRegistrationResponse.message));
                                });
                                await Task.Delay(3000);
                                App.Current.MainPage.Navigation.PopAsync();
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread( () =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(userRegistrationResponse.message) + "\n"+ userRegistrationResponse.error);
                                });
                            }
                        }
                        IsLoader = false;

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
