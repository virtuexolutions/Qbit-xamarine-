using QBid.QBidResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertBoxsView : ContentPage
    {
        public AlertBoxsView()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert(ResourceValues.TitleAlert, QBidResource.ResourceValues.AppExistMessage, ResourceValues.YesButtontext, ResourceValues.NoButtontext);
                if (result)
                {
                   
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    System.Diagnostics.Process.GetCurrentProcess().Close();
                }
            });

            return true;
        }
    }
}