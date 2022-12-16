using QBid.Helpers;
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
    public partial class NegotiatorBankDetailView : ContentPage
    {
        public NegotiatorBankDetailView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            switch (QBidHelper.NavigatePage)
            {
                case "UserLoginView":
                    VM.SkipShowHide = true;
                    VM.IsArrowShowHide = false;
                    break;
                default:
                    VM.SkipShowHide = false;
                    VM.IsArrowShowHide = true;
                    break;
            }
            if (!NegotiatorBankDetailViewModel.isFromFileUpload)
            {
                if (Preferences.Get(ConstantValues.IsNegotiatorAccountAddedPref, 0) == 1)
                {
                    VM.GetBankAccountDetail();
                    VM.MaxLength = 9;
                    VM.IsEditShow = true;
                    VM.IsCancelShow = false;
                    VM.IsUpdateShow = false;
                    VM.IsSubmitShow = false;
                    VM.IsDOBvisible = false;
                }
                else
                {
                    VM.MaxLength = 4;
                    VM.PlaceHolderSSN = "Last 4 Digit SSN Number";
                    VM.TitalSSN = "SSN Number";
                    VM.SSLNoError = "Please enter last 4 digit ssn number.";
                   // VM.GetBankAccountDetail();

                    VM.IsEditShow = false;
                    VM.IsCancelShow = false;
                    VM.IsUpdateShow = false;
                    VM.IsSubmitShow = true;
                    VM.Isvisible = false;
                    VM.IsSSNvisible = true;
                    VM.IsDOBvisible = true;

                }
            }
        }

        
    }
    
}