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
    public class AlertBoxViewModel : BindableObject
    {
        APIService aPIServices;
        #region Constructor
        public AlertBoxViewModel()
        {
            SetUp();
        }
        #endregion

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

        private bool isRightsAndProvisionLoader;
        /// <summary>
        /// Property for isRightsAndProvisionLoader
        /// </summary>

        public bool IsRightsAndProvisionLoader
        {
            get { return isRightsAndProvisionLoader; }
            set { isRightsAndProvisionLoader = value; OnPropertyChanged(nameof(IsRightsAndProvisionLoader)); }
        }

        private bool isVisibleAcceptRightAndProvisionErrorMessage;
        /// <summary>
        /// Property for isVisibleAcceptRightAndProvisionErrorMessage
        /// </summary>

        public bool IsVisibleAcceptRightAndProvisionErrorMessage
        {
            get { return isVisibleAcceptRightAndProvisionErrorMessage; }
            set { isVisibleAcceptRightAndProvisionErrorMessage = value; OnPropertyChanged(nameof(IsVisibleAcceptRightAndProvisionErrorMessage)); }
        }

        private string acceptRightAndProvisionErrorMessage;
        /// <summary>
        ///  Property for acceptRightAndProvisionErrorMessage
        /// </summary>

        public string AcceptRightAndProvisionErrorMessage
        {
            get { return acceptRightAndProvisionErrorMessage; }
            set { acceptRightAndProvisionErrorMessage = value; OnPropertyChanged(nameof(AcceptRightAndProvisionErrorMessage)); }
        }

        private bool isCheckedAcceptRights;
        /// <summary>
        ///  Property for isCheckedAcceptRights
        /// </summary>

        public bool IsCheckedAcceptRights
        {
            get { return isCheckedAcceptRights; }
            set
            {
                isCheckedAcceptRights = value; OnPropertyChanged(nameof(IsCheckedAcceptRights));
                if (isCheckedAcceptRights)
                {
                    IsVisibleAcceptRightAndProvisionErrorMessage = false;
                }
            }
        }

        private bool isVisibleRightsAndProvisionAccepted;
        /// <summary>
        /// Property for isVisibleRightsAndProvisionAccepted
        /// </summary>

        public bool IsVisibleRightsAndProvisionAccepted
        {
            get { return isVisibleRightsAndProvisionAccepted; }
            set { isVisibleRightsAndProvisionAccepted = value; OnPropertyChanged(nameof(IsVisibleRightsAndProvisionAccepted)); }
        }
        #endregion

        #region Commands
        private Command cmdOnAcceptRightsAndProvision;
        /// <summary>
        /// This command use for process AcceptRightsAndProvision
        /// </summary>
        public Command CommandOnAcceptRightsAndProvision
        {
            get
            {
                if (cmdOnAcceptRightsAndProvision == null)
                {
                    cmdOnAcceptRightsAndProvision = new Command(async () =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {

                                IsLoader = true;
                                if (IsCheckedAcceptRights)
                                {
                                    IsLoader = true;
                                    aPIServices = new APIService();
                                    AcceptRightAndProvisionRequest acceptRightAndProvisionRequest = new AcceptRightAndProvisionRequest();
                                    acceptRightAndProvisionRequest.status = 1;
                                    var userTypeId = Preferences.Get(ConstantValues.UserTypePref, 0);
                                    var acceptRAPResponse = await aPIServices.AcceptRightAndProvisionNegotiator(acceptRightAndProvisionRequest, userTypeId).ConfigureAwait(false);
                                    if (true)
                                    {
                                        IsVisibleRightsAndProvisionAccepted = false;
                                        Preferences.Set(ConstantValues.IsRightsAndProvisionAcceptedPref, true);
                                        Device.BeginInvokeOnMainThread(() =>
                                       {
                                           App.Current.MainPage = new NavigationPage(new DashboardView());
                                       });
                                    }
                                }
                                else
                                {
                                    IsVisibleAcceptRightAndProvisionErrorMessage = true;
                                    AcceptRightAndProvisionErrorMessage = ResourceValues.AcceptRightAndProvisionErrorMessage;
                                }
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
                            IsLoader = false;
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return cmdOnAcceptRightsAndProvision;
            }
        }
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

                            Uri url = null;
                            url = new Uri("https://drive.google.com/viewerng/viewer?embedded=true&url=" + APIHttpHelper.DocBaseURL + ConstantValues.RightandProvisionsentirePDF);

                            await Browser.OpenAsync(url, new BrowserLaunchOptions
                            {
                                LaunchMode = BrowserLaunchMode.SystemPreferred,
                                TitleMode = BrowserTitleMode.Show,
                                PreferredToolbarColor = Color.FromHex("2CD49C"),
                                PreferredControlColor = Color.FromHex("ffffff")
                            });
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

        private Command commandOpenTermsofUsePdf;
        /// <summary>
        /// command for show Terms of Use PDF
        /// </summary>
        public Command CommandOpenTermofUsePdf
        {
            get
            {
                if (commandOpenTermsofUsePdf == null)
                {
                    commandOpenTermsofUsePdf = new Command(async () =>
                    {
                        try
                        {
                            Uri url = null;
                            url = new Uri("https://drive.google.com/viewerng/viewer?embedded=true&url=" + APIHttpHelper.DocBaseURL + ConstantValues.TermsofUsePDF);


                            await Browser.OpenAsync(url, new BrowserLaunchOptions
                            {
                                LaunchMode = BrowserLaunchMode.SystemPreferred,
                                TitleMode = BrowserTitleMode.Show,
                                PreferredToolbarColor = Color.FromHex("2CD49C"),
                                PreferredControlColor = Color.FromHex("ffffff")
                            });
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOpenTermsofUsePdf;
            }


        }

        #endregion



        #region ALL Methods
        private async Task SetUp()
        {
            if (!Preferences.Get(ConstantValues.IsRightsAndProvisionAcceptedPref, false))
            {
                var result = await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, QBidResource.ResourceValues.AppExistMessage, ResourceValues.YesButtontext, string.Empty).ConfigureAwait(true);
                if (result)
                {
                    bool chk = App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, QBidResource.ResourceValues.AppExistMessage, ResourceValues.YesButtontext).IsCompleted;
                }
            }
        }


        #endregion

    }
}
