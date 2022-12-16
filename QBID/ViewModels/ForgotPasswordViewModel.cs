using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models.APIRequest;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class ForgotPasswordViewModel : BindableObject
    {

        #region Constructor
        public ForgotPasswordViewModel()
        {
            try
            {
                apiServices = new APIService();
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        #endregion

        #region property
        APIService apiServices = null;

        private bool isLoader;
        /// <summary>
        /// Property for Loader visible
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
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
        /// Property for email Error visible
        /// </summary>
        public bool IsVisibleEmailError
        {
            get { return isVisibleEmailError; }
            set { isVisibleEmailError = value; OnPropertyChanged(nameof(IsVisibleEmailError)); }
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

        private string forgotEmailErrorMsg;

        public string ForgotEmailErrorMsg
        {
            get { return forgotEmailErrorMsg; }
            set { forgotEmailErrorMsg = value; OnPropertyChanged(nameof(ForgotEmailErrorMsg)); }
        }

        private bool isVisibleForgotEmailError;

        public bool IsVisibleForgotEmailError
        {
            get { return isVisibleForgotEmailError; }
            set { isVisibleForgotEmailError = value; OnPropertyChanged(nameof(IsVisibleForgotEmailError)); }
        }

        #endregion


        #region Commands
        private Command commandOnBackForService;
        /// <summary>
        /// This command use for BackForService 
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

        private Command commandOnForgotSubmit;
        /// <summary>
        /// This command use for commandOnForgotSubmit 
        /// </summary>
        public Command CommandOnForgotSubmit
        {
            get
            {
                if (commandOnForgotSubmit == null)
                {
                    commandOnForgotSubmit = new Command(async () =>
                    {
                        try
                        {
                            if (IsValid())
                            {
                                await ForgotPassword();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnForgotSubmit;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        ///  This method is used to ForgotPassword
        /// </summary>
        /// <returns></returns>
        private async Task ForgotPassword()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    var forgotPasswordRequestModel = new ForgotPasswordRequestModel();
                    forgotPasswordRequestModel.email = Email;
                    var forgotPasswordModel = await apiServices.ForgotPassword(forgotPasswordRequestModel);
                    if (forgotPasswordModel != null)
                    {
                        if (forgotPasswordModel.code == 200)
                        {
                            Email = string.Empty;
                            await App.Current.MainPage.DisplayAlert(String.Empty, forgotPasswordModel.message, ResourceValues.OkButtontext);
                            await App.Current.MainPage.Navigation.PushAsync(new UserLoginView());

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, forgotPasswordModel.message, ResourceValues.OkButtontext);
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
                IsLoader = false;
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
                IsLoader = false;
            }
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
                    IsVisibleForgotEmailError = false;
                    ForgotEmailErrorMsg = string.Empty;
                    flag = false;
                }
                else
                {
                    IsVisibleForgotEmailError = true;
                    ForgotEmailErrorMsg = ResourceValues.CheckEmailErrorMessage;
                    flag = true;
                }
            }
            else
            {
                IsVisibleForgotEmailError = true;
                ForgotEmailErrorMsg = ResourceValues.EmailErrorMessage;
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

