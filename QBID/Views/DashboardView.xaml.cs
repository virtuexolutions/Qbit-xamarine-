using QBid.Helpers;
using QBid.QBidResource;
using QBid.ViewModels;
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
   
    public partial class DashboardView : MasterDetailPage
    {
        public DashboardView()
        {
            InitializeComponent();
           
            if (Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Member)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(DashboardTabbedView)));
            }
            else if(Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Negotiator)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(NegotiatorTabbbedPage)));
            }

        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.AppExistMessage, ResourceValues.YesButtontext, ResourceValues.NoButtontext);
                if (result)
                {
                    Preferences.Set(ConstantValues.IsloginPref, false);
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    System.Diagnostics.Process.GetCurrentProcess().Close();
                }// or anything else
            });

            return true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //VM.GetLoginUser();
            VM.AppVersion = VersionTracking.CurrentVersion;
            
            if(Preferences.Get(ConstantValues.IsBankAccountStatusPref, null) !=null && Preferences.Get(ConstantValues.IsNegotiatorAccountAddedPref,0) !=0)
            {
                if (Preferences.Get(ConstantValues.IsBankAccountStatusPref, null).ToLower() == ConstantValues.BankAccountActiveStatus.ToLower())
                {
                    VM.BankAccountStatusImage = ConstantValues.BankAccountActiveImage;
                }
                else
                {
                    VM.BankAccountStatusImage = ConstantValues.BankAccountInActiveImage; ;
                }
            }
     
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DashBoard.IsPresented = false;
        }
    }
}