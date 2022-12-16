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
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace QBid.ViewModels
{
    public class UserLoginViewModel : BindableObject
    {
     
        #region Constructor
        /// <summary>
        /// This Constructor use for process UserType
        /// </summary>
        public UserLoginViewModel()
        {
            try
            {
                service = new APIService();
                ListOfUserType = new ObservableCollection<UserTypeModel>();


            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        #endregion

        #region Properties
        APIService service;
        public ObservableCollection<UserTypeModel> listOfUserType;
        /// <summary>
        /// Propert for ListOfUserType
        /// </summary>
        public ObservableCollection<UserTypeModel> ListOfUserType
        {
            get { return listOfUserType; }
            set { listOfUserType = value; OnPropertyChanged(nameof(ListOfUserType)); }
        }

        private string appVersion;
        /// <summary>
        /// Property for AppVersion
        /// </summary>
        public string AppVersion
        {
            get { return appVersion; }
            set { appVersion = value; OnPropertyChanged(nameof(AppVersion)); }
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
                try
                {
                    if (UserTypeValue.UserTypeId > 0)
                    {
                        UserTypeErrorMessage = string.Empty;
                        IsVisibleuserTypeErrorMessage = false;
                    }
                }
                catch
                {

                }
                OnPropertyChanged(nameof(UserTypeValue));

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
        /// Property for user Type Error message
        /// </summary>
        public bool IsVisibleuserTypeErrorMessage
        {
            get { return isVisibleuserTypeErrorMessage; }
            set { isVisibleuserTypeErrorMessage = value; OnPropertyChanged(nameof(IsVisibleuserTypeErrorMessage)); }
        }


        private string username;
        /// <summary>
        /// Property for User Username
        /// </summary>
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));

                if (string.IsNullOrEmpty(username))
                {
                    UsernameErrorMsg = ResourceValues.EmailErrorMessage;
                    IsVisibleUsernameError = true;
                }
                else
                {
                    bool emailCheck = QBidHelper.IsValidEmail(username);
                    if (!emailCheck)
                    {
                        UsernameErrorMsg = ResourceValues.CheckEmailErrorMessage;
                        IsVisibleUsernameError = true;
                    }
                    else
                    {
                        UsernameErrorMsg = string.Empty;
                        IsVisibleUsernameError = false;
                    }
                }

            }
        }
        private string usernameErrorMsg;
        /// <summary>
        /// Property for User name Error Msg
        /// </summary>
        public string UsernameErrorMsg
        {
            get { return usernameErrorMsg; }
            set { usernameErrorMsg = value; OnPropertyChanged(nameof(UsernameErrorMsg)); }
        }
        private bool isVisibleUsernameError;
        /// <summary>
        /// Property for IsVisible User name Error
        /// </summary>
        public bool IsVisibleUsernameError
        {
            get { return isVisibleUsernameError; }
            set { isVisibleUsernameError = value; OnPropertyChanged(nameof(IsVisibleUsernameError)); }
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
                OnPropertyChanged(nameof(Password));
                if (!string.IsNullOrEmpty(password))
                {
                    passwordErrorMsg = string.Empty;
                    IsVisiblePasswordError = false;
                }
            }
        }
        private string passwordErrorMsg;
        /// <summary>
        /// Property for Password Error Msg
        /// </summary>
        public string PasswordErrorMsg
        {
            get { return passwordErrorMsg; }
            set { passwordErrorMsg = value; OnPropertyChanged(nameof(PasswordErrorMsg)); }
        }
        private bool isVisiblePasswordError;
        /// <summary>
        /// Property for IsVisible Password Error
        /// </summary>
        public bool IsVisiblePasswordError
        {
            get { return isVisiblePasswordError; }
            set { isVisiblePasswordError = value; OnPropertyChanged(nameof(IsVisiblePasswordError)); }
        }

        private bool isLoader;
        /// <summary>
        /// Property for IsLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private bool isRememberPassword;
        /// <summary>
        /// property for isRememberPassword
        /// </summary>

        public bool IsRememberPassword
        {
            get { return isRememberPassword; }
            set { isRememberPassword = value; OnPropertyChanged(nameof(IsRememberPassword)); }
        }

        private bool isVisibleRightsAndProvisionAccepted;
        /// <summary>
        /// property for isVisibleRightsAndProvisionAccepted
        /// </summary>
        public bool IsVisibleRightsAndProvisionAccepted
        {
            get { return isVisibleRightsAndProvisionAccepted; }
            set { isVisibleRightsAndProvisionAccepted = value; OnPropertyChanged(nameof(IsVisibleRightsAndProvisionAccepted)); }
        }

        #endregion


        #region Commands

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
                                UserTypeData();
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

        private Command commandOnSignUP, cmdForgotPassword;
        /// <summary>
        /// This command use for process commandForBack details
        /// </summary>
        public Command CommandOnSignUP
        {
            get
            {
                if (commandOnSignUP == null)
                {
                    commandOnSignUP = new Command( () =>
                    {
                        try
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new RegistrationView());
                            });

                        }
                        catch (Exception ex)
                        {

                            LogManager.TraceErrorLog(ex);

                        }
                    });
                }
                return commandOnSignUP;
            }
        }

        private Command commandOnQuickAppTour;
        /// <summary>
        /// COmmand for show On bording page
        /// </summary>
        public Command CommandOnQuickAppTour
        {
            get
            {
                if (commandOnQuickAppTour == null)
                {
                    commandOnQuickAppTour = new Command(() =>
                    {
                        try
                        {
                           
                            Device.BeginInvokeOnMainThread(() => 
                            {
                                 App.Current.MainPage.Navigation.PushAsync(new OnboardingPage());
                            });
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });

                }
                return commandOnQuickAppTour;
            }
        }
        private Command loginOnCommand;
        /// <summary>
        /// This command use for process LoginOnCommand details
        /// </summary>
        public Command LoginOnCommand
        {
            get
            {
                if (loginOnCommand == null)
                {
                    loginOnCommand = new Command(async () =>
                    {
                        try
                        {
                            if (ValidateLoginControls())
                            {

                                IsLoader = true;
                                await UserLogin().ConfigureAwait(false);
                                IsLoader = false;
                            }
                            else
                            {
                                await App.Current.MainPage.Navigation.PopAsync();
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
                return loginOnCommand;
            }
        }

        private Command cmdOnAcceptRightsAndProvision;
        /// <summary>
        /// This command use for process Accept Rights AndProvision
        /// </summary>
        public Command CommandOnAcceptRightsAndProvision
        {
            get
            {
                if (cmdOnAcceptRightsAndProvision == null)
                {
                    cmdOnAcceptRightsAndProvision = new Command( () =>
                    {
                        try
                        {
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                IsVisibleRightsAndProvisionAccepted = false;
                                Preferences.Set(ConstantValues.IsRightsAndProvisionAcceptedPref, true);
                                App.Current.MainPage = new DashboardView();
                            });
                        }
                        catch (Exception ex)
                        {

                            LogManager.TraceErrorLog(ex);

                        }
                    });
                }
                return cmdOnAcceptRightsAndProvision;
            }
        }
        /// <summary>
        /// This Command On Forgot Password
        /// </summary>
        public Command CommandOnForgotPassword
        {
            get
            {
                try
                {
                    if (cmdForgotPassword == null)
                        cmdForgotPassword = new Command(async () =>
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordView());
                        });
                }
                catch (Exception ex)
                {
                    LogManager.TraceErrorLog(ex);
                }
                return cmdForgotPassword;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// this method for user login
        /// </summary>
        /// <returns></returns>
        private async Task UserLogin()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    UserLoginResponse userLoginResponse;
                    IsLoader = true;
                    if (ValidateLoginControls())
                    {
                        var userLoginRequestModel = new UserLoginRequestModel();
                        userLoginRequestModel.Email = username;
                        userLoginRequestModel.Password = Password;
                        userLoginRequestModel.DeviceToken = Preferences.Get(ConstantValues.FcmTokenPreferenceText, string.Empty);
                      // userLoginRequestModel.DeviceToken = "dpEORWjsW0m3je14mXkCLJ:APA91bFwCIbScrJt65Zg9Ita8Xe2hfj2TksRXqYnLJy1WMv1_9GogUhTkttGSdekK2lUssabTZxO9Vd-bbuBZcS0SPg2ZVV1LJ6iY4AeeROzEvVSrEzlw4Fo97cByPmbHY-nkqci-d6o";

                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            userLoginRequestModel.DeviceType = Device.iOS.ToUpper();

                        }
                        else
                        {
                            userLoginRequestModel.DeviceType = Device.Android.ToLower();
                        }
                        if (UserTypeValue.UserTypeId == (int)UtilHelper.UserRoleType.Member)
                        {
                            userLoginResponse = await service.UserLogin(userLoginRequestModel).ConfigureAwait(true);

                        }
                        else
                        {
                            userLoginResponse = await service.NegoTiatorLoginDetails(userLoginRequestModel).ConfigureAwait(true);
                        }
                        if (userLoginResponse != null)
                        {
                            #region Handle Response
                            if (userLoginResponse.code == 200)
                            {

                                if (IsRememberPassword)
                                {

                                    Preferences.Set(ConstantValues.IsloginPref, true);
                                    Preferences.Set(ConstantValues.IsRememberPasswordPref, true);
                                    Preferences.Set(ConstantValues.UserTypePref, UserTypeValue.UserTypeId);
                                    //Preferences.Set(ConstantValues.UserTypePref, userLoginResponse.data.roleId);
                                    Preferences.Set(ConstantValues.EmailPref, userLoginRequestModel.Email);
                                    Preferences.Set(ConstantValues.PasswordPref, userLoginRequestModel.Password);
                                    Preferences.Set(ConstantValues.TokenKeyText, userLoginResponse.token);

                                }
                                else
                                {
                                    Preferences.Set(ConstantValues.IsRememberPasswordPref, false);
                                    Preferences.Set(ConstantValues.IsloginPref, true);
                                    Preferences.Set(ConstantValues.UserTypePref, UserTypeValue.UserTypeId);
                                    Preferences.Set(ConstantValues.TokenKeyText, userLoginResponse.token);
                                }

                                Preferences.Set(ConstantValues.IsRightsAndProvisionAcceptedPref, userLoginResponse.data.isRightsAndProvisionAccepted);
                                Preferences.Set(ConstantValues.CustomerStripeIdPref, userLoginResponse.data.customerStripeId);
                                Preferences.Set(ConstantValues.IsMemberCardAddedPref, userLoginResponse.data.cardAdded);
                                Preferences.Set(ConstantValues.IsNegotiatorAccountAddedPref, userLoginResponse.data.accountAdded);
                                Preferences.Set(ConstantValues.IsBankVerifiedPref, userLoginResponse.data.accountStatus);
                                Preferences.Set(ConstantValues.IsBankAccountStatusPref, userLoginResponse.data.bankTransefer);
                                // Preferences.Set(ConstantValues.RoleIdPref, userLoginResponse.data.roleId);  //RoleId 1=admin, 2=Member, 3=Negotiator

                                if (Preferences.Get(ConstantValues.IsRightsAndProvisionAcceptedPref, false))
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        DependencyService.Get<IToastMessage>().ShortAlert(Convert.ToString(userLoginResponse.message));
                                        await Task.Delay(3000);
                                        IsVisibleRightsAndProvisionAccepted = true;
                                        string QID = Preferences.Get(ConstantValues.QuotationIdPref, string.Empty);
                                        if (!string.IsNullOrEmpty(QID) && !string.IsNullOrWhiteSpace(QID))
                                        {
                                            Device.BeginInvokeOnMainThread(async () =>
                                            {
                                                await App.Current.MainPage.Navigation.PushAsync(new QBidViewDetail()).ConfigureAwait(true);
                                            });
                                        }
                                        else
                                        {
                                            await App.Current.MainPage.Navigation.PushAsync(new DashboardView());
                                        }
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        DependencyService.Get<IToastMessage>().ShortAlert(Convert.ToString(userLoginResponse.message));
                                        await Task.Delay(3000);
                                        IsVisibleRightsAndProvisionAccepted = true;
                                        App.Current.MainPage = new AlertBoxsView();
                                    });
                                }
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(userLoginResponse.message));
                                });
                            }
                            #endregion

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
        }
        /// <summary>
        /// Method for User Type Data
        /// </summary>
        public async Task UserTypeData()
        {
            try
            {
                IsLoader = true;
                var data = await UtilHelper.GetUserTypeFromAPI().ConfigureAwait(true);
                if (data != null)
                {
                    ListOfUserType = new ObservableCollection<UserTypeModel>(data.Select(a => new UserTypeModel()
                    {
                        UserTypeId = a.id,
                        UserType = a.name
                    }).ToList());
                    if (Preferences.Get(ConstantValues.IsRememberPasswordPref, false))
                    {
                        UserTypeValue = ListOfUserType.Where(x => x.UserTypeId == Preferences.Get(ConstantValues.UserTypePref, 0)).FirstOrDefault();

                        Username = Preferences.Get(ConstantValues.EmailPref, string.Empty);
                        Password = Preferences.Get(ConstantValues.PasswordPref, string.Empty);
                        IsRememberPassword = true;
                    }
                    IsVisibleRightsAndProvisionAccepted = false;

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
        /// Method for ValidateLoginControls
        /// </summary>
        /// <returns></returns>
        private bool ValidateLoginControls()
        {
            var res = true;
            try
            {
                if (UserTypeValue == null)
                {
                    UserTypeErrorMessage = ResourceValues.SelectUserTypeErrorMessage;
                    IsVisibleuserTypeErrorMessage = true;
                    res = false;
                }
                else if (UserTypeValue.UserTypeId == 0)
                {
                    UserTypeErrorMessage = ResourceValues.SelectUserTypeErrorMessage;
                    IsVisibleuserTypeErrorMessage = true;
                    res = false;
                }
                if (string.IsNullOrEmpty(username))
                {
                    UsernameErrorMsg = ResourceValues.EmailErrorMessage;
                    IsVisibleUsernameError = true;
                    res = false;
                }
                if (string.IsNullOrEmpty(Password))
                {
                    PasswordErrorMsg = ResourceValues.EnterPassword;
                    IsVisiblePasswordError = true;
                    res = false;
                }
                //return res = true;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return res;
        }
        /// <summary>
        /// Method for Clear Fields 
        /// </summary>
        private void ClearFields()
        {
            try
            {
                UserTypeValue.UserTypeId = 0;
                Username = string.Empty;
                Password = string.Empty;
                IsVisibleuserTypeErrorMessage = false;
                IsVisibleUsernameError = false;
                IsVisiblePasswordError = false;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }


        #endregion

    }
}
