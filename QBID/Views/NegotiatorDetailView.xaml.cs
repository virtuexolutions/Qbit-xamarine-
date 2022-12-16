using QBid.APILog;
using QBid.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NegotiatorDetailView : ContentPage
    {
        public NegotiatorDetailView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.IsCallorSmsShow = false;
            VM.ItemTreshold = 0;
            VM.GetUserQuotationDetail();


            var id = Preferences.Get(ConstantValues.QuotationIdPref, string.Empty);
            if (!string.IsNullOrWhiteSpace(id))
            {
                QBidHelper.QuotationId = Convert.ToInt32(id);
                Preferences.Set(ConstantValues.QuotationIdPref, string.Empty);
            }
            if (Device.RuntimePlatform == Device.Android)
            {
                webView.On<Android>().EnableZoomControls(true);
                webView.On<Android>().DisplayZoomControls(true);
            }
        }

        private void TapGestureRecognizer_HideDrawer(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {

                    VM.IsCallorSmsShow = false;
                });

            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            finally
            {

            }

        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {

                    VM.IsCallorSmsShow = false;
                });
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            finally
            {

            }

        }
    }
}