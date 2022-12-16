using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class QuotationFormPageViewModel : BindableObject
    {
        #region Constructor

        APIService apiServices = null;
        public QuotationFormPageViewModel()
        {
            GetState().ConfigureAwait(false);
            GetServiceType().ConfigureAwait(false);
            IsShowButton = false;
        }

        #endregion

        #region Private Properties
        private string test;
        #endregion

        #region Public Properties

        private bool isShowButton;
        /// <summary>
        /// Propert for isShowButton
        /// </summary>
        public bool IsShowButton
        {
            get { return isShowButton; }
            set { isShowButton = value; OnPropertyChanged(nameof(IsShowButton)); }
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
        /// Property for isVisibleStateNameError
        /// </summary>
        public bool IsVisibleStateNameError
        {
            get { return isVisibleStateNameError; }
            set { isVisibleStateNameError = value; OnPropertyChanged(nameof(IsVisibleStateNameError)); }
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
        private string saleRepresentativeName;
        /// <summary>
        /// Property for saleRepresentativeName
        /// </summary>
        public string SaleRepresentativeName
        {
            get { return saleRepresentativeName; }
            set
            {
                saleRepresentativeName = value;
                OnPropertyChanged(nameof(SaleRepresentativeName));
                ValidateSaleRepresentativeName();
            }
        }


        private string errorTextRepresentativeName;
        /// <summary>
        /// Property for errorTextRepresentativeName
        /// </summary>
        public string ErrorTextRepresentativeName
        {
            get { return errorTextRepresentativeName; }
            set { errorTextRepresentativeName = value; OnPropertyChanged(nameof(ErrorTextRepresentativeName)); }
        }

        private bool isVisibleSaleRepresentavtiveMsgErr;
        /// <summary>
        /// Property for isVisibleSaleRepresentavtiveMsgErr
        /// </summary>
        public bool IsVisibleSaleRepresentavtiveMsgErr
        {
            get { return isVisibleSaleRepresentavtiveMsgErr; }
            set
            {
                isVisibleSaleRepresentavtiveMsgErr = value;
                OnPropertyChanged(nameof(IsVisibleSaleRepresentavtiveMsgErr));
            }
        }

        private string city;
        /// <summary>
        /// Property for city
        /// </summary>
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
                ValidateCity();
            }
        }


        private string errorTextCity;
        /// <summary>
        /// Property for errorTextCity
        /// </summary>
        public string ErrorTextCity
        {
            get { return errorTextCity; }
            set { errorTextCity = value; OnPropertyChanged(nameof(ErrorTextCity)); }
        }

        private bool isVisibleCityMsgErr;
        /// <summary>
        /// Property for isVisibleCityMsgErr
        /// </summary>
        public bool IsVisibleCityMsgErr
        {
            get { return isVisibleCityMsgErr; }
            set
            {
                isVisibleCityMsgErr = value;
                OnPropertyChanged(nameof(IsVisibleCityMsgErr));
            }
        }


        private string vendorStreetAddress;
        /// <summary>
        /// Property for vendorStreetAddress
        /// </summary>
        public string VendorStreetAddress
        {
            get { return vendorStreetAddress; }
            set
            {
                vendorStreetAddress = value;
                OnPropertyChanged(nameof(VendorStreetAddress));
                ValidateVendorStreetAddress();
            }
        }


        private string errorTextVendorStreetAddress;
        /// <summary>
        /// Property for errorTextVendorStreetAddress
        /// </summary>
        public string ErrorTextVendorStreetAddress
        {
            get { return errorTextVendorStreetAddress; }
            set { errorTextVendorStreetAddress = value; OnPropertyChanged(nameof(ErrorTextVendorStreetAddress)); }
        }

        private bool isVisibleVendorStreetAddressMsgErr;
        /// <summary>
        /// Property for isVisibleVendorStreetAddressMsgErr
        /// </summary>
        public bool IsVisibleVendorStreetAddressMsgErr
        {
            get { return isVisibleVendorStreetAddressMsgErr; }
            set
            {
                isVisibleVendorStreetAddressMsgErr = value;
                OnPropertyChanged(nameof(IsVisibleVendorStreetAddressMsgErr));
            }
        }


        private string enterCompanyVendorName;
        /// <summary>
        /// Property for enterCompanyVendorName
        /// </summary>
        public string EnterCompanyVendorName
        {
            get { return enterCompanyVendorName; }
            set
            {
                enterCompanyVendorName = value;
                OnPropertyChanged(nameof(EnterCompanyVendorName));
                ValidateEnterCompanyVendorName();
            }
        }


        private string errorTextEnterCompanyVendorName;
        /// <summary>
        /// Property for errorTextEnterCompanyVendorName
        /// </summary>
        public string ErrorTextEnterCompanyVendorName
        {
            get { return errorTextEnterCompanyVendorName; }
            set { errorTextEnterCompanyVendorName = value; OnPropertyChanged(nameof(ErrorTextEnterCompanyVendorName)); }
        }

        private bool isVisibleEnterCompanyVendorNameMsgErr;
        /// <summary>
        /// Property for isVisibleEnterCompanyVendorNameMsgErr
        /// </summary>
        public bool IsVisibleEnterCompanyVendorNameMsgErr
        {
            get { return isVisibleEnterCompanyVendorNameMsgErr; }
            set
            {
                isVisibleEnterCompanyVendorNameMsgErr = value;
                OnPropertyChanged(nameof(IsVisibleEnterCompanyVendorNameMsgErr));
            }
        }


        private string workRepairOrder;
        /// <summary>
        /// Property for workRepairOrder
        /// </summary>
        public string WorkRepairOrder
        {
            get { return workRepairOrder; }
            set
            {
                workRepairOrder = value;
                OnPropertyChanged(nameof(WorkRepairOrder));
                ValidateEnterWorkRepairOrder();
            }
        }


        private string errorTextWorkRepairOrder;
        /// <summary>
        /// Property for errorTextWorkRepairOrder
        /// </summary>
        public string ErrorTextWorkRepairOrder
        {
            get { return errorTextWorkRepairOrder; }
            set { errorTextWorkRepairOrder = value; OnPropertyChanged(nameof(ErrorTextWorkRepairOrder)); }
        }

        private bool isVisibleWorkRepairOrderMsgErr;
        /// <summary>
        /// Property for isVisibleWorkRepairOrderMsgErr
        /// </summary>
        public bool IsVisibleWorkRepairOrderMsgErr
        {
            get { return isVisibleWorkRepairOrderMsgErr; }
            set
            {
                isVisibleWorkRepairOrderMsgErr = value;
                OnPropertyChanged(nameof(IsVisibleWorkRepairOrderMsgErr));
            }
        }

        private string repairOrServiceItemsNeeded;
        /// <summary>
        /// Property for repairOrServiceItemsNeeded
        /// </summary>
        public string RepairOrServiceItemsNeeded
        {
            get { return repairOrServiceItemsNeeded; }
            set
            {
                repairOrServiceItemsNeeded = value;
                OnPropertyChanged(nameof(RepairOrServiceItemsNeeded));
                ValidateEnterRepairOrServiceItemsNeeded();
            }
        }


        private string errorTextRepairOrServiceItemsNeeded;
        /// <summary>
        /// Property for errorTextRepairOrServiceItemsNeeded
        /// </summary>
        public string ErrorTextRepairOrServiceItemsNeeded
        {
            get { return errorTextRepairOrServiceItemsNeeded; }
            set { errorTextRepairOrServiceItemsNeeded = value; OnPropertyChanged(nameof(ErrorTextRepairOrServiceItemsNeeded)); }
        }

        private bool isVisibleRepairOrServiceItemsNeededMsgErr;
        /// <summary>
        /// Property for isVisibleRepairOrServiceItemsNeededMsgErr
        /// </summary>
        public bool IsVisibleRepairOrServiceItemsNeededMsgErr
        {
            get { return isVisibleRepairOrServiceItemsNeededMsgErr; }
            set
            {
                isVisibleRepairOrServiceItemsNeededMsgErr = value;
                OnPropertyChanged(nameof(IsVisibleRepairOrServiceItemsNeededMsgErr));
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
                OnPropertyChanged(nameof(Email));
                ValidateEmail();
            }
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

        private bool isVisibleEmailError;
        /// <summary>
        /// Property for isVisibleEmailError
        /// </summary>
        public bool IsVisibleEmailError
        {
            get { return isVisibleEmailError; }
            set { isVisibleEmailError = value; OnPropertyChanged(nameof(IsVisibleEmailError)); }
        }

        private string mobileNumber;
        /// <summary>
        /// Property for mobileNumber
        /// </summary>
        public string MobileNumber
        {
            get { return mobileNumber; }
            set
            {
                mobileNumber = value; OnPropertyChanged(nameof(MobileNumber));
                ValidateMobileNumber();
            }
        }

        private string mobileNumberforErrorMessage;
        /// <summary>
        /// Property for mobileNumberforErrorMessage
        /// </summary>
        public string MobileNumberforErrorMessage
        {
            get { return mobileNumberforErrorMessage; }
            set { mobileNumberforErrorMessage = value; OnPropertyChanged(nameof(MobileNumberforErrorMessage)); }
        }

        private bool isMobileNumberforError;
        /// <summary>
        /// Property for IsMobileNumberforError
        /// </summary>
        public bool IsMobileNumberforError
        {
            get { return isMobileNumberforError; }
            set { isMobileNumberforError = value; OnPropertyChanged(nameof(IsMobileNumberforError)); }
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
                OnPropertyChanged(nameof(ZipCode));
                ValidateZipCode();
            }
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

        private string zipCodeErrorMessage;
        /// <summary>
        /// Property for zip code Error message
        /// </summary>
        public string ZipCodeErrorMessage
        {
            get { return zipCodeErrorMessage; }
            set { zipCodeErrorMessage = value; OnPropertyChanged(nameof(ZipCodeErrorMessage)); }
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

        private ServiceData selectedServiceType;
        /// <summary>
        /// property for show selectedServiceType
        /// </summary>
        public ServiceData SelectedServiceType
        {
            get { return selectedServiceType; }
            set
            {
                selectedServiceType = value; OnPropertyChanged(nameof(SelectedServiceType));
                if (SelectedServiceType != null)
                {
                    SelectServiceErrorMessage = string.Empty;
                    IsVisibleSelectServiceError = false;
                    if (selectedServiceType.ServiceName == "Auto Repair")
                    {
                        IsVinStackVisible = true;
                        IsVisibleVINError = true;
                        IsVisibleMilesKMOrEngineyHours = true;                       
                    }
                }
            }
        }

        private bool isVinStackVisible;
        /// <summary>
        /// property for show isVinStackVisible
        /// </summary>
        public bool IsVinStackVisible
        {
            get { return isVinStackVisible; }
            set { isVinStackVisible = value; OnPropertyChanged(nameof(IsVinStackVisible)); }
        }

        public string Test
        {
            get { return test; }
            set { test = value; OnPropertyChanged(nameof(Test)); }
        }
        private ObservableCollection<ServiceData> serviceTypeList;
        /// <summary>
        /// property for show serviceTypeList
        /// </summary>
        public ObservableCollection<ServiceData> ServiceTypeList
        {
            get { return serviceTypeList; }
            set { serviceTypeList = value; OnPropertyChanged(nameof(ServiceTypeList)); }
        }


        private string price;
        /// <summary>
        /// Property for Price
        /// </summary>
        public string Price
        {
            get { return price; }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {

                        if (value.ToString().Replace(".", string.Empty).Length > 8)
                        {
                            IsVisiblePriceError = true;
                            PriceError = "Please enter valid price in each field. Maximum limit is 8 degit in each price field.";
                        }
                        else
                        {
                            if (decimal.TryParse(value, out _))
                            {
                                string[] val = value.Split('.');
                                if (val.Count() > 1)
                                {
                                    if (!string.IsNullOrEmpty(val[1]))
                                    {
                                        if (val[1].Length <= 2)
                                        {
                                            price = Math.Round(Convert.ToDecimal(value), 2).ToString();
                                        }

                                    }
                                    else
                                    {
                                        price = value;
                                        IsVisiblePriceError = false;
                                        PriceError = string.Empty;
                                    }
                                }
                                else
                                {
                                    price = value;
                                    IsVisiblePriceError = false;
                                    PriceError = string.Empty;
                                }
                            }
                            else
                            {
                                IsVisiblePriceError = true;
                                PriceError = "Please enter valid price.";
                            }

                        }
                    }
                    else
                    {
                        price = value;
                        IsVisiblePriceError = false;
                        PriceError = string.Empty;
                    }
                }
                catch (Exception ex)
                {

                }
                
                OnPropertyChanged(nameof(Price));
            }
        }

        private string priceError;
        /// <summary>
        /// Property for Price error
        /// </summary>
        public string PriceError
        {
            get { return priceError; }
            set { priceError = value; OnPropertyChanged(nameof(PriceError)); }
        }
        private bool isVisiblePriceError;
        /// <summary>
        /// Property for is visible Price error
        /// </summary>
        public bool IsVisiblePriceError
        {
            get { return isVisiblePriceError; }
            set { isVisiblePriceError = value; OnPropertyChanged(nameof(IsVisiblePriceError)); }
        }

        private string vIN;
        /// <summary>
        /// Property for VIN
        /// </summary>
        public string VIN
        {
            get { return vIN; }
            set
            {
                vIN = value;
                OnPropertyChanged(nameof(VIN));
                ValidateVIN();
            }
        }


        private string vINError;
        /// <summary>
        /// Property for VINError
        /// </summary>
        public string VINError
        {
            get { return vINError; }
            set { vINError = value; OnPropertyChanged(nameof(VINError)); }
        }

        private bool isVisibleVINError;
        /// <summary>
        /// Property for IsVisibleVINError
        /// </summary>
        public bool IsVisibleVINError
        {
            get { return isVisibleVINError; }
            set
            {
                isVisibleVINError = value;
                OnPropertyChanged(nameof(IsVisibleVINError));
            }
        }

        private string milesKMOrEngineyHours;
        /// <summary>
        /// Property for MilesKMOrEngineyHours
        /// </summary>
        public string MilesKMOrEngineyHours
        {
            get { return milesKMOrEngineyHours; }
            set
            {
                milesKMOrEngineyHours = value;
                OnPropertyChanged(nameof(MilesKMOrEngineyHours));
                ValidateMilesKMOrEngineyHours();
            }
        }


        private string milesKMOrEngineyHoursError;
        /// <summary>
        /// Property for MilesKMOrEngineyHoursError
        /// </summary>
        public string MilesKMOrEngineyHoursError
        {
            get { return milesKMOrEngineyHoursError; }
            set { milesKMOrEngineyHoursError = value; OnPropertyChanged(nameof(MilesKMOrEngineyHoursError)); }
        }


        private bool isVisibleMilesKMOrEngineyHours;
        /// <summary>
        /// Property for IsVisibleMilesKMOrEngineyHours
        /// </summary>
        public bool IsVisibleMilesKMOrEngineyHours
        {
            get { return isVisibleMilesKMOrEngineyHours; }
            set
            {
                isVisibleMilesKMOrEngineyHours = value;
                OnPropertyChanged(nameof(IsVisibleMilesKMOrEngineyHours));
            }
        }


        #endregion

        #region Command

        private Command submitCommand;
        /// <summary>
        /// This command use for process submit Command 
        /// </summary>
        public Command SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new Command(async () =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                IsLoader = true;

                                SubmitQuotation();

                                IsLoader = false;
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
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
                return submitCommand;
            }
        }
        public Command HireNegotiatorCommand
        {

            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        var current = Connectivity.NetworkAccess;
                        if (current == Xamarin.Essentials.NetworkAccess.Internet)
                        {
                                IsLoader = true;
                            CommonResponse commonResponse = new CommonResponse();

                            var hireNegotiatorRequestModel = new HireNegotiatorRequestModel();
                            hireNegotiatorRequestModel.quotationId = QBidHelper.QuotationId.ToString();
                            hireNegotiatorRequestModel.facilityId = QBidHelper.FacilityId;
                            commonResponse = await apiServices.HierNegotiatiorDetails(hireNegotiatorRequestModel);

                            if (commonResponse != null)
                            {
                                if (commonResponse.code == (int)HttpStatusCode.OK)
                                {
                                    IsLoader = false;
                                    Task.Delay(2000);
                                    await App.Current.MainPage.DisplayAlert(string.Empty, commonResponse.message, ResourceValues.OkButtontext);
                                   
                                    App.Current.MainPage = new NavigationPage(new QBidViewDetail());
                                    UtilHelper.Attachment = null;
                                }
                                else
                                {
                                    var err = string.Empty;
                                    if (Convert.ToString(commonResponse.message).ToLower() == "failed")
                                    {
                                        err = Convert.ToString(commonResponse.error);
                                    }
                                    DependencyService.Get<IToastMessage>().LongAlert(commonResponse.message.ToString() + "\n" + err);
                                }
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                            });
                        }


                        //App.Current.MainPage = new NavigationPage(new QBidViewDetail());
                        //UtilHelper.Attachment = null;
                        
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
        }

        public Command OkCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                        App.Current.MainPage = new NavigationPage(new DashboardView());
                        UtilHelper.Attachment = null;
                    }
                    catch (Exception ex)
                    {
                        LogManager.TraceErrorLog(ex);
                    }
                });
            }
        }

        #endregion

        #region Method
        /// <summary>
        /// This method is used to validate the SaleRepresentativeName.
        /// </summary>
        /// <returns></returns>
        public bool ValidatePrice()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(Price))
            {
                IsVisiblePriceError = true;
                PriceError = "Please enter price.";
                flag = true;
            }
            else
            {
                IsVisiblePriceError = false;
                PriceError = string.Empty;
            }
            return flag;
        }


        /// <summary>
        /// This method is used to validate the SaleRepresentativeName.
        /// </summary>
        /// <returns></returns>
        public bool ValidateSaleRepresentativeName()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(SaleRepresentativeName))
            {
                IsVisibleSaleRepresentavtiveMsgErr = true;
                ErrorTextRepresentativeName = "Please enter your advisor name.";
                flag = true;
            }
            else
            {
                IsVisibleSaleRepresentavtiveMsgErr = false;
                ErrorTextRepresentativeName = string.Empty;
            }
            return flag;
        }
        public string MobileNumberic { get; set; }
        /// <summary>
        /// This method is used to validate the MobileNumber.
        /// </summary>
        /// <returns></returns>
        public bool ValidateMobileNumber()
        {
            bool flag = false;
            string mobileNumber = string.Empty;
            if (!string.IsNullOrEmpty(MobileNumber))
            {
                var mobileNumber1 = MobileNumber.Replace(" ", String.Empty);
                var mobileNumber2 = mobileNumber1.Replace("(", String.Empty);
                var mobileNumber3 = mobileNumber2.Replace(")", String.Empty);
                mobileNumber = mobileNumber3.Replace("-", String.Empty);
                MobileNumberic = mobileNumber;
            }
            if (!string.IsNullOrEmpty(mobileNumber) && mobileNumber.Length == 10)
            {
                IsMobileNumberforError = false;
            }
            else if (!string.IsNullOrEmpty(mobileNumber) && mobileNumber.Length != 10)
            {
                IsMobileNumberforError = true;
                MobileNumberforErrorMessage = "Please enter valid company/vender mobile number.";
                flag = true;
            }
            else if (string.IsNullOrEmpty(mobileNumber))
            {
                IsMobileNumberforError = true;
                MobileNumberforErrorMessage = "Please enter company/vender mobile number.";
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// This method is used to validate the email id.
        /// </summary>
        /// <returns></returns>        
        public bool ValidateEmail()
        {
            bool flag = false;
            if (!QBidHelper.IsEmpty(Email))
            {
                if (QBidHelper.IsValidEmail(Email))
                {
                    IsVisibleEmailError = false;
                    EmailErrorMessage = string.Empty;
                    flag = false;
                }
                else
                {
                    IsVisibleEmailError = true;
                    EmailErrorMessage = "Please enter a valid company/vendor email.";
                    flag = true;
                }
            }
            else
            {
                IsVisibleEmailError = true;
                EmailErrorMessage = "Please enter company/vendor email";
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// This method is used to ValidateEnterCompanyVendorName.
        /// </summary>
        /// <returns></returns>
        public bool ValidateEnterCompanyVendorName()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(EnterCompanyVendorName))
            {
                IsVisibleEnterCompanyVendorNameMsgErr = true;
                ErrorTextEnterCompanyVendorName = "Please enter facility name.";
                flag = true;
            }
            else
            {
                IsVisibleEnterCompanyVendorNameMsgErr = false;
            }
            return flag;
        }

        /// <summary>
        /// This method is used to ValidateEnterWorkRepairOrder.
        /// </summary>
        /// <returns></returns>
        public bool ValidateEnterWorkRepairOrder()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(WorkRepairOrder))
            {
                IsVisibleWorkRepairOrderMsgErr = true;
                ErrorTextWorkRepairOrder = "Please enter Work/Repair Order.";
                flag = true;
            }
            else
            {
                IsVisibleWorkRepairOrderMsgErr = false;
            }
            return flag;
        }

        /// <summary>
        /// This method is used to ValidateEnterRepairOrServiceItemsNeeded.
        /// </summary>
        /// <returns></returns>
        public bool ValidateEnterRepairOrServiceItemsNeeded()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(RepairOrServiceItemsNeeded))
            {
                IsVisibleRepairOrServiceItemsNeededMsgErr = true;
                ErrorTextRepairOrServiceItemsNeeded = "Please enter service item.";
                flag = true;
            }
            else
            {
                IsVisibleRepairOrServiceItemsNeededMsgErr = false;
            }


            return flag;
        }

        /// <summary>
        /// This method is used to ValidateZipCode.
        /// </summary>
        /// <returns></returns>
        public bool ValidateZipCode()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(ZipCode))
            {
                IsZipCodeError = true;
                ZipCodeErrorMessage = ResourceValues.ZipCodeErrorMessage;
                flag = true;
            }
            else
            {
                IsZipCodeError = false;
            }
            return flag;
        }

        /// <summary>
        /// This method is used to ValidateVendorStreetAddress.
        /// </summary>
        /// <returns></returns>
        public bool ValidateVendorStreetAddress()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(VendorStreetAddress))
            {
                IsVisibleVendorStreetAddressMsgErr = true;
                ErrorTextVendorStreetAddress = "Please enter your street address.";
                flag = true;
            }
            else
            {
                IsVisibleVendorStreetAddressMsgErr = false;
            }
            return flag;
        }

        /// <summary>
        /// This method is used to ValidateCity.
        /// </summary>
        /// <returns></returns>
        public bool ValidateCity()
        {
            bool flag = false;
            if (QBidHelper.IsEmpty(City))
            {
                IsVisibleCityMsgErr = true;
                ErrorTextCity = "Please enter city name.";
                flag = true;
            }
            else
            {
                IsVisibleCityMsgErr = false;
            }
            return flag;
        }
        /// <summary>
        /// This method is used to ValidateState.
        /// </summary>
        /// <returns></returns>
        public bool ValidateState()
        {
            bool flag = false;
            if (StateValue == null)
            {
                StateNameErrorMessage = "Please select state.";
                IsVisibleStateNameError = true;
                flag = true;

            }
            else
            {
                IsVisibleStateNameError = false;
            }
            return flag;

        }
        /// <summary>
        /// This method is used to ValidateService.
        /// </summary>
        /// <returns></returns>
        public bool ValidateService()
        {
            bool flag = false;
            if (SelectedServiceType == null)
            {
                SelectServiceErrorMessage = "Please select service.";
                IsVisibleSelectServiceError = true;
                flag = true;

            }
            else
            {
                IsVisibleSelectServiceError = false;
            }
            return flag;

        }

        /// <summary>
        /// This method is used to ValidateCity.
        /// </summary>
        /// <returns></returns>
        public bool ValidateVIN()
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(VIN) && VIN.Length == 17)
            {
                IsVisibleVINError = false;
            }
            else if (!string.IsNullOrEmpty(VIN) && VIN.Length != 17)
            {
                IsVisibleVINError = true;
                VINError = "Please enter Valid VIN, must will be 17 character.";
                flag = true;
            }
            //else if (string.IsNullOrEmpty(VIN))
            //{
            //    IsVisibleVINError = true;
            //    VINError = "Please enter Valid VIN, must will be 17 character.";
            //    flag = true;
            //}
            return flag;
        }

        private bool submitPopUpVisible;
        public bool SubmitPopUpVisible
        {
            get { return submitPopUpVisible; }
            set { submitPopUpVisible = value; OnPropertyChanged(nameof(SubmitPopUpVisible)); }
        }


        /// <summary>
        /// This method is used to ValidateMilesKMOrEngineyHours.
        /// </summary>
        /// <returns></returns>
        public bool ValidateMilesKMOrEngineyHours()
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(MilesKMOrEngineyHours) && MilesKMOrEngineyHours.Length == 7)
            {
                IsVisibleMilesKMOrEngineyHours = false;
            }
            else if (!string.IsNullOrEmpty(MilesKMOrEngineyHours) && MilesKMOrEngineyHours.Length != 7)
            {
                IsVisibleMilesKMOrEngineyHours = true;
                MilesKMOrEngineyHoursError = "Must be mileage 7 digit in numeric.";
                flag = true;
            }
            //else if (string.IsNullOrEmpty(MilesKMOrEngineyHours))
            //{
            //    IsVisibleMilesKMOrEngineyHours = true;
            //    MilesKMOrEngineyHoursError = "Must be mileage 7 digit in numeric.";
            //    flag = true;
            //}
            return flag;
        }

        /// <summary>
        /// Method for check null value for validation
        /// </summary>
        /// <returns></returns>
        public bool IsValidQuotationForm()
        {
            bool flag = true;
            try
            {
                if (ValidateSaleRepresentativeName())
                    flag = false;
                if (ValidateEmail())
                    flag = false;
                if (ValidateMobileNumber())
                    flag = false;
                if (ValidateEnterCompanyVendorName())
                    flag = false;
                if (ValidateEnterWorkRepairOrder())
                    flag = false;
                if (ValidateEnterRepairOrServiceItemsNeeded())
                    flag = false;
                if (ValidateZipCode())
                    flag = false;
                if (ValidateService())
                    flag = false;
                if (ValidatePrice())
                    flag = false;
                if (selectedServiceType != null)
                {
                    if (SelectedServiceType.ServiceName == "Auto Repair")
                    {

                        if (ValidateVIN())
                        {  
                            IsVinStackVisible = true;
                            flag = false;
                        }
                        else
                        {
                            IsVinStackVisible = true;
                            flag = true;
                        }

                        if (ValidateMilesKMOrEngineyHours())
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        IsVinStackVisible = false;
                    }
                }


            }
            catch (Exception ex)
            {
            }
            return flag;
        }


        /// <summary>
        /// Method for Get Service Type
        /// </summary>
        /// <returns></returns>
        public async Task GetServiceType()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    using (APIService aPIService = new APIService())
                    {

                        var responce = await aPIService.SelectService().ConfigureAwait(true);
                        if (responce.Data != null && responce.Data.Count > 0)
                        {
                            var y = new ObservableCollection<ServiceData>(responce.Data.OrderBy(x => x.Id));
                            ServiceTypeList = y;
                        }
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
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

                        var responce = await aPIService.GetStateAPI();
                        if (responce.data != null && responce.data.Count > 0)
                        {
                            var y = new ObservableCollection<State>(responce.data.OrderBy(x => x.code));
                            ListOfState = y;
                        }
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
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

        public LData lData = new LData();
        private async Task SubmitQuotation()
        {
            try
            {
                apiServices = new APIService();
                lData = new LData();
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    if (IsValidQuotationForm())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoader = true;
                        });
                        var quotationFormRequestModel = new QuotationFormRequestModel();
                        MultipartFormDataContent dataContent = new MultipartFormDataContent();
                        int counter = 0;
                        foreach (var image in UtilHelper.Attachment)
                        {
                            dataContent.Add(new ByteArrayContent(new StreamContent(image.File).ReadAsByteArrayAsync().Result), nameof(quotationFormRequestModel.attachment) + "[" + counter + "]", image.FileName);

                            counter++;
                        }
                        
                        dataContent.Add(new StringContent(SaleRepresentativeName), nameof(quotationFormRequestModel.representativeName));
                        dataContent.Add(new StringContent(MobileNumberic), nameof(quotationFormRequestModel.contact));
                        dataContent.Add(new StringContent(Email), nameof(quotationFormRequestModel.email));
                        dataContent.Add(new StringContent(EnterCompanyVendorName), nameof(quotationFormRequestModel.vendorName));
                        dataContent.Add(new StringContent(ZipCode), nameof(quotationFormRequestModel.zipCode));
                        dataContent.Add(new StringContent(WorkRepairOrder), nameof(quotationFormRequestModel.workOrder));
                        if (!string.IsNullOrEmpty(MilesKMOrEngineyHours))
                        {
                            dataContent.Add(new StringContent(MilesKMOrEngineyHours), nameof(quotationFormRequestModel.mileage));
                        }
                        if (!string.IsNullOrEmpty(VIN))
                        {
                            dataContent.Add(new StringContent(VIN), nameof(quotationFormRequestModel.VIN));
                        }

                        if (!string.IsNullOrEmpty(VendorStreetAddress))
                        {
                            dataContent.Add(new StringContent(VendorStreetAddress), nameof(quotationFormRequestModel.streetAddress));
                        }
                        if (!string.IsNullOrEmpty(City))
                        {
                            dataContent.Add(new StringContent(City), nameof(quotationFormRequestModel.city));
                        }
                        if (StateValue != null)
                        {
                            dataContent.Add(new StringContent(StateValue.code), nameof(quotationFormRequestModel.state));
                        }


                        
                        dataContent.Add(new StringContent(SelectedServiceType.Id.ToString()), nameof(quotationFormRequestModel.serviceType));
                        dataContent.Add(new StringContent(RepairOrServiceItemsNeeded), nameof(quotationFormRequestModel.repairItemName));
                        dataContent.Add(new StringContent(price), nameof(quotationFormRequestModel.price));

                        //quotationFormRequestModel.OEM = OEM;

                        var commonResponse = await apiServices.SubmitQuotation(dataContent);
                        if (commonResponse != null)
                        {
                            if (commonResponse.code == 200)
                            {
                                 QBidHelper.QuotationId = commonResponse.data.quptationId;
                                 QBidHelper.FacilityId = commonResponse.data.facilityId;
                                IsLoader = false;
                                SubmitPopUpVisible = true;
                             
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(commonResponse.message) + "\n" + commonResponse.error);
                                });
                                SubmitPopUpVisible = false;
                                UtilHelper.Attachment = null;
                            }
                        }

                        IsLoader = false;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
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
