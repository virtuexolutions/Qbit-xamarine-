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
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class DashboardViewModel : BindableObject
    {
        APIService apiservices = new APIService();

        public DashboardViewModel()
        {
            AppLogo = ConstantValues.AppLogo;
            GetLoginUser();

            int userType = Preferences.Get(ConstantValues.UserTypePref, 0);
            if (userType == (int)UtilHelper.UserRoleType.Member)
            {
                IsLoggedInUserMember = true;
                IsLoggedInNegotiator = false;
            }
            else
            {
                IsLoggedInUserMember = false;
                IsLoggedInNegotiator = true;
            }
            MessagingCenter.Subscribe<ImageSource>(this, "ProfilePick", (value) =>
            {
                ProfileImage = value;
            });
        }


        #region Property

        private bool isLoader;
        /// <summary>
        /// Property for isLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
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

        private bool isLoggedInUserMember;
        /// <summary>
        /// property to show/hide card payment menu based on user
        /// </summary>
        public bool IsLoggedInUserMember
        {
            get { return isLoggedInUserMember; }
            set { isLoggedInUserMember = value; OnPropertyChanged(nameof(IsLoggedInUserMember)); }
        }
        private bool isLoggedInNegotiator;
        /// <summary>
        /// property to show/hide Bank account menu based on user
        /// </summary>
        public bool IsLoggedInNegotiator
        {
            get { return isLoggedInNegotiator; }
            set { isLoggedInNegotiator = value; OnPropertyChanged(nameof(IsLoggedInNegotiator)); }
        }
        private string appLogo;
        /// <summary>
        /// Property for FirstName
        /// </summary>
        public string AppLogo
        {
            get { return appLogo; }
            set { appLogo = value; OnPropertyChanged(nameof(AppLogo)); }
        }

        private string firstName;
        /// <summary>
        /// Property for FirstName
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        private string lastName;
        /// <summary>
        /// Property for LastName
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private ImageSource profileImage;
        /// <summary>
        /// Property for ProfileImage
        /// </summary>
        public ImageSource ProfileImage
        {
            get { return profileImage; }
            set { profileImage = value; OnPropertyChanged(nameof(ProfileImage)); }
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
        private string logedinUser;
        /// <summary>
        /// property for logedinUser
        /// </summary>
        public string LogedinUser
        {
            get { return logedinUser; }
            set { logedinUser = value; }
        }

        #endregion

        #region Commands

        
        private Command commandOnlogout;
        /// <summary>
        /// This Command use for commandOnlogout
        /// </summary>
        public Command CommandOnlogout
        {
            get
            {
                if (commandOnlogout == null)
                {
                    commandOnlogout = new Command(async () =>
                 {
                     try
                     {
                         var current = Connectivity.NetworkAccess;
                         if (current == Xamarin.Essentials.NetworkAccess.Internet)
                         {

                             string DToken = Preferences.Get(ConstantValues.FcmTokenPreferenceText, string.Empty);
                             int UID = Preferences.Get(ConstantValues.UserTypePref, 0);
                             LogoutModel logoutModel = new LogoutModel();
                             logoutModel.deviceToken = DToken;
                             logoutModel.roleId = Convert.ToString(UID);
                             bool isresult = await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.ConfirmLogOutMessage, ResourceValues.YesButtontext, ResourceValues.NoButtontext);
                             if (isresult)
                             {
                                 IsLoader = true;
                                 var response = await apiservices.LogOutByMember(logoutModel);
                                 if (response)
                                 {
                                     Preferences.Set(ConstantValues.EditProfilePref, false);
                                     Preferences.Set(ConstantValues.IsloginPref, false);
                                     Preferences.Set(ConstantValues.TokenKeyText, null);
                                     Preferences.Set(ConstantValues.CustomerStripeIdPref, 0);
                                     Preferences.Set(ConstantValues.IsMemberCardAddedPref, 0);
                                     Preferences.Set(ConstantValues.IsNegotiatorAccountAddedPref, 0);
                                     
                                     if (Preferences.Get(ConstantValues.IsRememberPasswordPref, false) == false)
                                     {
                                         Preferences.Set(ConstantValues.UserTypePref, 0);
                                     }
                                     Preferences.Set(ConstantValues.IsBankVerifiedPref, null);
                                     Preferences.Set(ConstantValues.IsBankAccountStatusPref, null);
                                     Preferences.Set(ConstantValues.TokenKeyText, null);
                                     if (isresult)
                                     {
                                         Preferences.Set(ConstantValues.QuotationIdPref, string.Empty);
                                         App.Current.MainPage = new NavigationPage(new UserLoginView());

                                     }

                                 }
                                 else
                                 {
                                     DependencyService.Get<IToastMessage>().ShortAlert(ResourceValues.ApiErrorMessage);
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
                         IsLoader = false;
                     }
                 });
                }
                return commandOnlogout;
            }
        }

        private Command commandOnCardDetail;
        /// <summary>
        /// This Command use for commandOnCardDetail
        /// </summary>
        public Command CommandOnCardDetail
        {
            get
            {
                if (commandOnCardDetail == null)
                {
                    commandOnCardDetail = new Command(() =>
                    {
                        try
                        {

                            App.Current.MainPage.Navigation.PushAsync(new PaymetCardList());
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });

                }
                return commandOnCardDetail;
            }
        }

        private Command commandOnUpdateProfile;
        /// <summary>
        /// This Command use for commandOnCardDetail
        /// </summary>
        public Command CommandOnOnUpdateProfile
        {
            get
            {
                if (commandOnUpdateProfile == null)
                {
                    commandOnUpdateProfile = new Command(() =>
                    {
                        try
                        {

                            Preferences.Set(ConstantValues.EditProfilePref, true);
                            App.Current.MainPage.Navigation.PushAsync(new RegistrationView());

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnUpdateProfile;
            }
        }

        private Command commandOnHowToUseApp;
        /// <summary>
        /// This Command use for HowToUseApp
        /// </summary>
        public Command CommandOnHowToUseApp
        {
            get
            {
                if (commandOnHowToUseApp == null)
                {
                    commandOnHowToUseApp = new Command(async () =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                var uRlLink = APIHttpHelper.DocBaseURL + ConstantValues.HowToUseAppLink;
                                var uri = new Uri(uRlLink);
                                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);

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
                return commandOnHowToUseApp;
            }
        }

        private Command commandOnNegotiatorBankAccount;
        /// <summary>
        /// This Command use for commandOnCardDetail
        /// </summary>
        public Command CommandOnNegotiatorBankAccount
        {
            get
            {
                if (commandOnNegotiatorBankAccount == null)
                {
                    commandOnNegotiatorBankAccount = new Command(() =>
                    {
                        try
                        {
                            App.Current.MainPage.Navigation.PushAsync(new NegotiatorBankDetailView());
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnNegotiatorBankAccount;
            }
        }

        private Command commandOnAddQueryAndComplaint;
        /// <summary>
        /// This Command use for commandOnCardDetail
        /// </summary>
        public Command CommandOnAddQueryAndComplaint
        {
            get
            {
                if (commandOnAddQueryAndComplaint == null)
                {
                    commandOnAddQueryAndComplaint = new Command(() =>
                    {
                        try
                        {
                            App.Current.MainPage.Navigation.PushAsync(new QueryAndComplaintView());
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnAddQueryAndComplaint;
            }
        }



        #endregion

        #region Methods

        /// <summary>
        /// method for get login user
        /// </summary>
        public async void GetLoginUser()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
                    if (userTypeId == (int)UtilHelper.UserRoleType.Member)
                    {

                        UserDetailResponse response = new UserDetailResponse();
                        response = await apiservices.GetUserDetail();
                        if (response.data != null)
                        {
                            FirstName = response.data.firstName;
                            LastName = response.data.lastName;
                            ProfileImage = response.data.profilePicture;
                        }
                    }
                    else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                    {

                        NegotiatorDetailResponce response = new NegotiatorDetailResponce();
                        response = await apiservices.GetNegotiatorDetail();
                        if (response != null)
                        {
                            if (response.data != null)
                            {
                                FirstName = response.data.firstName;
                                LastName = response.data.lastName;
                                if (!string.IsNullOrWhiteSpace(response.data.profilePicture))
                                {
                                    ProfileImage = response.data.profilePicture;
                                }
                                else
                                {
                                    ProfileImage = ConstantValues.NegotiatorProfileImage;
                                }
                            }
                        }
                    }
                    else
                    {

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
        }

        #endregion
    }
}
