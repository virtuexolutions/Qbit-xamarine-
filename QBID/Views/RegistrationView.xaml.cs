using QBid.APILog;
using QBid.Helpers;
using QBid.QBidResource;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationView : ContentPage
    {

        public RegistrationView()
        {
            InitializeComponent();
        }

        protected  override void OnAppearing()
        {
            try
            {
                base.OnAppearing(); 
                //await VM.GetState().ConfigureAwait(false);
                
                var lang = string.Empty;
               
                if (QBidHelper.LanguageDetails != null && QBidHelper.LanguageDetails.Count > 0)
                {
                    VM.Language = string.Empty;
                    lang = String.Empty;
                    foreach (var item in QBidHelper.LanguageDetails)
                    {
                        if (item.IsCheckedLang && item.IsSubmit)
                        {
                            lang = lang + ConstantValues.OneSpace + item.Name + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(lang))
                    {
                        VM.Language = lang.Remove(lang.Length - 1, 1);
                    }
                }
                else
                {

                   // VM.Language = string.Empty;
                }
                var serviceDetails = string.Empty;
                if (QBidHelper.SelectServiceDetails != null && QBidHelper.SelectServiceDetails.Count > 0)
                {
                    foreach (var item in QBidHelper.SelectServiceDetails)
                    {
                        if (item.IsInYearVisible)
                        {
                            serviceDetails = serviceDetails + ConstantValues.OneSpace + item.Name + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(serviceDetails))
                    {
                        VM.SelectService = serviceDetails.Remove(serviceDetails.Length - 1, 1);
                    }
                }
                else
                {
                  //  VM.SelectService = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            VM.IsLoader = true;
            Task.Delay(2500);
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Task.Delay(2500);
            VM.IsLoader = false;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.AppExistMessage, ResourceValues.YesButtontext, ResourceValues.NoButtontext);
                if (result) await this.Navigation.PopAsync(); 
            });

            return true;
        }
    }
}