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
    public partial class QutationListView : ContentPage
    {
        public QutationListView()
        {
            InitializeComponent();
        }

       
        protected  override void OnAppearing()
        {
            base.OnAppearing();
           
            
            VM.IsCallorSmsShow = false;
            VM.ItemThreshold = 1;
            VM.GetUserQuotationDetail();
            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    webView.On<Android>().EnableZoomControls(true);
            //    webView.On<Android>().DisplayZoomControls(true);
            //}
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