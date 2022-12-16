using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class ProfileViewModel : BindableObject
    {
        APIService aPIServices = new APIService();
        public ProfileViewModel()
        {

        }

        #region Property


        private bool isVisibleDeleteBTN;
        /// <summary>
        /// Property for IsVisibleDeleteBTN
        /// </summary>
        public bool IsVisibleDeleteBTN
        {
            get { return isVisibleDeleteBTN; }
            set { isVisibleDeleteBTN = value; OnPropertyChanged(nameof(IsVisibleDeleteBTN)); }
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
        private string contact;
        /// <summary>
        /// Property for Contact
        /// </summary>
        public string Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyChanged(nameof(Contact)); }
        }

        private string email;
        /// <summary>
        /// Property for Email
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string address;
        /// <summary>
        /// Property for Address
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }

        private string zipCode;
        /// <summary>
        /// Property for ZipCode
        /// </summary>
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; OnPropertyChanged(nameof(ZipCode)); }
        }

        private string userType;
        /// <summary>
        /// Property for UserType
        /// </summary>
        public string UserType
        {
            get { return userType; }
            set { userType = value; OnPropertyChanged(nameof(UserType)); }
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

        private string services;
        /// <summary>
        /// Property for Services
        /// </summary>
        public string Services
        {
            get { return services; }
            set { services = value; OnPropertyChanged(nameof(Services)); }
        }
        private string companyName;
        /// <summary>
        /// Property for CompanyName
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; OnPropertyChanged(nameof(CompanyName)); }
        }

        private string ratting;
        /// <summary>
        /// Property for Ratting
        /// </summary>
        public string Ratting
        {
            get { return ratting; }
            set { ratting = value; OnPropertyChanged(nameof(Ratting)); }
        }

        private string language;
        /// <summary>
        /// Property for Language
        /// </summary>
        public string Language
        {
            get { return language; }
            set { language = value; OnPropertyChanged(nameof(Language)); }
        }

        private int totalQbid;
        /// <summary>
        /// Property for TotalQbid
        /// </summary>
        public int TotalQbid
        {
            get { return totalQbid; }
            set { totalQbid = value; OnPropertyChanged(nameof(TotalQbid)); }
        }

        private int totalApproved;
        /// <summary>
        /// Property for TotalApproved
        /// </summary>
        public int TotalApproved
        {
            get { return totalApproved; }
            set { totalApproved = value; OnPropertyChanged(nameof(TotalApproved)); }
        }

        private int totalDeclined;
        /// <summary>
        /// Property for totalDeclined
        /// </summary>
        public int TotalDeclined
        {
            get { return totalDeclined; }
            set { totalDeclined = value; OnPropertyChanged(nameof(TotalDeclined)); }
        }

        private int totalInProgress;
        /// <summary>
        /// Property for totalDeclined
        /// </summary>
        public int TotalInProgress
        {
            get { return totalInProgress; }
            set { totalInProgress = value; OnPropertyChanged(nameof(TotalInProgress)); }
        }

        private string totalBonus;
        /// <summary>
        /// Property for total bonus
        public string TotalBonus
        {
            get { return totalBonus; }
            set { totalBonus = value; OnPropertyChanged(nameof(TotalBonus)); }
        }

        private string totalCommision;
        /// <summary>
        /// Property for total commision
        public string TotalCommision
        {
            get { return totalCommision; }
            set { totalCommision = value; OnPropertyChanged(nameof(TotalCommision)); }
        }

        private string totalEarning;
        /// <summary>
        /// Property for total Earning
        public string TotalEarning
        {
            get { return totalEarning; }
            set { totalEarning = value; OnPropertyChanged(nameof(TotalEarning)); }
        }

        private string totalSaving;
        /// <summary>
        /// Property for total Saving
        public string TotalSaving
        {
            get { return totalSaving; }
            set { totalSaving = value; OnPropertyChanged(nameof(TotalSaving)); }
        }

        private bool isRatingStarShow;
        /// <summary>
        /// Property for Rating star
        /// </summary>
        public bool IsRatingStarShow
        {
            get { return isRatingStarShow; }
            set { isRatingStarShow = value; OnPropertyChanged(nameof(IsRatingStarShow)); }
        }
        private bool isShowHide;
        /// <summary>
        /// Property for IsShowHide
        /// </summary>
        public bool IsShowHide
        {
            get { return isShowHide; }
            set { isShowHide = value; OnPropertyChanged(nameof(IsShowHide)); }
        }


        private bool isvisible = true;
        /// <summary>
        /// Property for ShowHide label for negotiator
        /// </summary>
        public bool Isvisible
        {
            get { return isvisible; }
            set { isvisible = value; OnPropertyChanged(nameof(Isvisible)); }
        }

        private bool isvisibleForMember = true;
        /// <summary>
        /// Property for ShowHide label for member
        /// </summary>
        public bool IsvisibleForMember
        {
            get { return isvisibleForMember; }
            set { isvisibleForMember = value; OnPropertyChanged(nameof(IsvisibleForMember)); }
        }

        private bool isImagePopupVisible;
        /// <summary>
        /// Property for IsImagePopupVisible
        /// </summary>
        public bool IsImagePopupVisible
        {
            get { return isImagePopupVisible; }
            set { isImagePopupVisible = value; OnPropertyChanged(nameof(IsImagePopupVisible)); }
        }

        private bool isDeleteVisible;
        /// <summary>
        /// Property for IsDeleteVisible
        /// </summary>
        public bool IsDeleteVisible
        {
            get { return isDeleteVisible; }
            set { isDeleteVisible = value; OnPropertyChanged(nameof(IsDeleteVisible)); }
        }

        private bool isImageUploaded;
        /// <summary>
        /// Property for IsImageUploaded
        /// </summary>
        public bool IsImageUploaded
        {
            get { return isImageUploaded; }
            set { isImageUploaded = value; OnPropertyChanged(nameof(IsImageUploaded)); }
        }
        private string imageStatus;
        /// <summary>
        /// Property for ImageStatus
        /// </summary>
        public string ImageStatus
        {
            get { return imageStatus; }
            set { imageStatus = value; OnPropertyChanged(nameof(ImageStatus)); }
        }

        private string qbidStatusName;
        /// <summary>
        /// Property for ImageStatus
        /// </summary>
        public string QbidStatusName
        {
            get { return qbidStatusName; }
            set { qbidStatusName = value; OnPropertyChanged(nameof(QbidStatusName)); }
        }

        private string statusImage;
        /// <summary>
        /// Property for StatusImage
        /// </summary>
        public string StatusImage
        {
            get { return statusImage; }
            set { statusImage = value; OnPropertyChanged(nameof(StatusImage)); }
        }

        private ImageSource imageSource;
        /// <summary>
        /// Property for ImageSource
        /// </summary>
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; OnPropertyChanged(nameof(ImageSource)); }
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

        #endregion


        #region Command

        private Command commandOnDeleteAccount;
        /// <summary>
        /// This command use for Delete user account
        /// </summary>
        public Command CommandOnDeleteAccount
        {
            get
            {
                if (commandOnDeleteAccount == null)
                {
                    commandOnDeleteAccount = new Command(async () =>
                    {
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(DeleteAccount, ResourceValues.YesButtontext, ResourceValues.NoButtontext, ResourceValues.ConfirmDeleteAccountMessage, true)).ConfigureAwait(true);
                    });
                }
                return commandOnDeleteAccount;
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

        #endregion

        #region Methods
        /// <summary>
        /// method for delete user account
        /// </summary>
        public async void DeleteAccount()
        {
            var value = QBidHelper.AccceptQuotationStatus;
            switch (value)
            {
                case true:
                    try
                    {
                        var current = Connectivity.NetworkAccess;
                        if (current == Xamarin.Essentials.NetworkAccess.Internet)
                        {
                            IsLoader = true;

                            var deleteUserRequestModel = new DeleteUserRequestModel();
                            deleteUserRequestModel.roleId = Preferences.Get(ConstantValues.UserTypePref, 0).ToString();

                            var deleteAccountResponse = await aPIServices.DeleteUserAccountAPI(deleteUserRequestModel).ConfigureAwait(false);

                            if(deleteAccountResponse!=null)
                            {

                            
                                if (deleteAccountResponse.code == (int)HttpStatusCode.OK)
                                {
                                    Preferences.Set(ConstantValues.EditProfilePref, false);
                                    Preferences.Set(ConstantValues.IsloginPref, false);
                                    Preferences.Set(ConstantValues.TokenKeyText, null);
                                    Preferences.Set(ConstantValues.CustomerStripeIdPref, 0);
                                    Preferences.Set(ConstantValues.IsMemberCardAddedPref, 0);
                                    Preferences.Set(ConstantValues.IsNegotiatorAccountAddedPref, 0);
                                    //Preferences.Set(ConstantValues.RoleIdPref, 0);  //RoleId 1=admin, 2=Member, 3=Negotiator
                                    Preferences.Set(ConstantValues.UserTypePref, 0);
                                    Preferences.Set(ConstantValues.IsBankVerifiedPref, null);
                                    Preferences.Set(ConstantValues.IsBankAccountStatusPref, null);
                                    Preferences.Set(ConstantValues.QuotationIdPref, string.Empty);
                                    Preferences.Set(ConstantValues.IsRememberPasswordPref, true);                               
                                    Preferences.Set(ConstantValues.EmailPref, null);
                                    Preferences.Set(ConstantValues.PasswordPref, null);
                                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(ConfirmDeleteAccount, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, deleteAccountResponse.message.ToString(), false)).ConfigureAwait(true);

                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread( () =>
                                    {
                                        DependencyService.Get<IToastMessage>().ShortAlert(deleteAccountResponse.message.ToString());
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

                    break;
                case false:
                    break;
            }
        }

        /// <summary>
        /// Popup for delete account
        /// </summary>
        public  void ConfirmDeleteAccount()
        {
            var value = QBidHelper.AccceptQuotationStatus;
            switch (value)
            {
                case true:
                    try
                    {

                        App.Current.MainPage = new NavigationPage(new UserLoginView());

                    }
                    catch (Exception ex)
                    {

                        LogManager.TraceErrorLog(ex);
                    }
                    break;

                case false:
                    break;
            }
        }

        /// <summary>
        /// method for set photo from gallery 
        /// </summary>
        /// <returns></returns>
        public async Task GalleryClick()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await Application.Current.MainPage.DisplayAlert(ResourceValues.TitelGallery, ResourceValues.GalleryErrorMessage, ResourceValues.OkButtontext);
                        return;
                    }

                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        //PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                        CustomPhotoSize = 10
                    });


                    if (file == null)
                    {
                        return;
                    }
                    else
                    {

                        IsLoader = true;

                        Stream stream = file.GetStream();
                        var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
                        var response = new UpdateProfilePictureResponseModel();
                        if (userTypeId == (int)UtilHelper.UserRoleType.Member)
                        {
                            response = await aPIServices.UpdateProfilePictureUser(stream).ConfigureAwait(false);
                        }
                        else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                        {
                            response = await aPIServices.UpdateProfilePictureNegotiator(stream).ConfigureAwait(true);
                        }
                        if (stream != null)
                        {
                            file.Dispose();
                        }
                        if (response != null)
                        {
                            if (response.data != null)
                            {
                                ProfileImage = response.data.ToString();
                                UtilHelper.UpdatedProfileImage = ProfileImage;
                                MessagingCenter.Send<ImageSource>(response.data.ToString(), "ProfilePick");
                                IsVisibleDeleteBTN = true;


                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IToastMessage>().ShortAlert(response.message.ToString());
                                });


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
                isTap = false;

            }
        }

        /// <summary>
        /// method for set photo from camera
        /// </summary>
        public async void CameraClick()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await Application.Current.MainPage.DisplayAlert(ResourceValues.TitelCamera, ResourceValues.CameraErrorMessage, ResourceValues.OkButtontext);
                        return;
                    }
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        //PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                        CustomPhotoSize = 10,
                        Directory = "profilePicture",
                        Name = "test.jpg",
                    });

                    if (file == null)
                    {
                        return;
                    }
                    else
                    {

                        IsLoader = true;

                        Stream stream = file.GetStream();
                        var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
                        var response = new UpdateProfilePictureResponseModel();
                        if (userTypeId == (int)UtilHelper.UserRoleType.Member)
                        {
                            response = await aPIServices.UpdateProfilePictureUser(stream);
                        }
                        else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                        {
                            response = await aPIServices.UpdateProfilePictureNegotiator(stream);
                        }

                        if (stream != null)
                        {
                            file.Dispose();
                        }
                        if (response != null)
                        {
                            if (response.data != null)
                            {
                                ProfileImage = response.data.ToString();
                                UtilHelper.UpdatedProfileImage = ProfileImage;
                                MessagingCenter.Send<ImageSource>(response.data.ToString(), "ProfilePick");
                                IsVisibleDeleteBTN = true;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IToastMessage>().ShortAlert(response.message.ToString());
                                });

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
                isTap = false;

            }
        }

        /// <summary>
        /// method for delete profile photo
        /// </summary>
        public async void DeleteClick()
        {
            var action = await Application.Current.MainPage.DisplayAlert(ResourceValues.TitelDelete, ResourceValues.ConfirmPicDeleteMessage, ResourceValues.YesButtontext, ResourceValues.NoButtontext);
            if (action)
            {
                try
                {
                    var current = Connectivity.NetworkAccess;
                    if (current == Xamarin.Essentials.NetworkAccess.Internet)
                    {
                        IsLoader = true;

                        var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
                        var response = new UpdateProfilePictureResponseModel();
                        if (userTypeId == (int)UtilHelper.UserRoleType.Member)
                        {
                            response = await aPIServices.DeleteProfilePictureUser();
                            ProfileImage = string.Empty;
                            IsVisibleDeleteBTN = false;
                        }
                        else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                        {
                            response = await aPIServices.DeleteProfilePictureNegotiator();
                            ProfileImage = string.Empty;
                            IsVisibleDeleteBTN = false;
                        }
                        if(response !=null)
                        {
                            if (response.success)
                            {
                                ProfileImage = ConstantValues.NegotiatorProfileImage;
                                UtilHelper.UpdatedProfileImage = ProfileImage;
                                MessagingCenter.Send<ImageSource>(UtilHelper.UpdatedProfileImage, "ProfilePick");
                                DependencyService.Get<IToastMessage>().LongAlert(response.message.ToString());
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(response.message.ToString());
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
                    Task.Delay(5000);
                    IsLoader = false;

                }
            }
        }

       

        /// <summary>
        /// method for get profile data
        /// </summary>
        /// <returns></returns>
        public async Task GetUserProfile()
        {
            try
            {
                UtilHelper.PageNavigate = false;
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    //QbidStatusName = "QBid Status - ( Year: " + DateTime.UtcNow.Year + " )";
                    QbidStatusName = ResourceValues.LabelQbidStatus + ConstantValues.OneSpace + ConstantValues.Hyphen + ConstantValues.OneSpace + ConstantValues.BracketRoundFore + ConstantValues.OneSpace + ResourceValues.TextYear + ConstantValues.OneSpace + DateTime.UtcNow.Year + ConstantValues.OneSpace + ConstantValues.BracketRoundBack;
                    APIService aPIServices = new APIService();
                    var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
                    if (userTypeId == (int)UtilHelper.UserRoleType.Member)
                    {
                        Isvisible = false;
                        IsvisibleForMember = true;
                        IsShowHide = false;
                        UserType = ResourceValues.LabelProfilescreen + ConstantValues.QBidProfileTitelMember;

                        UserDetailResponse response = new UserDetailResponse();
                        response = await aPIServices.GetUserDetail();
                        if(response !=null && response.success)
                        {
                            if (response.data != null)
                            {
                                FirstName = response.data.firstName;
                                LastName = response.data.lastName;
                                Contact = response.data.mobileCall;
                                Email = response.data.email;
                                Address = response.data.addressLine1 + ConstantValues.OneSpace + response.data.addressLine2;
                                ZipCode = response.data.zipCode;
                                TotalSaving = ConstantValues.CurencySymbal + response.data.TotalSaving;
                                if (response.data.quotationCounts != null)
                                {
                                    TotalQbid = response.data.quotationCounts.totalCounts;
                                    TotalDeclined = response.data.quotationCounts.declined;
                                    TotalApproved = response.data.quotationCounts.completed;
                                    TotalInProgress = response.data.quotationCounts.inProgress;
                                }
                                if (!string.IsNullOrWhiteSpace(response.data.profilePicture))
                                {
                                    ProfileImage = response.data.profilePicture.ToString();
                                    IsVisibleDeleteBTN = true;
                                }
                                else
                                {
                                    ProfileImage = ConstantValues.NegotiatorProfileImage;
                                    IsVisibleDeleteBTN = false;

                                }
                                UtilHelper.UpdatedProfileImage = ProfileImage;
                                MessagingCenter.Send<ImageSource>(UtilHelper.UpdatedProfileImage, "ProfilePick");

                            }
                        }
                    }
                    else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                    {
                        UserType = ResourceValues.LabelProfilescreen + ConstantValues.QBidProfileTitelNegotiator;

                        IsShowHide = true;
                        IsvisibleForMember = false;
                        NegotiatorDetailResponce response = new NegotiatorDetailResponce();
                        response = await aPIServices.GetNegotiatorDetail();
                        if(response !=null && response.success)
                        {
                            if (response.data != null)
                            {

                                FirstName = response.data.firstName;
                                LastName = response.data.lastName;
                                Contact = response.data.mobileCall;
                                Email = response.data.email;
                                Address = response.data.addressLine1 + ConstantValues.OneSpace + response.data.addressLine2;
                                ZipCode = response.data.zipCode;
                                CompanyName = response.data.companyName;
                                if (response.data.negotiatorRating != null)
                                {
                                    Ratting = Convert.ToString(response.data.negotiatorRating);
                                    IsRatingStarShow = true;
                                }
                                else
                                {
                                    Ratting = string.Empty;
                                    IsRatingStarShow = false;
                                }

                                if (response.data.services != null && response.data.services.Count > 0)
                                {
                                    string servicename = string.Empty;
                                    foreach (var item in response.data.services)
                                    {
                                        servicename = servicename + item.serviceName.ToString() + ConstantValues.BracketRoundFore + "Ex-" + item.experianceTime.ToString() + ConstantValues.BracketRoundBack + ", ";
                                    }
                                    Services = servicename.Substring(0, servicename.Length - 2);
                                }
                                if (response.data.languages != null && response.data.languages.Count > 0)
                                {
                                    string languages = string.Empty;
                                    foreach (var item in response.data.languages)
                                    {
                                        languages = languages + item.languageName.ToString() + ", ";
                                    }
                                    Language = languages.Substring(0, languages.Length - 2);
                                }
                                if (response.data.totalQbid != null)
                                {
                                    TotalQbid = response.data.totalQbid.totalCounts;
                                    TotalDeclined = response.data.totalQbid.declined;
                                    TotalApproved = response.data.totalQbid.completed;
                                    TotalInProgress = response.data.totalQbid.inProgress;
                                }
                                if (response.data.commisionDetails != null)
                                {
                                    TotalBonus = ConstantValues.CurencySymbal + response.data.commisionDetails.totalBonus;
                                    TotalCommision = ConstantValues.CurencySymbal + response.data.commisionDetails.todayCommision;
                                    TotalEarning = ConstantValues.CurencySymbal + response.data.commisionDetails.totalEarning;
                                }
                                else
                                {
                                    TotalBonus = ConstantValues.CurencySymbal + ConstantValues.DefaultValue;
                                    TotalCommision = ConstantValues.CurencySymbal + ConstantValues.DefaultValue;
                                    TotalEarning = ConstantValues.CurencySymbal + ConstantValues.DefaultValue;
                                }
                                if (!string.IsNullOrWhiteSpace(response.data.profilePicture))
                                {
                                    ProfileImage = response.data.profilePicture.ToString();
                                    IsVisibleDeleteBTN = true;
                                }
                                else
                                {
                                    ProfileImage = ConstantValues.NegotiatorProfileImage;
                                    IsVisibleDeleteBTN = false;
                                }
                                UtilHelper.UpdatedProfileImage = ProfileImage;
                                MessagingCenter.Send<ImageSource>(UtilHelper.UpdatedProfileImage, "ProfilePick");
                            }
                        }
                    }
                    else
                    {
                        UserType = string.Empty;
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


        public bool isTap = false;
        #endregion


    }
}
