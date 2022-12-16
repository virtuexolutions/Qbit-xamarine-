
using QBid.QBidResource;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLoginView : ContentPage
    {
        public UserLoginView()
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
        protected async override void OnAppearing()
        {
            base.OnAppearing();
          
                VM.AppVersion = VersionTracking.CurrentVersion;
                await VM.UserTypeData();
            
            
            

        }

    }
}