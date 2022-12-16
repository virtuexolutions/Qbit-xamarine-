using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models.APIRequest;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class ReSendQbidMailViewModel : BindableObject
    {
        #region Constructor
      
        #endregion
        #region Properties

        private string emailAddress;
        /// <summary>
        ///  Property for User emailAddress
        /// </summary>

        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                emailAddress = value; OnPropertyChanged(nameof(EmailAddress));
                ValidateEmail();
            }
        }

        private string emailAddressError;
        /// <summary>
        ///  Property for User emailAddressError
        /// </summary>

        public string EmailAddressError
        {
            get { return emailAddressError; }
            set { emailAddressError = value; OnPropertyChanged(nameof(EmailAddressError)); }
        }


        private bool isVisibleEmailAddress;
        /// <summary>
        ///  Property for User isVisibleEmailAddress 
        /// </summary>

        public bool IsVisibleEmailAddress
        {
            get { return isVisibleEmailAddress; }
            set { isVisibleEmailAddress = value; OnPropertyChanged(nameof(IsVisibleEmailAddress)); }
        }

        private bool isLoader;
        /// <summary>
        /// Property for IsLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set
            {
                isLoader = value; OnPropertyChanged(nameof(IsLoader));
            }
        }
        #endregion

   

        #region Commands
        private Command CmdReSendQuatation;
        /// <summary>
        /// This command use for process CommandOnAddNewQuatation
        /// </summary>
        public Command CommandOnReSendQuatation
        {
            get
            {
                if (CmdReSendQuatation == null)
                {
                    CmdReSendQuatation = new Command(async () =>
                    {
                        try
                        {
                            if (IsValid())
                            {
                                var current = Connectivity.NetworkAccess;
                                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                                {
                                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(ResendPopUp, ResourceValues.YesButtontext, ResourceValues.NoButtontext, ResourceValues.ResendQbidMessage, true)).ConfigureAwait(true);

                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread( () =>
                                    {
                                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                                    });
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return CmdReSendQuatation;
            }
        }

        /// <summary>
        /// command for 
        /// </summary>
        private Command commandOnFacility;
        /// <summary>
        /// property use for commandOnFacility
        /// </summary>
        public Command CommandOnFacility
        {
            get
            {
                if (commandOnFacility == null)
                {
                    commandOnFacility = new Command(() =>
                    {
                        try
                        {

                            App.Current.MainPage.DisplayAlert(ResourceValues.OkButtontext, "Enter you message", ResourceValues.CancelButtontext);

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnFacility;
            }
        }
        #endregion


        #region Methods
        /// <summary>
        /// popup for resend Quotation to vendor
        /// </summary>
        public  void ResendPopUp()
        {
            var value = QBidHelper.AccceptQuotationStatus;
            switch (value)
            {
                case true:
                    ReSendQuatation().ConfigureAwait(true);
                    break;
                case false:
                    break;
            }
        }

        /// <summary>
        /// popup for resend Quotation confirm message
        /// </summary>
        public async void ResendConfirmPopUp()
        {
            var value = true;
            switch (value)
            {
                case true:
                    EmailAddress = string.Empty;
                    IsVisibleEmailAddress = false;
                    EmailAddressError = string.Empty;
                    var memberCardAdded = Preferences.Get(ConstantValues.IsMemberCardAddedPref, 0);
                    if (memberCardAdded == 0)
                    {
                        await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.AlertAddCreditCardMessage, QBidResource.ResourceValues.OkButtontext);
                    }
                    App.Current.MainPage.Navigation.PushAsync(new DashboardView());
                    break;
                case false:
                    break;
            }
        }

        APIService apiServices = null;
        /// <summary>
        /// This method is used for send New Quatation.
        /// </summary>
        private async Task ReSendQuatation()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    IsLoader = true;
                    apiServices = new APIService();
                    var reSendQuatationRequestModel = new ReSendQuatationRequestModel();
                    reSendQuatationRequestModel.oldEmail = QutationListViewModel.FacilityMail;
                    reSendQuatationRequestModel.newEmail = EmailAddress;
                    reSendQuatationRequestModel.quotationId = QutationListViewModel.QuotationId;
                    reSendQuatationRequestModel.facilityId = QutationListViewModel.FacilityId;

                    var sendNewQuatationResponse = await apiServices.ReSendNewQuatation(reSendQuatationRequestModel).ConfigureAwait(false);
                    if(sendNewQuatationResponse !=null)
                    {
                        if (sendNewQuatationResponse.code == (int)HttpStatusCode.OK)
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(ResendConfirmPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.ResendQuotationConfirmMessagw + ConstantValues.OneSpace + EmailAddress, false)).ConfigureAwait(true);

                            });

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(sendNewQuatationResponse.message));
                            });
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
                IsVisibleEmailAddress = false;
                EmailAddressError = string.Empty;
                IsLoader = false;
            }
        }

        /// <summary>
        /// This method is used to validate the EmailAddress
        /// </summary>
        /// <returns></returns>        
        public bool ValidateEmail()
        {
            bool flag = false;
            if (!QBidHelper.IsEmpty(EmailAddress))
            {
                if (QBidHelper.IsValidEmail(EmailAddress))
                {
                    IsVisibleEmailAddress = false;
                    EmailAddressError = string.Empty;
                    flag = false;
                }
                else
                {
                    IsVisibleEmailAddress = true;
                    EmailAddressError = ResourceValues.ResendValidateErrorMessage;
                    flag = true;
                }
            }
            else
            {
                IsVisibleEmailAddress = false;
                EmailAddressError = "Please enter vendor/ representative email address.";
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Method for check null value
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool flag = true;
            try
            {
                if (ValidateEmail())
                    flag = false;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return flag;
        }

        #endregion
    }
}
